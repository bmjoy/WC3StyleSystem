using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WCBlizzard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void BJDebugMsg(string msg){
		Text _text = GameObject.Find ("BJDebugMsg").GetComponent<Text> ();
		if (_text != null)
			_text.text += msg + "\n";
	}
}
