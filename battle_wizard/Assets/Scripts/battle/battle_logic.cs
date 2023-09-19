using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class battle_logic : MonoEditorDebug
{
    private bool isActive = false;

    private List<HashSet<EButton>> frame_input = new List<HashSet<EButton>>();
    private List<HashSet<EButton>> prev_frame_input = new List<HashSet<EButton>>();


    private int progress = 0;

    EButton[] last_valid_input = new EButton[2];

    EButton[] element_buttons = new EButton[3]
    {
        EButton.B,
        EButton.X,
        EButton.Y
    };
    [ExposeInInspector("Progress")] int Progress => progress;

    void Start()
    {
        var battle_flow = GetComponent<battle_flow>();
        battle_flow.OnBattle += OnBattle;

        frame_input.Add(new HashSet<EButton>());
        frame_input.Add(new HashSet<EButton>());

        progress = 0;
    }

    private void OnBattle()
    {
        isActive = true;
    }

    public void SetInput(int idx, HashSet<EButton> input)
    {
        frame_input[idx] = input;
    }

    void PreUpdate()
    {
        frame_input[0].Clear();
        frame_input[1].Clear();
    }

    //NOTE: UPDATE EXECUTES LATE
    void Update()
    {
        if (!isActive)
            return;

         //determine if we have just cast
         EButton[] spell_casts = { EButton.Count , EButton.Count};
         for (int i = 0; i < frame_input.Count; i++)
         {
             if (frame_input[i] != null)
             {
                 if (frame_input[i].Contains(EButton.A))
                 {
                     for (int j = 0; j < element_buttons.Length; j++)
                     {
                         if (last_valid_input[i] == element_buttons[j])
                         {
                             spell_casts[i] = element_buttons[j];
                             break;
                         }
                     }
                 }
                 else
                 {
                     for (int j = 0; j < element_buttons.Length; j++)
                     {
                         if (frame_input[i].Contains(element_buttons[j]))
                         {
                            last_valid_input[i] = element_buttons[j];
                            break;
                         }
                     }
                 }
             }
         }

        //did we both cast?
        bool was_clash = false;
        bool did_both_cast = spell_casts[0] != EButton.Count && spell_casts[1] != EButton.Count;

        EButton p1 = spell_casts[0] != EButton.Count ? spell_casts[0] : last_valid_input[0];
        EButton p2 = spell_casts[1] != EButton.Count ? spell_casts[1] : last_valid_input[1];

        progress += progress += DoDamage(p1, p2, did_both_cast, ref was_clash);
    }

    int DoDamage(EButton p1_spell, EButton p2_spell, bool both_cast, ref bool is_clash)
    {
        float mult = 1.0f;
        if (both_cast)
        {
            //clash!
            if (p1_spell == p2_spell)
            {
                is_clash = true;
                return 0;
            }

            mult = 2.5f;
        }

        //first cast protection
        if (p1_spell == EButton.Count) p1_spell = (EButton)(((int)p2_spell + 2) % 3);
        if (p2_spell == EButton.Count) p2_spell = (EButton)(((int)p1_spell + 2) % 3);

        int diff = (p1_spell - p2_spell + 3) % 3;

        if (diff == 0)
        {
            //attacker whiffs?
            return 0;
        }
        else if (diff == 1)
        {
            //p2 does damage
            return -5;
        }
        else if (diff == 2)
        {
            //p1 does damage
            return 5;
        }

        return 0;
    }
}