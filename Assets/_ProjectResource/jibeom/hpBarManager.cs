using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBarManager : MonoBehaviour
{
    public GameObject player;
    Health health;
    int index, flags = 0;
    public List<Image> HPbar;
    public List<Sprite> img;

    void Update()
    {
        health = player.GetComponent<Health>();

        for (int i = 0; i < 5; i++)
        {
            HPbar[i].sprite = img[0];
        }

        for (int i = 0; i < 10 - (int)(health.currentHP * 2); i++)
        {
            if(flags == 0)
            {
                HPbar[index].sprite = img[1];
                flags++;
            }
            else
            {
                HPbar[index].sprite = img[2];
                flags = 0;
                index++;
            }
        }
        flags = index = 0;
    }
}
