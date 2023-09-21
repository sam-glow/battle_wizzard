using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class char_select : MonoBehaviour
{
    [Serializable]
    public class char_selec_references
    {
        public Image Icon;
        public TextMeshProUGUI Name;
        public List<TextMeshProUGUI> stats;
    }

    public List<char_selec_references> refs;

    public AudioClip music;

    private bool p1_locked = false;
    private bool p2_locked = false;

    void Start()
    {
        p1_locked = p2_locked = false;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.volume = 0.5f;
        source.Play();

        Refresh();
    }

    void Refresh()
    {
        var currernt_selection = FindFirstObjectByType<player_char_selection>();
        var prefabs = FindFirstObjectByType<wizzard_prefabs>();

        //update selection
        for (int i = 0; i < refs.Count; i++)
        {
            int wiz_id = i == 0 ? currernt_selection.p1_char_selection : currernt_selection.p2_char_selection;
            var wiz = prefabs.GetPrefab(wiz_id, i);

            var stats = wiz.GetComponent<wizzard_details>();
            var player = refs[i];
            player.Icon.sprite = stats.select_tex;
            player.Name.text = stats.name;

            for (int j = 0; j < player.stats.Count; j++)
            {
                player.stats[j].name = stats.stats[j];
            }
        }
    }

    void Update()
    {
        //load selection
        var currernt_selection = FindFirstObjectByType<player_char_selection>();
        var prefabs = FindFirstObjectByType<wizzard_prefabs>();

        //input
        bool is_dirty = false;
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            currernt_selection.p1_char_selection = currernt_selection.p1_char_selection == 0 ? 1 : 0;
            is_dirty = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            currernt_selection.p1_char_selection = currernt_selection.p1_char_selection == 0 ? 1 : 0;
            is_dirty = true;
        }

        //update selection
        if (is_dirty)
        {
           Refresh();
        }

        //confirm
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            p1_locked = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            p2_locked = true;
        }

        if (p1_locked && p2_locked)
            SceneManager.LoadScene(2);
    }
}
