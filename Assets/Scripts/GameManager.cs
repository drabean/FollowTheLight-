using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    public Vector2[] BulletStartArea;

    public Player player;
    public Lantern lantern;

    public Bullet bullet;
    public float bulletCooltime;

        
    public TextMeshProUGUI TMP_ScoreValue;
    public GameObject UIStartGroup;
    int highScore;
    float curScore;

    public GameObject UIGameGroup;
    public TextMeshProUGUI TMP_CurScore;
    public GameObject TMP_HighScoreNotice;
    
    
    public GameObject UIEndGroup;
    public TextMeshProUGUI TMP_FinalScore;
    public GameObject TMP_FinalHighScoreNotice;

    public MainCam cam;

    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        TMP_ScoreValue.text = highScore.ToString() + " Seconds";
        SoundMgr.Inst.PlayBGM("BGM");
    }

    public void GameStart()
    {
        SoundMgr.Inst.Play("Click");

        StartCoroutine(co_BulletShoot());
        StartCoroutine(co_CountTime());
        lantern.StartAction();
        UIStartGroup.SetActive(false);
        UIGameGroup.SetActive(true);
    }
    public void GameOver()
    {
        StopAllCoroutines();
        StartCoroutine(co_EndGame());
        SoundMgr.Inst.BGMFadeout();

    }

    IEnumerator co_EndGame()
    {
        Debug.Log(curScore);

        PlayerPrefs.SetInt("HighScore", Mathf.Max(highScore, (int)curScore));
        cam.Shake(0.7f, 10, 0.07f);
        yield return new WaitForSeconds(1.5f);
        SoundMgr.Inst.Play("GameOver");
        UIGameGroup.SetActive(false);
        UIEndGroup.SetActive(true);
        TMP_FinalScore.text = ((int)curScore).ToString() + " Seconds!";
        if (isHighScore) TMP_FinalHighScoreNotice.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    IEnumerator co_BulletShoot()
    {
        yield return new WaitForSeconds(2.0f);
        while(true)
        {
            ShootBullet();
            if (bulletCooltime > 0.17f) bulletCooltime -= 0.015f;
            yield return new WaitForSeconds(bulletCooltime);
        }
    }



    public void ShootBullet()
    {
        int bulletStart = Random.Range(0, 2);
        Vector3 bulletStartPos;
        if(bulletStart ==0)
        {
            bulletStartPos = Vector3.right * (Random.Range(0, 2) == 1 ? 1 : -1) * 12 + Vector3.up * Random.Range(-7f, 7f);
        }
        else
        {
            bulletStartPos = Vector3.right * Random.Range(-12f, 12f) + Vector3.up * (Random.Range(0, 2) == 1 ? 1 : -1) * 7;
        }

        Instantiate(bullet, bulletStartPos, Quaternion.identity).Shoot(player.transform.position.Randomize(4.0f) - bulletStartPos, Random.Range(1.5f, 4f));
    }

    bool isHighScore = false;
    IEnumerator co_CountTime()
    {
        curScore = 0;
        while(true)
        {
            curScore += Time.deltaTime;
            TMP_CurScore.text = ((int)curScore).ToString() + " Seconds";

            if ((int)curScore > highScore && !isHighScore)
            {
                isHighScore = true;
                TMP_HighScoreNotice.SetActive(true);
                SoundMgr.Inst.Play("HighScore");
            }
            yield return null;
        }
    }
}
