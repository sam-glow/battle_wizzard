using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class wizzard_visual : MonoBehaviour
{
    [SerializeField] GameObject deathvfx;
    [SerializeField] GameObject wand_vfx;
    [SerializeField] private GameObject red_spell_prefab;
    [SerializeField] private GameObject blue_spell_prefab;
    [SerializeField] private GameObject yellow_spell_prefab;

    [SerializeField] private int idx;

    private GameObject death_vfx_instance;
    private GameObject cast_vfx_instance;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;
        bf.OnBattle+= OnBattle;
        bf.OnVictory+= OnVictory;
        bf.OnWinner += OnWinner;

        var bl = FindFirstObjectByType<battle_logic>();
        bl.OnCast += OnCast;


        var player_selection = FindFirstObjectByType<player_char_selection>();
        var prefabs = FindFirstObjectByType<wizzard_prefabs>();

        int selection = player_selection.GetSelection(idx);
        GameObject prefab = prefabs.GetPrefab(selection, idx);

        Instantiate(prefab, transform, false);
        SetWandVfx(false);
    }

    void OnWinner(int _v)
    {
        DestroyImmediate(cast_vfx_instance);
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
        DestroyImmediate(cast_vfx_instance);
        Destroy(death_vfx_instance);
        SetWandVfx(false);
    }

    void OnBattle()
    {
        DestroyImmediate(cast_vfx_instance);
        SetWandVfx(true);
    }

    void OnVictory(int _v)
    {
        DestroyImmediate(cast_vfx_instance);
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

    void OnCast(int _V, EButton button)
    {
        if (idx != _V)
            return;

        DestroyImmediate(cast_vfx_instance);

        if (button == EButton.B)
            cast_vfx_instance = Instantiate(red_spell_prefab, transform, false);
        if (button == EButton.X)
            cast_vfx_instance = Instantiate(blue_spell_prefab, transform, false);
        if (button == EButton.Y)
            cast_vfx_instance = Instantiate(yellow_spell_prefab, transform, false);
    }
}
