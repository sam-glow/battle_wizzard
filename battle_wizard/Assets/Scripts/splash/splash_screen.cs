using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class splash_screen : MonoBehaviour
{
    [SerializeField] private Button start_button;

    void Start()
    {
        start_button.onClick.AddListener(OnClickStartButton);
    }
    void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }
}
