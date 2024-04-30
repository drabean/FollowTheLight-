using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    //X¹æÇâ Shake°ª Curve
    [SerializeField] AnimationCurve shakeX;

    public void Shake(float duration, float shakeSpeed, float xPower, bool isForced = false)
    {
        StartCoroutine(co_Shake(duration, shakeSpeed, xPower, isForced));
    }

    IEnumerator co_Shake(float duration, float shakeSpeed, float xPower, bool isForced = false)
    {
        float timer = 0;
        while (timer <= duration)
        {
            float x = shakeX.Evaluate(timer * shakeSpeed) * xPower;

            transform.position = Vector3.forward * -10 + Vector3.right * x;


            timer += Time.deltaTime;
            yield return null;
        }


    }
}
