using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] private AudioClip music;
    
	private Sound _sound;
    
	[Inject]
	private void Init(Sound sound)
	{
		_sound = sound;
	}

	private void Awake()
	{
		_sound.PlayMusic(music);
		SceneManager.LoadScene("Menu");
	}
}
