using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FxCode {
    None = 0,
    PlayerMove,
    PlayerJump,
    PlayerLanding,

}

public class CsFxManager : MonoBehaviour
{
    void Awake() {
        EventManager.StartListening(typeof(FxEvent), FxListener);
        
    }




    private void FxListener (IEvent param) {
        FxEvent fxEvent = (FxEvent)param;

        switch (fxEvent.fxType)
        {
            case FxCode.PlayerMove :
                break;

            case FxCode.PlayerJump :
                break;

            case FxCode.PlayerLanding :
                break;


            case FxCode.None :
            default:
                break;
        }
    }
}
