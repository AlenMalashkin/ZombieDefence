using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VavilichevGD.Utils.Timing;
using Zenject;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private float waveDuration;
    [SerializeField] private Image timerImage;
    [SerializeField] private Text timerText;

    private SyncedTimer _timer;
    private CurrentAfterWaveState _currentAfterWaveState;

    [Inject]
    private void Init(CurrentAfterWaveState currentAfterWaveState)
    {
        _currentAfterWaveState = currentAfterWaveState;
    }

    private void Awake()
    {
        _timer = new SyncedTimer(TimerType.UpdateTick);

        _timer.Start(waveDuration);
    }

    private void OnEnable()
    {
        _timer.TimerFinished += EndGame;
        _timer.TimerValueChanged += UpdateTimerView;
    }

    private void OnDisable()
    {
        _timer.TimerFinished -= EndGame;
        _timer.TimerValueChanged -= UpdateTimerView;
    }

    private void UpdateTimerView(float remainTime, TimeChangingSource source)
    {
        var normalizedValue = Mathf.Clamp(remainTime / waveDuration, 0.0f, 1.0f);
        timerImage.fillAmount = normalizedValue;
        
        float minutes = Mathf.FloorToInt(remainTime / 60);
        float seconds = Mathf.FloorToInt(remainTime % 60);
        timerText.text = $"{minutes:00} : {seconds:00}";
    }

    private void EndGame()
    {
        var currentWave = PlayerPrefs.GetInt("CurrentWave", 1);
        PlayerPrefs.SetInt("CurrentWave", currentWave + 1);
        
        _currentAfterWaveState.State = AfterWaveState.Win;
        SceneManager.LoadScene("AfterWave");
    }
}
