using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizzard_input : MonoEditorDebug
{
    [SerializeField] private int player_index;
    [SerializeField] private wizzard_visual visual;

    private bool isActive = false;

    [ExposeInInspector] bool IsActive => isActive;

    void Start()
    {
        var battle_flow = GetComponent<battle_flow>();
        battle_flow.OnBattle += OnBattle;
        battle_flow.OnCountDown+= OnCountDown;
        battle_flow.OnVictory += OnVictory;
    }

    void OnBattle()
    {
        isActive = true;
    }

    void OnCountDown()
    {
        isActive = false;
    }

    void OnVictory()
    {
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {

        }
    }
}
