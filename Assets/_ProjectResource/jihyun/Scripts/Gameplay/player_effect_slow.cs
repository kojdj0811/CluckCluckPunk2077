﻿using UnityEngine;
using System.Collections;

public class player_effect_slow : player_effect
{
    public player_effect_slow(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value = 0, float _time = 0) : base(ref _pc, _type, _value, _time) { }

    // Update is called once per frame
    float dtime = 0;
    float original_speed = 0;
    public override void Update()
    {
        base.Update();

        if (deleted == true)
            return;
        if (original_speed == 0)
            original_speed = pc.maxSpeed;
        dtime += Time.deltaTime;
        if(dtime >= this.time_to_keep)
        {
            pc.maxSpeed = original_speed;
            deleted = true;
        }
        else
        {
            pc.maxSpeed = original_speed * this.value / 100;
        }
    }
}
