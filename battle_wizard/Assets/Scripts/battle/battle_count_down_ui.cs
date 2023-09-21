using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class battle_count_down_ui : MonoBehaviour
{
    public TextMeshPro text;

    // Update is called once per frame
    void Update()
    {
        var flow = FindFirstObjectByType<battle_flow>();

        int seconds_left = Mathf.Max(0, Mathf.FloorToInt(flow.RoundTime));
        text.text = seconds_left.ToString();

        if(flow.CurrentPhase == battle_flow.Phase.battle)
            transform.localScale = Vector3.one * (1f + .3f * ((Mathf.Sin(Time.time * 2f * Mathf.PI * 2f) + 1f) * 0.5f));
    }
}
