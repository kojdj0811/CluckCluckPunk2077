using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEvent : MonoBehaviour
{

    void RunPrintLogEvent(IEvent param)
    {
        var _param = (PrintLogEvent)param;
        Debug.Log(_param.message);
    }


     void Awake() {
        EventManager.StartListening(typeof(PrintLogEvent), RunPrintLogEvent);
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            EventManager.TriggerEvent(new PrintLogEvent("Message!"));
        }
    }




    private void OnDestroy() {
        EventManager.StopListening(typeof(PrintLogEvent), RunPrintLogEvent);
    }
}
