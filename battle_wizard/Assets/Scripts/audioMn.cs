using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioMn : MonoBehaviour
{
    public AudioClip yellow_spell;
    public AudioClip blue_spell;
    public AudioClip red_spell;
    public AudioClip DeathClip;
    public AudioClip CelebrateClip;

    public AudioClip countdownMusic;
    public AudioClip battleMusic;
    public AudioClip victoryMusic;
    public AudioClip WinnerMusic;

    private AudioSource music;
    private AudioSource cast_0;
    private AudioSource cast_1;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;
        bf.OnVictory += OnVictory;
        bf.OnWinner += OnWinner;
        bf.OnBattle += OnBattle;

        var logic = FindFirstObjectByType<battle_logic>();
        logic.OnCast += OnCast;
        logic.OnCelebrate += OnCelebrate;

        music = gameObject.AddComponent<AudioSource>();
        cast_0 = gameObject.AddComponent<AudioSource>();
        cast_1 = gameObject.AddComponent<AudioSource>();
    }

    private void OnCelebrate(int obj)
    {
        cast_0.clip = CelebrateClip;
        cast_0.Play();
    }

    private void OnCast(int arg1, EButton arg2)
    {
        AudioSource sauce = arg1 == 0 ? cast_0 : cast_1;

        switch (arg2)
        {
            case EButton.B:
                sauce.clip = red_spell;
                break;
            case EButton.X:
                sauce.clip = blue_spell;
                break;
            case EButton.Y:
                sauce.clip = yellow_spell;
                break;
        }

        sauce.Play();
    }

    private void OnBattle()
    {
        music.clip = battleMusic;
        music.Play();
    }

    private void OnWinner(int obj)
    {
        music.clip = WinnerMusic;
        music.Play();
            
        var dc = gameObject.AddComponent<AudioSource>();
        dc.clip = DeathClip;
        dc.Play();
    }

    private void OnVictory(int obj)
    {
        music.clip = victoryMusic;
        music.Play();
    }

    private void OnCountDown()
    {
        music.clip = countdownMusic;
        music.Play();
    }
}


