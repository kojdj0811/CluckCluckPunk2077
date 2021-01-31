using System.Collections;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    GameObject missileObj;

    [SerializeField]
    private float shotInterval = 6f;

    void Start()
    {
        StartCoroutine(FireCoroutine(shotInterval));
    }

    IEnumerator FireCoroutine(float intervalTime)
    {
        while(true)
        {
            Instantiate(missileObj, transform);
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
