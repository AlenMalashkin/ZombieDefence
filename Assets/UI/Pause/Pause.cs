using System;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        pause.onClick.AddListener(PauseGame);
        resume.onClick.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        pause.onClick.RemoveListener(PauseGame);
        resume.onClick.RemoveListener(ResumeGame);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pause.gameObject.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pause.gameObject.SetActive(true);
        pausePanel.SetActive(false);
    }
}
