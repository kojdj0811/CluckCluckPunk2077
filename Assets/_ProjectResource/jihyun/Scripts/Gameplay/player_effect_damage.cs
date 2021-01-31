using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_effect_damage : player_effect
{
    public player_effect_damage(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value = 0, float _time = 0) : base(ref _pc, _type, _value, _time) { }
    public override void Update()
    {
        base.Update();
        if (deleted == false)
        {
            pc.AddHealth(value);
            deleted = true;
        }
    }
}
