using UnityEngine;
using System.Collections;

public class player_effect
{
    public enum enumPlayerEffectType
    {
        slow,           // 물 블럭. 3초 동안 이동속도 30%감소. 얼음 미사일. 4초동안 이동속도 50% 감소 / time, 감소 %
        damage_once,    // 1회성 데미지. 가시 블럭 등    / - , 데미지
        HighJumping,    // 고공 점프. 2배 점프. 구름 블럭. / - , 
        stun,           // 일시 정지. 레이저 미사일. / time, -
        healing,        // 일시 회복. / -, 증가 값
        shield,         // 1회 방어. / -
        poison,         // 다음 스테이지에... 스킬 사용 불가(병아리 밀고 당기기, 달리기) 스테이지가 끝나고 유지되어야 함. / -
    };

    public enumPlayerEffectType effectType;
    protected float time_to_keep; //  효과의 유지시간
    protected float value;        //  적용값
    protected bool deleted = false;
    public bool bDeleteNow = false;
    protected Platformer.Mechanics.PlayerController pc;
    float waiting = 1;             //   바로 삭제되지 않고 1초 대기. 이미지 랜더링 될 수 있으니 ... 

    public player_effect(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value=0, float _time = 0)
    {
        pc = _pc;
        effectType = _type;
        time_to_keep = _time;
        value = _value;
    }

    public void Set(ref Platformer.Mechanics.PlayerController _pc, enumPlayerEffectType _type, float _value = 0, float _time = 0)
    {
        pc = _pc;
        effectType = _type;
        time_to_keep = _time;
        value = _value;

        deleted = false;
        bDeleteNow = false;
        waiting = 1;             //   바로 삭제되지 않고 1초 대기. 이미지 랜더링 될 수 있으니 ... 

    }

    virtual public void Update()
    {
        if (bDeleteNow == true)
            return;
        if (deleted == true)
        {
            waiting -= Time.deltaTime;
            if (waiting <= 0)
            {
                bDeleteNow = true;
            }
                
        }
    }
}
