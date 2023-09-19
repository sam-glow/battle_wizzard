using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class battle_logic : MonoEditorDebug
{
    private bool isActive = false;

    private List<HashSet<EButton>> frame_input = new List<HashSet<EButton>>();



    void Start()
    {
        var battle_flow = GetComponent<battle_flow>();
        battle_flow.OnBattle += OnBattle;

        frame_input.Add(null);
        frame_input.Add(null);
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

        
    }
}
