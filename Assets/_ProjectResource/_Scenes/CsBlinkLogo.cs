using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsBlinkLogo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;


    private void Awake() {
        SceneManager.LoadScene("Scene_00_Manager", LoadSceneMode.Additive);
    }

    private void Start() {
        EventManager.TriggerEvent(new SoundEvent(SoundType.Bgm, BgmSoundCode.Title_Bgm));        
    }

    private void OnMouseDown() {
        SceneManager.LoadScene("stage", LoadSceneMode.Single);
        SceneManager.LoadScene("Scene_01_2dLight", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene_00_Manager", LoadSceneMode.Additive);
    }

    

}
