using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int type;
    public GameObject player;

    public void OnClick() {
        if(type == 1 && player.GetComponent<PlayerController>().money >= 190)
        {
            player.GetComponent<PlayerController>().AddHealth(1);
            player.GetComponent<PlayerController>().money -= 190;
        }
        else if(type == 2 && player.GetComponent<PlayerController>().money >= 10)
        {
            player.GetComponent<PlayerController>().bPoison = true;
            player.GetComponent<PlayerController>().AddHealth(-0.5f);
            player.GetComponent<PlayerController>().money -= 10;
        }
        else if(type == 3 && player.GetComponent<PlayerController>().money >= 100)
        {
            player.GetComponent<PlayerController>().isShield = true;
            player.GetComponent<PlayerController>().money -= 100;
        }
    }
}
