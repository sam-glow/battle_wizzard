using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_flow : MonoEditorDebug
{
    [SerializeField] Animator m_countDownAnimator;
    enum Phase
    {
        count_down,
        battle,
        victory,
        none
    }

    Phase phase = Phase.none;

    [ExposeInInspector()] Phase CurrentPhase { get { return phase; } }

    void Start()
    {
        EnterState(Phase.count_down);

        var cd = m_countDownAnimator.GetBehaviour<count_down_anim_exit>();
        cd.onExit += OnCountDownComplete;

    }

    void Update()
    {
        switch (phase)
        {
            default: break;
            case Phase.count_down:
                DoCountDown();
                break;
            case Phase.battle:
                break;
            case Phase.victory:
                break;
        }
    }

    void EnterState(Phase _phase)
    {
        switch (_phase)
        {
            default: break;
            case Phase.count_down:
                EnterCountDown();
                break;
            case Phase.battle:
                break;
            case Phase.victory:
                break;
        }
    }

    private void EnterCountDown()
    {
        m_countDownAnimator.SetTrigger("start");
    }

    void DoCountDown()
    {
        
    }

    void OnCountDownComplete()
    {
        phase = Phase.battle;
    }
}
