using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
	public TMP_Text FPSText;
	public string defaultString = "FPS : ";
	private string fpsString;
	private int fps;

	private void Start()
	{
		StartCoroutine(CalculateFPS());
	}

	IEnumerator CalculateFPS()
	{
		while(true)
		{
			fps = (int)(1.0f / Time.deltaTime);
			fpsString = defaultString + fps.ToString("000");
			FPSText.text = fpsString;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
