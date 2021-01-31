using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_effect_shield : player_effect
{
    public player_effect_shield(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value = 0, float _time = 0) : base(ref _pc, _type, _value, _time) { }
    public void Defenced()  // 한 번 막으면 호출해주면 됨.
    {
        deleted = true;
    }
}

