using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBlock : MonoBehaviour
{
    public GameObject player;
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            player.GetComponent<PlayerController>().maxSpeed = 3.0f;
            Invoke("Recover", 3);
        }
    }
    void Recover()
    {
        player.GetComponent<PlayerController>().maxSpeed = 4.5f;
    }
}
