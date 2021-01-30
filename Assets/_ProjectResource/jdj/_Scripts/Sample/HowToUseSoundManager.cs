using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToUseSoundManager : MonoBehaviour
{

    void Update()
    {
        //fx sound
        if(Input.GetKeyDown(KeyCode.Space)) {
            EventManager.TriggerEvent(new SoundEvent(SoundType.Fx, FxSoundCode.Chicken_Run_Sound, transform.position));
        }




        //bgm sound
        if(Input.GetKeyDown(KeyCode.LeftBracket)) {
            EventManager.TriggerEvent(new SoundEvent(SoundType.Bgm, BgmSoundCode.Title_Bgm));
        }
        if(Input.GetKeyDown(KeyCode.RightBracket)) {
            EventManager.TriggerEvent(new SoundEvent(SoundType.Bgm, BgmSoundCode.Ingame_Bgm));
        }
        if(Input.GetKeyDown(KeyCode.Return)) {
            EventManager.TriggerEvent(new SoundEvent(SoundType.Bgm, BgmSoundCode.None));
        }
    }
}
