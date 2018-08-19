using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour {
	
	public static GlobalInfo current;

	//Game NPC Prefabs
	[Header("--- Game Prefabs ---")]
	public GameObject Prefabs_wolf;
	public GameObject prefabs_CenterAura;
	public GameObject prefabs_Explosion;
	public GameObject prefabs_ball;

	//Game Camera
	[Header("--- Game Camera ---")]
	public GameObject CameraGame;
	public GameObject CameraUI;

	//Game UI
	[Header("--- Game UI ---")]
	public GameObject Panel_TitleUI;
	public GameObject Panel_GameUI;
	public GameObject Panel_InGame_SystemInfo;
	public GameObject ButtonStartGame;
	public GameObject ImageIP;


	//Game Controll Pad
	[Header("--- Controll Pad ---")]
	public GameObject Pad_move;
	public GameObject Pad_skill;
	public GameObject Pad_skill_ice;

	//Runtime Data
	[Header("--- Runtime ---")]
	//in client , local player
	public GameObject localPlayer;


	void Awake(){
		current = this;
	}

	void Start(){
		//NetworkManager.singleton.
	}
		
	public void SetupLocalPlayerObject(GameObject obj){
		localPlayer = obj;
	}

	public void DisconnectConnection(){
		if (localPlayer == null)
			return;
		localPlayer.GetComponent<LocalPlayerSetting> ().DisconnectGame ();
	}

	public void TurnIntoGameUI(){
		//localPlayer = player;

		//skillPadSpt.Caster = player;


		CameraUI.SetActive (false);
		CameraGame.SetActive (true);

		Pad_move.SetActive (true);
		Pad_skill.SetActive (true);

		Panel_TitleUI.SetActive (false);
		Panel_GameUI.SetActive (true);

	}

	public void SetupTargetCamera(GameObject player){
		CameraGame.GetComponent<ThirdCamera> ().target = player.transform;
	}

	public void BackToMenu(){

		CameraUI.SetActive (true);

		CameraGame.SetActive (false);
		Pad_move.SetActive (false);
		Pad_skill.SetActive (false);

		Panel_TitleUI.SetActive (true);
		Panel_GameUI.SetActive (false);

	}

	public void ShowServerControlPad(bool tn){
		ImageIP.SetActive (tn);
		ButtonStartGame.SetActive (tn);
	}

}
