using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveVec;
    public float moveSpd;
    public GameObject Sprite;

    public float LifeTime;

    public void Shoot(Vector3 moveVec, float moveSpd)
    {
        this.moveVec = moveVec.normalized;
        this.moveSpd = moveSpd;
        Sprite.transform.rotation = moveVec.ToQuaternion();
        StartCoroutine(co_BulletMove());
    }

    IEnumerator co_BulletMove()
    {
        float timeLeft = LifeTime;

        while(timeLeft > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVec, moveSpd * Time.deltaTime);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }




}
