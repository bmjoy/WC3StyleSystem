using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustonNetworkManager : NetworkManager {

	public GameObject Textfield_ip;
	public Text TextMyIP;

	void Awake(){
		singleton = this;
		logLevel = LogFilter.FilterLevel.Fatal;
	}

	public void StartupHost(){
		if (!NetworkClient.active && !NetworkServer.active && NetworkManager.singleton.matchMaker == null) {
			SetPort ();
			NetworkManager.singleton.StartHost ();
			TextMyIP.text = LocalIPAddress ();
		}
	}

	public void JoinGame(){
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	public void CloseHost(){
		if (NetworkServer.active || NetworkClient.active) {
			NetworkManager.singleton.StopHost ();
		}
	}

	public void CancelConnect(){
		
	}

	public void LeaveGame(){
		NetworkManager.singleton.StopClient ();
	}

	void SetIPAddress(){
		string ipAddress = Textfield_ip.GetComponent<InputField> ().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}

	public string LocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}
		
	/// <summary>
	/// Callbacks
	/// </summary>

	// --- Start Step ---

	//Step 1st
	public override void OnStartHost() {
		//Debug.Log("1st >> Host has started");
	}
	//Step 2nd
	public override void OnStartServer() {
		//Debug.Log("2nd >> Server has started");
	}
	//step 3rd
	public override void OnServerConnect(NetworkConnection conn) {
		//Debug.Log("3rd >> A client connected to the server: " + conn);
	}
	//Step 4th (client only)
	public override void OnStartClient(NetworkClient client) {
		//Debug.Log("4th >> Client has started");
	}
	//step 5th
	public override void OnServerReady(NetworkConnection conn) {
		NetworkServer.SetClientReady(conn);
		//Debug.Log("5th >> Client is set to the ready state (ready to receive state updates): " + conn);
	}
	//Step 6th
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		//Debug.Log("6th >> Client has requested to get his player added to the game" + player);
	}
	//Step 7th (client only)
	public override void OnClientConnect(NetworkConnection conn) {
		base.OnClientConnect(conn);
		//Debug.Log("7th >> Connected successfully to server, now to set up other stuff for the client...");
		//TurnPlayController (true);
	}
		
	// -- Stop Step ---

	//Stop Step 1st
	public override void OnStopHost() {
		//Debug.Log("1st >> Host has stopped");
	}
	//Stop Step 2nd
	public override void OnStopServer() {
		//Debug.Log("2nd >> Server has stopped");
	}
	//Stop Step 3rd
	public override void OnStopClient() {
		//Debug.Log("3rd >> Client has stopped");
		//TurnPlayController (false);
		//GlobalInfo.current.BackToMenu();
	}

	// --- other event ---

	public override void OnServerDisconnect(NetworkConnection conn) {
		// This will display the value of the variable PlayerName in the Server's console window
		Debug.Log(conn.playerControllers[0].gameObject.GetComponent<LocalPlayerSetting>().infoUniqueName);
		string tmpName = conn.playerControllers[0].gameObject.GetComponent<LocalPlayerSetting>().infoUniqueName;
		// If you want to relay the message to all other clients, call a RPC
		ServerLogic.current.RpcBroadcastmessage(tmpName + " has disconnected the game."); // Only a sample RPC call


		NetworkServer.DestroyPlayersForConnection(conn);
		if (conn.lastError != NetworkError.Ok) {
			//if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }
		}
		Debug.Log("A client disconnected from the server: " + conn);

	}

	public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player) {
		if (player.gameObject != null)
			NetworkServer.Destroy(player.gameObject);
		Debug.Log ("Server remove player ");
	}

	public override void OnServerError(NetworkConnection conn, int errorCode) {
		Debug.Log("Server network error occurred: " + (NetworkError)errorCode);
	}

	public override void OnClientDisconnect(NetworkConnection conn) {
		StopClient();
		if (conn.lastError != NetworkError.Ok) {
			//if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }
		}
		Debug.Log("Client disconnected from server: " + conn);
	}

	public override void OnClientError(NetworkConnection conn, int errorCode) {
		Debug.Log("Client network error occurred: " + (NetworkError)errorCode);
	}

	public override void OnClientNotReady(NetworkConnection conn) {
		Debug.Log("Server has set client to be not-ready (stop getting state updates)");
	}
		
	public override void OnClientSceneChanged(NetworkConnection conn) {
		base.OnClientSceneChanged(conn);
		Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");
	}

}