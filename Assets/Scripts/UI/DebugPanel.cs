using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
	[Space(10f)]
	[SerializeField]
	private TMP_Text FPSText;
	[SerializeField]
	private string defaultFpsString = "FPS : ";
	[Space(10f)]
	[SerializeField]
	public TMP_Text CurrentUI;
	[SerializeField]
	public string defaultCurrentUIString = "Current UI : ";

	public Coroutine CalculateFPS_Coroutine;


	private void Start()
	{
		CalculateFPS_Coroutine = StartCoroutine(CalculateFPS());
	}

	IEnumerator CalculateFPS()
	{
		while (true)
		{
			int fps = (int)(1.0f / Time.deltaTime);
			string fpsString = defaultFpsString + fps.ToString("000");
			FPSText.text = fpsString;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
