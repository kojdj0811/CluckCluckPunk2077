using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToUseFxManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            EventManager.TriggerEvent(new FxEvent(FxCode.Effect_Missile_Basic1, transform.position, transform.rotation));
        }


        if(Input.GetKeyDown(KeyCode.W)) {
            EventManager.TriggerEvent(new FxEvent(FxCode.Effect_Missile_Basic3, transform.position, transform.rotation));
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            EventManager.TriggerEvent(new FxEvent(FxCode.Effect_Missile_Lazer1, transform.position, transform.rotation));
        }
    }
}
