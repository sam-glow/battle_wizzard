using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class wizzard_ui : MonoBehaviour
{
    public TextMeshPro name;
    public MeshRenderer icon;
    public List<GameObject> Wands;

    public int idx = 0;

    bool is_initialised = false;

    void Start()
    {
        var bf = FindFirstObjectByType<battle_flow>();
    }
    void Update()
    {
        if (!is_initialised)
        {
            var wizs = FindObjectsOfType<wizzard_visual>();
            for (int i = 0; i < wizs.Length; i++)
            {
                var wv = wizs[i];
                if (wv.Index == idx)
                {
                    var details = wv.gameObject.GetComponentInChildren<wizzard_details>();
                    name.text = details.name;
                    icon.material.mainTexture = details.tex;
                }
            }

            is_initialised = true;
        }


        var bf = FindFirstObjectByType<battle_flow>();
        int lives = idx == 0 ? bf.P1_Lives : bf.P2_Lives;

        for (int i = 0; i < Wands.Count; i++)
        {
            bool enabled = i == lives;
            if(Wands[i].activeSelf != enabled)
                Wands[i].SetActive(enabled);
        }
    }
}
