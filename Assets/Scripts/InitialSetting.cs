using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetting : MonoBehaviour {


	public GameObject MovePad;
	public GameObject SkillPad;

	// Use this for initialization
	void Start () {
		MovePad.SetActive (false);
		SkillPad.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
