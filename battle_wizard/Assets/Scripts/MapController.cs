using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Material skyboxMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;

    }
}
