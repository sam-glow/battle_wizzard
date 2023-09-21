using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class SpawnRandomMap : MonoBehaviour
{
    public GameObject[] maps;

    private GameObject world;

    private bool fist_setup = true;
    void Awake()
    {
        var bf = FindFirstObjectByType<battle_flow>();
        bf.OnCountDown += OnCountDown;

        int randomIndex = Random.Range(0,maps.Length);

        world = Instantiate(maps[randomIndex], transform.position, transform.rotation);
        fist_setup = true;
    }

    private void OnCountDown()
    {
        if (fist_setup)
        {
            fist_setup = false;
            return;
        }

        if(world != null)
            Destroy(world);
        int randomIndex = Random.Range(0, maps.Length);
        world = Instantiate(maps[randomIndex], transform.position, transform.rotation);
    }
}
