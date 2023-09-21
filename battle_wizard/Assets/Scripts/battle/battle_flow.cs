using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battle_flow : MonoEditorDebug
{
    public event Action OnCountDown;
    public event Action OnBattle;
    public event Action<int> OnVictory;
    public event Action<int> OnWinner;

    [SerializeField] Animator m_countDownAnimator;
    [SerializeField] private int max_lives = 2;

    private float max_round_time = 30f;

    int p1_lives = 0;
    int p2_lives = 0;
    int last_victor =  0;
    private float round_time = 0f;
    public int P1_Lives => p1_lives;
    public int P2_Lives => p2_lives;
    public float RoundTime => round_time;
    public enum Phase
    {
        count_down,
        battle,
        victory,
        winner,
        none
    }

    Phase phase = Phase.none;

    [ExposeInInspector()] public Phase CurrentPhase { get { return phase; } }
    public int LastVictor => last_victor;

    void Start()
    {
        var cd = m_countDownAnimator.GetBehaviour<count_down_anim_exit>();
        cd.onExit += OnCountDownComplete;

        p1_lives = p2_lives = max_lives;
        round_time = max_round_time;

        StartCoroutine(DoNextFrame(() => { EnterState(Phase.count_down, 0); }));
    }

    IEnumerator DoNextFrame(Action action)
    {
        yield return null;
        action();
    }

    void Update()
    {
        switch (phase)
        {
            default: break;
            case Phase.count_down:
                break;
            case Phase.battle:
                round_time -= Time.deltaTime;
                if (round_time < 0f)
                {
                    var logic = GetComponent<battle_logic>();
                    OnPlayerVictory(logic.Progress > 0 ? 0 : 1);
                }
                break;
            case Phase.victory:
                break;
        }
    }

    [EditorDebugMethod(false)]
    void EnterState(Phase _phase, int _victor)
    {
        phase = _phase;

        switch (phase)
        {
            default: break;
            case Phase.count_down:
                if (OnCountDown != null)
                    OnCountDown();

                m_countDownAnimator.SetTrigger("start");
                break;
            case Phase.battle:
                round_time = max_round_time;
                if (OnBattle != null)
                    OnBattle();
                break;
            case Phase.victory:
                if (OnVictory != null)
                    OnVictory(_victor);

                StartCoroutine(RunVictoryStage(5f));
                break;
            case Phase.winner:
                if (OnWinner != null)
                    OnWinner(_victor);

                StartCoroutine(RunWinnerStage(10f));
                break;
        }
    }

    IEnumerator RunWinnerStage(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
    IEnumerator RunVictoryStage(float time)
    {
        yield return new WaitForSeconds(time);
        EnterState(Phase.count_down, 0);
    }

    void OnCountDownComplete()
    {
        EnterState(Phase.battle, 0);
    }
    public void OnPlayerVictory(int winner_idx)
    {
        if (winner_idx == 0)
        {
            last_victor = 0;
            if (--p2_lives <= 0)
            {
                EnterState(Phase.winner, 0);
            }
            else
            {
                EnterState(Phase.victory, 0);
            }
        }
        else
        {
            last_victor = 1;
            if (--p1_lives <= 0)
            {
                EnterState(Phase.winner, 1);
            }
            else
            {
                EnterState(Phase.victory, 1);
            }
        }
    }
}
