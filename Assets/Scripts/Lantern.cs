using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    public Light2D lanternLight;

    public float curLightPower;
    public float LanternReduceSpeed;
    public Vector3[] LanternArea;

    public LightItem LightPrefab;

    public float moveSpeed = 1.5f;

    public void StartAction()
    {
        StartCoroutine(co_LanternMove());
        StartCoroutine(co_Light());
        StartCoroutine(co_LightItemSpawn());
    }

    IEnumerator co_LanternMove()
    {
        yield return new WaitForSeconds(2.0f);
        while(true)
        {
            Vector3 destination = Vector3.zero.Randomize(4).Clamp(LanternArea[0], LanternArea[1]);
            //너무 구석으로 몰리지 않도록 중앙값 보정해줌. 

            while(Vector3.Distance(transform.position, destination) >= 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(3.0f);

        }
    }

    IEnumerator co_Light()
    {
        while (curLightPower >= 0)
        {
            curLightPower -= LanternReduceSpeed * Time.deltaTime;

            lanternLight.pointLightInnerRadius = curLightPower * 0.5f;
            lanternLight.pointLightOuterRadius = curLightPower;
            yield return null;
        }
    }

    float lightSpawnTime = 4.0f;
    IEnumerator co_LightItemSpawn()
    {
        while(true)
        {
            Instantiate(LightPrefab, transform.position.Randomize(Mathf.Max(curLightPower-3f, 0)).Clamp(LanternArea[0], LanternArea[1]), Quaternion.identity).onAquire+= LanternPowerUp;
            yield return new WaitForSeconds(lightSpawnTime);
        }
    }
    public void LanternPowerUp()
    {
        curLightPower += LanternReduceSpeed * lightSpawnTime;
    }


}
