using System;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
	public Action<Collider> OnTriggerEntered;
	public Action<Collider> OnTriggerExited;
	
	private void OnTriggerEnter(Collider other)
	{
		OnTriggerEntered?.Invoke(other);
	}

	private void OnTriggerExit(Collider other)
	{
		OnTriggerExited?.Invoke(other);
	}
}
