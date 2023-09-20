using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class wizzard_visual : MonoBehaviour
{
    [SerializeField] GameObject deathvfx;
    [SerializeField] GameObject wand_vfx;
    [SerializeField] private int idx;

    private GameObject death_vfx_instance;

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

        if (idx != _v)
        {
            death_vfx_instance = Instantiate(deathvfx, transform, false);
        }
        else
        {
            //winner
        }
    }

    void OnCountDown()
    {
        Destroy(death_vfx_instance);
        SetWandVfx(false);
    }

    void OnBattle()
    {
        SetWandVfx(true);
    }

    void OnVictory(int _v)
    {
        SetWandVfx(false);

        if (idx != _v)
        {
            death_vfx_instance = Instantiate(deathvfx, transform, false);
        }
    }

    void SetWandVfx(bool enable)
    {
        wand_vfx.SetActive(enable);
    }
}
