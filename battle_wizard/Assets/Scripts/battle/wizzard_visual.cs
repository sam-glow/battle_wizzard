using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class wizzard_visual : MonoBehaviour
{
    [SerializeField] GameObject deathvfx;
    [SerializeField] GameObject wand_vfx;
    [SerializeField] private GameObject red_spell_prefab;
    [SerializeField] private GameObject blue_spell_prefab;
    [SerializeField] private GameObject yellow_spell_prefab;

    [SerializeField] private List<GameObject> go_to_move_to_wand;

    [SerializeField] private int idx;
    private float damage_time = 0.2f;
    private float celebrate_time = 0.2f;

    private GameObject death_vfx_instance;
    private GameObject cast_vfx_instance;
    private wizzard_sprite sprites;

    private float vibration_timer = 0f;
    private Vector3 initial_pos;
    public int Index => idx;

    public enum state
    {
        aggresive,
        almost_dead, 
        slmost_win,
        hit,
        celebrate,
        dead,
        relax,
        striggle
    }

    private float override_time = 0f;
    private state _state;
    private state prev_base_state;
    private state base_state;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;
        bf.OnBattle+= OnBattle;
        bf.OnVictory+= OnVictory;
        bf.OnWinner += OnWinner;

        var bl = FindFirstObjectByType<battle_logic>();
        bl.OnCast += OnCast;
        bl.OnDamage += OnDamage;
        bl.OnCelebrate += OnCelebrate;

        var player_selection = FindFirstObjectByType<player_char_selection>();
        var prefabs = FindFirstObjectByType<wizzard_prefabs>();

        int selection = player_selection.GetSelection(idx);
        GameObject prefab = prefabs.GetPrefab(selection, idx);

        SetWandVfx(false);

        var go = Instantiate(prefab, transform, false);
        sprites = go.GetComponent<wizzard_sprite>();

        prev_base_state = base_state = _state = state.relax; 
        sprites.SetState(_state);
        override_time = 0f;
        initial_pos = gameObject.transform.position;
    }

    void OnCelebrate(int _v)
    {
        if (_v != idx)
            return;

        override_time = celebrate_time;
        _state = state.celebrate;
        sprites.SetState(_state);
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

        if (button == EButton.B)
            cast_vfx_instance = Instantiate(red_spell_prefab, transform, false);
        if (button == EButton.X)
            cast_vfx_instance = Instantiate(blue_spell_prefab, transform, false);
        if (button == EButton.Y)
            cast_vfx_instance = Instantiate(yellow_spell_prefab, transform, false);

        if (idx != 0)
        {
            cast_vfx_instance.transform.rotation = Quaternion.Euler(Vector3.up * 90f);
        }

        vibration_timer = 1f;

        _state = state.aggresive;
        override_time = 0.15f;
        sprites.SetState(_state);
    }
    void OnDamage(int _v)
    {
        if (_v != idx)
            return;

        vibration_timer = 1.0f;
        _state = state.hit;
        sprites.SetState(_state);
        override_time = damage_time;
    }

    void Update()
    {
        //get our base state
        var flow = FindFirstObjectByType<battle_flow>();
        var logic = FindFirstObjectByType<battle_logic>();

        //VIBRATION
        vibration_timer -= Time.deltaTime;
        float vb_progress = Mathf.Pow(vibration_timer, 4);
        if (vibration_timer >= 0f)
        {
            float alpha = UnityEngine.Random.value;
            sprites.transform.localPosition = new Vector3(
                vb_progress * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value)),
                vb_progress * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value)),
                0f
            );
        }
        else
        {
            vibration_timer = 0f;
            sprites.transform.localPosition = Vector3.zero;
        }

        //wand update
        var lines = GetComponentsInChildren<LineRendererTwoPoints>();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].pos1 = sprites.Wand().transform;
            lines[i].pos2 = logic.vfx_point.transform;
        }

        var particles_to_target = GetComponentsInChildren<ParticlesToTarget>();
        for (int i = 0; i < particles_to_target.Length; i++)
        {
            particles_to_target[i].Target = logic.vfx_point.transform;
        }

        for (int i = 0; i < go_to_move_to_wand.Count; i++)
        {
            var gb = go_to_move_to_wand[i];
            gb.transform.position = sprites.Wand().transform.position;
        }

        //state stuff here]
        override_time -= Time.deltaTime;
        if (override_time < 0f)
            override_time = 0f;

        state new_state = base_state;
        switch (flow.CurrentPhase)
        {
            case battle_flow.Phase.count_down:
                new_state = state.relax;
                break;
            case battle_flow.Phase.battle:
                float progress = idx == 0 ? logic.visualP : 1f - logic.visualP;
                if (progress > 0.7f)
                    new_state = state.slmost_win;
                else if (progress > 0.4f)
                    new_state = state.relax;
                else if (progress > 0.25f)
                    new_state = state.striggle;
                else
                    new_state = state.almost_dead;
                break;
            case battle_flow.Phase.victory:
                new_state = flow.LastVictor == idx ? state.relax : state.dead;
                break;
            case battle_flow.Phase.winner:
                new_state = flow.LastVictor == idx ? state.relax : state.dead;
                break;
            case battle_flow.Phase.none:
                break;
        }

        if (new_state != _state)
        {
            if (override_time > 0f)
            {
                if (new_state != prev_base_state)
                {
                    _state = base_state = new_state;
                    sprites.SetState(_state);
                }
            }
            else
            {
                _state = base_state = new_state;
                sprites.SetState(_state);
            }
        }

        prev_base_state = base_state = new_state;
    }
}