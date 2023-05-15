using YG;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Reward : MonoBehaviour
{
    [SerializeField] private Text rewardAdText;
    [SerializeField] private int adId;
    
    private Bank _bank;
    
    private int _reward;

    [Inject]
    private void Init(Bank bank)
    {
        _bank = bank;
    }
    
    private void Awake()
    {
        _reward = PlayerPrefs.GetInt("CurrentWave", 1) * 200;
        rewardAdText.text = $"Посмотрите рекламу и получите {_reward} монет";
    }

    private void OnEnable()
    {
        YandexGame.CloseVideoEvent += GetReward;
    }

    private void OnDisable()
    {
        YandexGame.CloseVideoEvent -= GetReward;
    }

    private void GetReward(int id)
    {
        if (id == adId)
            _bank.AddMoney(_reward);
    }
}
