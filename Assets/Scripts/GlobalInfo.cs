using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GlobalInfo : NetworkBehaviour {
	
	public static GlobalInfo current;

	public GameObject Prefabs_wolf;

	public GameObject CameraGame;
	public GameObject CameraUI;
	public GameObject MovePad;
	public GameObject skillPad;
	public SkillArea skillPadSpt;
	public GameObject spellPad;

	public GameObject TitleUI;
	public GameObject GameUI;
	public GameObject StartButton;
	public GameObject PanelGameSystem;

	public GameObject ImageIP;
	public GameObject ButtonStartGame;

	public GameObject localPlayer;

	public GameObject nowBallOwner;
	[SyncVar] public int randomID;

	public List<GameObject> totalPlayer;
	public List<GameObject> historyPlayer;

	public GameObject prefabs_CenterAura;
	public GameObject prefabs_Explosion;
	public GameObject prefabs_ball;

	//Server use only
	public GameObject gameWolf;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {
		totalPlayer = new List<GameObject> ();
		historyPlayer = new List<GameObject> ();
	}

	public void AddPlayer(GameObject player){
		totalPlayer.Add (player);
		historyPlayer.Add (player);
		Debug.Log ("Total:" + historyPlayer.Count + ", now add:" + player.name);
	}

	public void TurnIntoGameUI(GameObject player){
		localPlayer = player;

		skillPadSpt.Caster = player;
		CameraGame.GetComponent<ThirdCamera> ().target = player.transform;

		CameraUI.SetActive (false);

		CameraGame.SetActive (true);
		MovePad.SetActive (true);
		skillPad.SetActive (true);

		TitleUI.SetActive (false);
		GameUI.SetActive (true);

	}

	public void BackToMenu(){

		CameraUI.SetActive (true);

		CameraGame.SetActive (false);
		MovePad.SetActive (false);
		skillPad.SetActive (false);

		TitleUI.SetActive (true);
		GameUI.SetActive (false);

	}

	public void ServerInfoAndButton(bool tn){
		ImageIP.SetActive (tn);
		ButtonStartGame.SetActive (tn);
	}

	[Command]
	void SetupWolf(){
		gameWolf = Instantiate (Prefabs_wolf, Vector3.zero, Quaternion.identity);
		gameWolf.SetActive (false);
		gameWolf.transform.position = Vector3.zero;
		NetworkServer.Spawn (gameWolf);
	}

	[Command]
	public void CmdStartGame(){
		if (isServer) {
			//Player Setting
			RpcStartGame ();

			SetupWolf ();
		}
	}

	[ClientRpc]
	public void RpcStartGame(){
		totalPlayer = new List<GameObject>(historyPlayer);
		for(var i = totalPlayer.Count - 1; i > -1; i--)
		{
			if (totalPlayer [i] == null) {
				totalPlayer.RemoveAt (i);
				continue;
			}
			totalPlayer [i].GetComponent<UnitCtrl> ().Revive ();
			Debug.Log ("list Count:" + totalPlayer.Count + ", name: " +totalPlayer [i].name);
		}

		GlobalInfo.current.spellPad.SetActive (false);
		PanelGameSystem.SetActive (false);
		StartCoroutine (StartGameIE ());
	}

	IEnumerator StartGameIE(){
		//Effect for Every Player
		GameObject start3 = Instantiate (prefabs_CenterAura, Vector3.zero, Quaternion.Euler (new Vector3 (-90, 0, 0)));
		yield return new WaitForSeconds (1.0f);
		GameObject start2 = Instantiate (prefabs_CenterAura, Vector3.zero, Quaternion.Euler (new Vector3 (-90, 0, 0)));
		start2.transform.localScale = new Vector3 (2.0f, 2.0f, 2.0f);
		yield return new WaitForSeconds (1.0f);
		GameObject start1 = Instantiate (prefabs_CenterAura, Vector3.zero, Quaternion.Euler (new Vector3 (-90, 0, 0)));
		start1.transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
		yield return new WaitForSeconds (1.0f);
		GameObject start0 = Instantiate (prefabs_Explosion, Vector3.zero, Quaternion.Euler (new Vector3 (-90, 0, 0)));

		if (isServer) {
			gameWolf.SetActive (true);
			ResetBall ();
		}

		yield return new WaitForSeconds (1.0f);
		Destroy (start3);
		Destroy (start2);
		Destroy (start1);
		Destroy (start0, 1);

	}

	public void ResetBall(){
		if (totalPlayer.Count <= 0)
			return;
		nowBallOwner = totalPlayer[Random.Range(0,totalPlayer.Count - 1)];

		nowBallOwner.GetComponent<UnitCtrl> ().hasBall = true;
		nowBallOwner.GetComponent<LocalPlayerSetting> ().SetLocalSkillPad ();

		GameObject projectile = Instantiate (prefabs_ball, gameObject.transform.position + new Vector3 (0, 2, 0), Quaternion.identity) as GameObject;
		projectile.transform.LookAt(nowBallOwner.transform.position);
		projectile.GetComponent<MagicProjectileScript> ().impactNormal = new Vector3 (0, 1, 0);
		MagicBallScript _ball = projectile.GetComponent<MagicBallScript> ();
		_ball.SetSourceAndTarget (gameObject, nowBallOwner.gameObject);
	}

	public void KillPlayer(GameObject obj){
		totalPlayer.Remove (obj);
	}

	public void PrepareNext(){
		if (totalPlayer.Count > 1) {
			StartCoroutine (PrepareNextIE ());
		} else {
			ResetGame ();
		}
	}

	IEnumerator PrepareNextIE(){
		yield return new WaitForSeconds (3);
		ResetBall ();
	}

	public void ResetGame(){
		StartButton.SetActive (true);
	}
}
