using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizzard_prefabs : MonoBehaviour
{
    public GameObject prefab_wizzard_01_left;
    public GameObject prefab_wizzard_01_right;

    public GameObject prefab_wizzard_02_left;
    public GameObject prefab_wizzard_02_right;

    public GameObject prefab_wizzard_03_left;
    public GameObject prefab_wizzard_03_right;

    public GameObject prefab_wizzard_04_left;
    public GameObject prefab_wizzard_04_right;

    public GameObject prefab_wizzard_05_left;
    public GameObject prefab_wizzard_05_right;

    public GameObject prefab_wizzard_06_left;
    public GameObject prefab_wizzard_06_right;

    public GameObject GetPrefab(int char_idx, int player_idx)
    {
        if (char_idx == 0)
            return player_idx == 0 ? prefab_wizzard_01_left : prefab_wizzard_01_right;

        if(char_idx == 1)
            return player_idx == 0 ? prefab_wizzard_02_left : prefab_wizzard_02_right;

        if (char_idx == 2)
            return player_idx == 0 ? prefab_wizzard_03_left : prefab_wizzard_03_right;

        if (char_idx == 3)
            return player_idx == 0 ? prefab_wizzard_04_left : prefab_wizzard_04_right;

        if (char_idx == 4)
            return player_idx == 0 ? prefab_wizzard_05_left : prefab_wizzard_05_right;

        if (char_idx == 5)
            return player_idx == 0 ? prefab_wizzard_06_left : prefab_wizzard_06_right;

        return player_idx == 0 ? prefab_wizzard_01_left : prefab_wizzard_01_right;
    }
}
