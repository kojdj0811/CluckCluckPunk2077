using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FxCode {
    None = -1,
    Effect_Missile_Basic1,
    Effect_Missile_Basic3,
    Effect_Missile_Ice3,
    Effect_Missile_Lazer1,
}

public class CsFxManager : MonoBehaviour
{
    private static CsFxManager S;


    [SerializeField]
    private List<GameObject> fxs;


    void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }


        EventManager.StartListening(typeof(FxEvent), FxListener);
    }

    private void OnDestroy() {
        EventManager.StopListening(typeof(FxEvent), FxListener);
    }




    private void FxListener (IEvent param) {
        FxEvent fxEvent = (FxEvent)param;
        GameObject go = Instantiate(fxs[(int)fxEvent.fxType], fxEvent.fxWorldPosition, fxEvent.fxWorldRotation, transform);
    }
}
