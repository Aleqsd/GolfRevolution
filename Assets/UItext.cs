using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		UnityEngine.UI.Text[] textUIs = GetComponentsInChildren<UnityEngine.UI.Text> ();
		foreach (UnityEngine.UI.Text textUI in textUIs)
		{
			if (textUI.name.Equals ("Par"))
				textUI.text = "Par : " + gameflow.par;
			else if (textUI.name.Equals ("CurrentStrokes"))
				textUI.text = "CurrentStrokes : " + gameflow.currentStrokes;
			else if (textUI.name.Equals ("TotalStrokes"))
				textUI.text = "TotalStrokes : " + gameflow.totalStrokes;
			else if (textUI.name.Equals ("Lands")) 
			{
				if (gameflow.size < 10) 
					textUI.text = "Lands : "+gameflow.size + " (Easy)";
				else if (gameflow.size > 10 && gameflow.size < 20) 
					textUI.text = "Lands : "+gameflow.size + " (Medium)";
				else if (gameflow.size > 20 && gameflow.size < 40) 
					textUI.text = "Lands : "+gameflow.size + " (Hard)";
				else
					textUI.text = "Lands : "+gameflow.size + " (Extreme)";
			}

		}
	}
}
