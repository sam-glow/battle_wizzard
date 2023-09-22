using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class splash_screen : MonoBehaviour
{
    [SerializeField] private RectTransform scroll_text;
    [SerializeField] private RectTransform button;

    [SerializeField] private List<GameObject> stuff_to_turn_on;
    [SerializeField] private List<GameObject> stuff_to_turn_off;

    [SerializeField] private float tutorial_time = 60f;

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
        ass.Play();
        app_time = 0f;

        button_ap = button.anchoredPosition;

        for (int i = 0; i < stuff_to_turn_off.Count; i++)
            stuff_to_turn_off[i].SetActive(false);

        for (int i = 0; i < stuff_to_turn_on.Count; i++)
            stuff_to_turn_on[i].SetActive(true);

    }

    void Update()
    {
        scroll_text.anchoredPosition += Vector2.up * Time.deltaTime * scroll_speed;

        app_time += Time.deltaTime;
        if (app_time > tutorial_time)
        {
            for (int i = 0; i < stuff_to_turn_off.Count; i++)
                stuff_to_turn_off[i].SetActive(true);

            for (int i = 0; i < stuff_to_turn_on.Count; i++)
                stuff_to_turn_on[i].SetActive(false);

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
    }
}
