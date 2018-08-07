using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerSetting : NetworkBehaviour {

	[SyncVar] public string playerUniqueName;
	private NetworkInstanceId playerNetID;

	//For every one , Client and Server will be Run
	void Start () {
		SetObjectIdentity ();
		GlobalInfo.current.AddPlayer (gameObject);
	}

	// For local player Setup
	public override void OnStartLocalPlayer(){
		
		GlobalInfo.current.TurnIntoGameUI (gameObject);
		playerNetID = GetNetIdentity ();
		string uniqueName = "Player " + playerNetID.ToString ();
		CmdTellServerMyIdentity (uniqueName);

		ServerInfos ();
	}

	[Server]
	void ServerInfos(){
		GlobalInfo.current.ServerInfoAndButton (true);
	}

	[Client]
	NetworkInstanceId GetNetIdentity(){
		return GetComponent<NetworkIdentity> ().netId;
	}

	[Client]
	void SetObjectIdentity(){
		if (!isLocalPlayer)
			transform.name = playerUniqueName;
		else
			transform.name = "Player " + playerNetID.ToString ();
	}

	[Command]
	void CmdTellServerMyIdentity(string name)
	{
		playerUniqueName = name;
	}

	public void SetLocalSkillPad(){
		if (isLocalPlayer) {
			GlobalInfo.current.spellPad.SetActive (true);
		}
	}
}
