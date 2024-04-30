using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    #region INPUT
    float x;
    float y;
    //bool atk;
    #endregion
    public Animator animator;
    public SpriteRenderer sp;


    bool isDeath = false;

    // Update is called once per frame
    void Update()
    {
        if (isDeath) return;
        getInput();

        Vector3 moveVec = (x * Vector3.right + y * Vector3.up).normalized;

       // if (atk) animator.SetTrigger("doAttack");

        if (moveVec != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            sp.flipX = !(moveVec.x > 0);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVec, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    void getInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
       // atk = Input.GetButton("Jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeath) return;

        if(collision.CompareTag("Bullet"))
        {
            Death();
        }
    }


    void Death()
    {
        SoundMgr.Inst.Play("Impact");
        isDeath = true;
        Destroy(GetComponent<Collider2D>());
        animator.SetTrigger("doDeath");

        GameManager.Inst.GameOver();
    }

}
