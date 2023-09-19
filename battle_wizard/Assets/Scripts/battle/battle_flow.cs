using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_flow : MonoEditorDebug
{
    public event Action OnCountDown;
    public event Action OnBattle;
    public event Action OnVictory;
    public event Action OnWait;


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

    [EditorDebugMethod(false)]
    void EnterState(Phase _phase)
    {
        phase = _phase;

        switch (phase)
        {
            default: break;
            case Phase.count_down:
                EnterCountDown();
                break;
            case Phase.battle:
                EnterBattle();
                break;
            case Phase.victory:
                break;
        }
    }

    private void EnterBattle()
    {
        if (OnBattle != null)
            OnBattle();
    }

    private void EnterCountDown()
    {
        if (OnCountDown != null)
            OnCountDown();
        m_countDownAnimator.SetTrigger("start");
    }

    void DoCountDown()
    {
        
    }

    void OnCountDownComplete()
    {
        EnterState(Phase.battle);
    }
}
