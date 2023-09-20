using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class wizzard_visual : MonoBehaviour
{
    [SerializeField] GameObject deathvfx;
    [SerializeField] GameObject wand_vfx;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;
        bf.OnBattle+= OnBattle;
        bf.OnVictory+= OnVictory;
        bf.OnWinner += OnWinner;
    }

    void OnWinner(int _v)
    {
        SetWandVfx(false);
    }

    void OnCountDown()
    {
        SetWandVfx(false);
    }

    void OnBattle()
    {
        SetWandVfx(true);
    }

    void OnVictory(int _v)
    {
        SetWandVfx(false);
    }

    void SetWandVfx(bool enable)
    {
        wand_vfx.SetActive(enable);
    }
}
