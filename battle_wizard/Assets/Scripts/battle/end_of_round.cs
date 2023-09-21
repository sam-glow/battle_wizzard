using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class end_of_round : MonoBehaviour
{
    public TextMeshPro player_text;
    public TextMeshPro victory_text;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;
        bf.OnVictory += OnVictory;
        bf.OnWinner += OnWinner;

        gameObject.SetActive(false);
    }

    private void OnWinner(int idx)
    {
        gameObject.SetActive(true);
        player_text.text = idx == 0 ? "P1" : "P2";
        victory_text.text = idx == 0 ? "is dancing on P2's wizardly grave" : "is dancing on P1's wizardly grave";
    }

    private void OnVictory(int idx)
    {
        gameObject.SetActive(true);
        player_text.text = idx == 0 ? "P1" : "P2";
        victory_text.text = idx == 0 ? "casts fireball on P2's attempts" : "casts magic missile on P1's attempts";
    }

    private void OnCountDown()
    {
        gameObject.SetActive(false);
    }
}
