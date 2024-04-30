using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightItem : MonoBehaviour
{
    public Action onAquire;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            onAquire?.Invoke();
            SoundMgr.Inst.Play("Item");
            Destroy(gameObject);
        }
    }
}
