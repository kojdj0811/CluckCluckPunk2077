using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    Bgm = 0,
    Fx
}

public enum BgmSoundCode
{
    None = -1,
    Title_Bgm,
    Ingame_Bgm,
    Stage_Bgm,
    Shop_Bgm,
    Boss_Stage_Bgm,
    Ending_Bgm,
}

public enum FxSoundCode
{
    None = -1,
    Chicken_Run_Sound,
    Chick_Sound,
    Jump_Sound,
    Skill_Sound,
    Player_Shot,
    Chick_Shot,
    Lazer_Shot,
    Portal_Sound,
    Item_Sound,
    Chicken_Sound,
    Chick_Happy_Sound,
}

public class CsSoundManager : MonoBehaviour
{
    private static CsSoundManager S;



    public static  BgmSoundCode currentBgm;
    public static  BgmSoundCode previousBgm;
    public static AudioSource bgmSpeaker;



    [SerializeField]
    private List<AudioClip> bgmSounds;
    [SerializeField]
    private List<AudioClip> fxSounds;



    private void Awake() {
        if(S != null) {
            DestroyImmediate(gameObject);
            return;
        }

        S = this;

        currentBgm = previousBgm = BgmSoundCode.None;



        Camera mainCamera = Camera.main;

        bgmSpeaker = mainCamera.GetComponent<AudioSource>();
        if(bgmSpeaker == null)
            bgmSpeaker = mainCamera.gameObject.AddComponent<AudioSource>();


        EventManager.StartListening(typeof(SoundEvent), SoundListener);
    }


    private void OnDestroy() {
        EventManager.StopListening(typeof(SoundEvent), SoundListener);
    }




    private void SoundListener (IEvent param) {
        SoundEvent soundEvent = (SoundEvent)param;

        if(soundEvent.soundType == SoundType.Bgm) {
            StopAndPlayBgmSound(soundEvent.bgmSoundCode);
        } else if(soundEvent.soundType == SoundType.Fx) {
            PlayFxSound(soundEvent.fxSoundCode, soundEvent.fxSoundWorldPosition);
        }
    }


    private void StopAndPlayBgmSound (BgmSoundCode bgmSoundCode) {
        if((int)bgmSoundCode >= bgmSounds.Count || bgmSounds[(int)bgmSoundCode] == null)
            return;



        previousBgm = currentBgm;

        bgmSpeaker.Stop();

        if(bgmSoundCode == BgmSoundCode.None) {
            bgmSpeaker.clip = null;
        } else {
            bgmSpeaker.clip = bgmSounds[(int)bgmSoundCode];
            bgmSpeaker.Play();
        }

        currentBgm = bgmSoundCode;
    }


    private void PlayFxSound (FxSoundCode fxSoundCode, Vector3 worldPosition) {
        if((int)fxSoundCode >= fxSounds.Count || fxSounds[(int)fxSoundCode] == null)
            return;


        if(fxSoundCode != FxSoundCode.None) {
            GameObject goFxSpeaker = new GameObject("fxSpeaker");
            AudioSource asFxSpeaker = goFxSpeaker.AddComponent<AudioSource>();

            goFxSpeaker.transform.SetParent(transform);
            goFxSpeaker.transform.position = worldPosition;

            asFxSpeaker.clip = fxSounds[(int)fxSoundCode];
            asFxSpeaker.spatialBlend = 0.5f;
            asFxSpeaker.Play();

            Destroy(goFxSpeaker, asFxSpeaker.clip.length);
        }
    }
}
