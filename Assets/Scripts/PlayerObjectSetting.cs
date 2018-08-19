using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObjectSetting : NetworkBehaviour {

	[SyncVar] public NetworkInstanceId playerID;
	public GameObject infoObject;

	public override void OnStartClient ()
	{
		base.OnStartClient ();

		infoObject = ClientScene.FindLocalObject (playerID);
		gameObject.name = "Player (" + playerID.ToString() + ")";

		GlobalInfo.current.SetupTargetCamera (gameObject);

	}

	// this is not work
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		Debug.Log ("player obj local player");
	}

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () {

		Debug.Log ("i have authority: " + hasAuthority );

		//Debug.Log (">" + );
		/*
		if(!connectionToServer.isConnected){
			Debug.Log ("disconnect !");
			gameObject.SetActive (false);
		}
		*/
	}
}
