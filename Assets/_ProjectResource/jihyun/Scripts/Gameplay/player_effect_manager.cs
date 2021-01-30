using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_effect_manager : MonoBehaviour
{
    Dictionary<player_effect.enumPlayerEffectType, player_effect> peMap;
    Platformer.Mechanics.PlayerController pc;

    private void Awake()
    {
        pc = gameObject.GetComponent<Platformer.Mechanics.PlayerController>();
        peMap = new Dictionary<player_effect.enumPlayerEffectType, player_effect>();
    }

    public void add(player_effect.enumPlayerEffectType _type, float _value =0, float _time =0)
    {
        if (peMap.ContainsKey(_type))
        {
            peMap[_type].Set(ref pc, _type, _value, _time);
            return;
        }

        if (_type == player_effect.enumPlayerEffectType.damage_once)
        {
            peMap.Add(_type, new player_effect_damage(ref pc, _type, _value, _time));
        }else if (_type == player_effect.enumPlayerEffectType.healing)
        {
            peMap.Add(_type, new player_effect_healing(ref pc, _type, _value, _time));
        }
        else if (_type == player_effect.enumPlayerEffectType.HighJumping)
        {
            peMap.Add(_type, new player_effect_highjumping(ref pc, _type, _value, _time));
        }
        else if (_type == player_effect.enumPlayerEffectType.poison)
        {
            peMap.Add(_type, new player_effect_poison(ref pc, _type, _value, _time));
        }
        else if (_type == player_effect.enumPlayerEffectType.shield)
        {
            peMap.Add(_type, new player_effect_shield(ref pc, _type, _value, _time));
        }
        else if (_type == player_effect.enumPlayerEffectType.slow)
        {
            peMap.Add(_type, new player_effect_slow(ref pc, _type, _value, _time));
        }
        else if (_type == player_effect.enumPlayerEffectType.stun)
        {
            peMap.Add(_type, new player_effect_stun(ref pc, _type, _value, _time));
        }

    }

    void Update()
    {
        foreach(KeyValuePair<player_effect.enumPlayerEffectType, player_effect> ele in peMap)
        {
            ele.Value.Update();
        }

        //  한번 등록된 이펙트는 삭제 안하겠습니다. 삭제/추가 비용이 더 크니까
        //  그냥 등록된 후 갱신되게할께요.
    }
}
