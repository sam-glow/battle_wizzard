using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructions_scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenGo());
    }

    IEnumerator WaitThenGo()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(3);
    }
}
