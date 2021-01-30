using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_effect_poison : player_effect
{
    public player_effect_poison(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value = 0, float _time = 0) : base(ref _pc, _type, _value, _time) { }
    // 획득 후 스테이지가 시작되면 그때 발동되고 스테이지 끝날 때 삭제된다.
    bool bAdaptived = false;

    public void StartStage()
    {
        if(bAdaptived == false)
        {
            pc.bPoison = true;
        }
    }
    public void EndStage()
    {
        if(bAdaptived == true)
        {
            pc.bPoison = false;
            deleted = true;
        }
    }
}
