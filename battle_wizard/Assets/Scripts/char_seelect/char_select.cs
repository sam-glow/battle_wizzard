using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class char_select : MonoBehaviour
{
    [SerializeField] private Button start_button;

    void Start()
    {
        start_button.onClick.AddListener(OnClickStartButton);
    }
    void OnClickStartButton()
    {
        SceneManager.LoadScene(2);
    }
}
