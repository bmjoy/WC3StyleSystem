using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerSetting : NetworkBehaviour {

	[SerializeField]
	private GameObject prefabs_Player;

	[SyncVar(hook = "UpdateThisName")] public string infoUniqueName;
	private NetworkInstanceId infoNetID;

	public bool closeIt;

	// 1st 
	public override void OnStartServer ()
	{
		base.OnStartServer ();
		ServerSetup ();

		//ServerLogic.current.AddPlayer (gameObject);
	}

	// 2nd
	public override void OnStartClient ()
	{
		base.OnStartClient ();
	}

	// 3rd 
	// Setup local player variables
	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
	}

	// 4th
	// For local player Setup
	public override void OnStartLocalPlayer(){
		
		// self setting
		if (isLocalPlayer) {
			string infoName = GetThisInfoName ();

			//it will call UpdateThisName at next step
			CmdTellServerMyIdentity (infoName);

			CmdSpawnPlayer ();

			GlobalInfo.current.SetupLocalPlayerObject (gameObject);
			GlobalInfo.current.TurnIntoGameUI ();

			//GlobalInfo.current.spellPad.SetActive (true);
		}
	}

	// 5th
	// For every one , Client and Server will be Run
	void Start () {
		// Earlier created object won't receive callback , because it has initial with sync value
		if (infoUniqueName != "")
			gameObject.name = infoUniqueName;
	}

	public override void OnNetworkDestroy () {
		base.OnNetworkDestroy ();
	}


	void Update(){
		if (isServer) {
			//Debug.Log (infoUniqueName + "now connection state: " + connectionToClient.ToString());

			if (closeIt == true)
				connectionToClient.Disconnect ();
		}

	}

	/// <summary>
	/// working function
	/// </summary>

	[Server]
	void ServerSetup(){
		//GlobalInfo.current.ShowServerControlPad (true);
	}

	[Client]
	NetworkInstanceId GetNetIdentity(){
		return GetComponent<NetworkIdentity> ().netId;
	}

	[Command]
	void CmdTellServerMyIdentity(string name)
	{
		infoUniqueName = name;
	}

	[Command]
	void CmdSpawnPlayer(){
		StartCoroutine (WaitForReady ());
	}

	IEnumerator WaitForReady(){
		while (!connectionToClient.isReady) {
			yield return new WaitForSeconds (0.25f);
		}
		SpawnPlayer();
	}

	void SpawnPlayer(){
		GameObject player = Instantiate (prefabs_Player, NetworkManager.singleton.GetStartPosition ().position, Quaternion.identity);
		player.GetComponent<PlayerObjectSetting> ().playerID = infoNetID;
		NetworkServer.SpawnWithClientAuthority (player, connectionToClient);
	}

	public void DisconnectGame(){
		if (isLocalPlayer) {
			connectionToServer.Disconnect ();
			connectionToServer.Dispose ();
		}
	}
		
	// --- work methods ---

	string GetThisInfoName(){
		infoNetID = GetComponent<NetworkIdentity> ().netId;
		return "PlayerInfo (" + infoNetID.ToString () + ")";
	}

	// --- callbacks ---
	void UpdateThisName(string name){
		gameObject.name = name;
		infoUniqueName = name;
	}
}
