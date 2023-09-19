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
    [SerializeField] private int max_lives = 2;

    int p1_lives = 0;
    int p2_lives = 0;

    enum Phase
    {
        count_down,
        battle,
        victory,
        winner,
        none
    }

    Phase phase = Phase.none;

    [ExposeInInspector()] Phase CurrentPhase { get { return phase; } }

    void Start()
    {
        EnterState(Phase.count_down);

        var cd = m_countDownAnimator.GetBehaviour<count_down_anim_exit>();
        cd.onExit += OnCountDownComplete;

        p1_lives = p2_lives = max_lives;
    }

    void Update()
    {
        switch (phase)
        {
            default: break;
            case Phase.count_down:
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
                EnterVictory();
                break;
        }
    }

    private void EnterVictory()
    {
        if(OnVictory != null)
            OnVictory();
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

    void OnCountDownComplete()
    {
        EnterState(Phase.battle);
    }
    public void OnPlayerVictory(int winner_idx)
    {
        EnterState(Phase.victory);

        //start some kind of coroutine?

        if (winner_idx == 0)
        {
            if (--p2_lives <= 0)
            {
                EnterState(Phase.winner);
            }
        }
        else
        {
            if (--p1_lives <= 0)
            {
                EnterState(Phase.winner);
            }
        }
    }
}
