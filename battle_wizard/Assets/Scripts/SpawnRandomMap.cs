using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnRandomMap : MonoBehaviour
{
    public GameObject[] maps;

    // Start is called before the first frame update
    void Awake()
    {
        int randomIndex = Random.Range(0,maps.Length);
        Instantiate(maps[randomIndex], transform.position, transform.rotation);
    }
}
