using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class splash_screen : MonoBehaviour
{
    [SerializeField] private RectTransform scroll_text;
    [SerializeField] private RectTransform button;

    [SerializeField] private float scroll_speed = 30f;
    [SerializeField] private AudioClip audio;

    private Vector2 initial_pos;

    private Vector2 button_ap;

    private float app_time = 0f;

    private float vibration_timer = 0f;

    void Start()
    {
        AudioSource ass = gameObject.AddComponent<AudioSource>();
        ass.clip = audio;
        ass.volume = 0.5f;
        ass.loop = true;
        ass.Play();
        app_time = 0f;

        button_ap = button.anchoredPosition;
    }
    void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        scroll_text.anchoredPosition += Vector2.up * Time.deltaTime * scroll_speed;

        app_time += Time.deltaTime;
        if (app_time > 5f)
        {
            button.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.JoystickButton0))
                SceneManager.LoadScene(1);

            vibration_timer -= Time.deltaTime;
            float vb_progress = Mathf.Pow(vibration_timer, 4);
            if (vibration_timer > 0f)
            {
                button.anchoredPosition = button_ap + new Vector2(
                    vb_progress * Random.value * 50f * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value)),
                    vb_progress * Random.value * 50f * Mathf.PerlinNoise1D(15f * (Time.time + UnityEngine.Random.value))
                );
            }
            else
            {
                vibration_timer = 1f;
                button.anchoredPosition = button_ap;
            }

        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
}
