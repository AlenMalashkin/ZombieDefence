using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AfterWaveSceneSetuper : MonoBehaviour
{
	[Header("Scene Objects")]
	[SerializeField] private Image background;
	[SerializeField] private Text textMassage;
	[SerializeField] private Text displayCurrentWave;
	[SerializeField] private ChangeSceneButton buttonPrefab;
	[SerializeField] private Transform buttonsContainer;

	[Header("Resources")] 
	[SerializeField] private Sprite nextLevelButtonSprite;
	[SerializeField] private Sprite retryButtonSprite;
	[SerializeField] private Sprite shopButtonSprite;
	[SerializeField] private Sprite backButtonSprite;
	
	private CurrentAfterWaveState _state;
	
	[Inject]
	private void Init(CurrentAfterWaveState state)
	{
		_state = state;
	}

	private void Awake()
	{
		switch (_state.State)
		{
			case AfterWaveState.Win:
				SetupWinAfterWaveScreen();
				break;
			case AfterWaveState.Defeat:
				SetupDefeatAfterWaveScreen();
				break;
		}
	}

	private void SetupWinAfterWaveScreen()
	{
		background.color = Color.green;
		textMassage.text = "Волна пройдена!";
		displayCurrentWave.text = "Текущая волна: " + PlayerPrefs.GetInt("CurrentWave", 1);
		CreateChangeSceneButton("Main", nextLevelButtonSprite);
		CreateChangeSceneButton("Shop", shopButtonSprite);
		CreateChangeSceneButton("Menu", backButtonSprite);
	}

	private void SetupDefeatAfterWaveScreen()
	{
		background.color = Color.red;
		textMassage.text = "Волна не пройдена!";
		displayCurrentWave.text = "Текущая волна: " + PlayerPrefs.GetInt("CurrentWave", 1);
		CreateChangeSceneButton("Main", retryButtonSprite);
		CreateChangeSceneButton("Shop", shopButtonSprite);
		CreateChangeSceneButton("Menu", backButtonSprite);
	}

	private void CreateChangeSceneButton(string sceneName, Sprite sprite)
	{
		var changeSceneButton = Instantiate(buttonPrefab, buttonsContainer);
		changeSceneButton.SetupButton(sceneName, sprite);
	}
}
