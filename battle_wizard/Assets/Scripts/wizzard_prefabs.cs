using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizzard_prefabs : MonoBehaviour
{
    public GameObject prefab_wizzard_01_left;
    public GameObject prefab_wizzard_01_right;

    public GameObject GetPrefab(int char_idx, int player_idx)
    {
        if (char_idx == 0)
        {
            return player_idx == 0 ? prefab_wizzard_01_left : prefab_wizzard_01_right;
        }

        return null;
    }
}
