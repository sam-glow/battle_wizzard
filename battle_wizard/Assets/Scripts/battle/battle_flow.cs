using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_flow : MonoBehaviour
{
    [SerializeField] Animator m_countDownAnimator;
    enum Phase
    {
        count_down,
        battle,
        victory,
        none
    }

    Phase phase;

    void Start()
    {
        phase = Phase.count_down;
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

    void DoCountDown()
    {
        m_countDownAnimator.StartPlayback();
        m_countDownAnimator.SetBool("start", true);
    }
}
