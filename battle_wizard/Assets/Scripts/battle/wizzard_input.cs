using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum EButton
{
    A, B,
    X, Y,
    Up, Down,
    Left, Right,
    L1, L2, 
    R1, R2,
    Count
}

public class wizzard_input : MonoEditorDebug
{
    [SerializeField] private int player_index;
    [SerializeField] private wizzard_visual visual;

    private Dictionary<KeyCode, EButton> keyButtonMap;

    private bool isActive = false;

    [ExposeInInspector] bool IsActive => isActive;

    void Start()
    {
        var battle_flow = GetComponent<battle_flow>();
        battle_flow.OnBattle += OnBattle;
        battle_flow.OnCountDown+= OnCountDown;
        battle_flow.OnVictory += OnVictory;

        keyButtonMap = new Dictionary<KeyCode, EButton>()
        { 
            {player_index == 0 ? KeyCode.Joystick1Button14 : KeyCode.Joystick2Button14, EButton.A},
            {player_index == 0 ? KeyCode.Joystick1Button13 : KeyCode.Joystick2Button13, EButton.B},
            {player_index == 0 ? KeyCode.Joystick1Button15 : KeyCode.Joystick2Button15, EButton.X},
            {player_index == 0 ? KeyCode.Joystick1Button12 : KeyCode.Joystick2Button12, EButton.Y},

            {player_index == 0 ? KeyCode.Joystick1Button4 : KeyCode.Joystick2Button4, EButton.Up},
            {player_index == 0 ? KeyCode.Joystick1Button5 : KeyCode.Joystick2Button5, EButton.Down},
            {player_index == 0 ? KeyCode.Joystick1Button6 : KeyCode.Joystick2Button6, EButton.Left},
            {player_index == 0 ? KeyCode.Joystick1Button7 : KeyCode.Joystick2Button7, EButton.Right},

            {player_index == 0 ? KeyCode.Joystick1Button8 : KeyCode.Joystick2Button8, EButton.L1},
            {player_index == 0 ? KeyCode.Joystick1Button9 : KeyCode.Joystick2Button9, EButton.L2},

            {player_index == 0 ? KeyCode.Joystick1Button10 : KeyCode.Joystick2Button10, EButton.R1},
            {player_index == 0 ? KeyCode.Joystick1Button11 : KeyCode.Joystick2Button11, EButton.R2},
        };
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
            HashSet<EButton> active_buttons = new HashSet<EButton>();
            foreach (var kvp in keyButtonMap)
                if (Input.GetKeyDown(kvp.Key))
                    active_buttons.Add(kvp.Value);

            var logic = GetComponent<battle_logic>();
            logic.SetInput(player_index, active_buttons);
        }
    }
}