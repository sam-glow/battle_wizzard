using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_char_selection : MonoBehaviour
{
    public static player_char_selection inst;

    public int p1_char_selection = 0;
    public int p2_char_selection = 0;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this.gameObject);
    }

    public int GetSelection(int player_index)
    {
        return player_index == 0 ? p1_char_selection : p2_char_selection;
    }
}
