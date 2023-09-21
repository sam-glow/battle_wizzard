using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class callout_jiggla : MonoBehaviour
{
    public int idx = 0;

    public TextMeshPro text;

    private float vibration_timer = 0f;

    Vector2 initial_ap = Vector2.zero;
    void Start()
    {
        var logic = FindFirstObjectByType<battle_logic>();
        logic.OnCast += OnCast;
        logic.OnCelebrate += OnCelebrate;

        var flow = FindFirstObjectByType<battle_flow>();
        flow.OnCountDown += OnCountDown;
        flow.OnBattle += OnBattle;
        flow.OnVictory += OnVictory;
        flow.OnWinner += OnVictory;

        RectTransform rt = GetComponent<RectTransform>();
        initial_ap = rt.anchoredPosition;
    }

    private void OnCelebrate(int obj)
    {
        if (obj == idx)
        {
            vibration_timer = 1f;
            ShowHide(true);
            text.text = "aspersions on your prowess";
        }
    }

    void ShowHide( bool enable)
    {
        gameObject.SetActive(enable);
    }

    void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();
        vibration_timer -= Time.deltaTime;
        float vb_progress = Mathf.Pow(vibration_timer, 4);
        if (vibration_timer >= 0f)
        {
            rt.anchoredPosition = initial_ap + new Vector2(
                vb_progress * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value)),
                vb_progress * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value))
            );
        }
        else
        {
            vibration_timer = 0f;
            transform.localPosition = initial_ap;
            ShowHide(false);
        }
    }

    private void OnCast(int arg1, EButton arg2)
    {
        if (arg1 == idx)
        {
            vibration_timer = 1f;
            ShowHide(true);
            switch (arg2)
            {
                case EButton.B:
                    text.text = "Fireball!";
                    break;
                case EButton.X:
                    text.text = "Chain Lightning!";
                    break;
                case EButton.Y:
                    text.text = "Guiding Bolt!";
                    break;
            }
        }
    }

    private void OnVictory(int obj)
    {
        ShowHide(true);
    }

    private void OnBattle()
    {
        ShowHide(true);
    }

    private void OnCountDown()
    {
        ShowHide(false);
    }
}