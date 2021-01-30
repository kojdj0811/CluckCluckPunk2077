using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornBlcok : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            player.GetComponent<PlayerController>().AddHealth(-0.5f);
            Debug.Log(player.GetComponent<Health>().currentHP);
        }
    }
}
