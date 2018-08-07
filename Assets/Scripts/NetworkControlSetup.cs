using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkControlSetup : NetworkBehaviour {

	// Use this for initialization
	void Start () {

		if (!isLocalPlayer) {
			GetComponent<MoveController> ().enabled = false;

		}
	}
}
