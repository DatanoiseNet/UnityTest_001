using UnityEngine;
using System.Collections;

public class NetworkHandler : MonoBehaviour
{
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	
	private bool isRefreshingHostList = false;
	
	public GameObject playerPrefab;
	
	void Start() {	
		MasterServer.ipAddress = "87.238.194.227"; // Different location than the default one
		MasterServer.port = 23466;
	}
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			
			if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
				JoinServer();
			
		}
	}
	
	private void StartServer()
	{
		
		Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
		MasterServer.ipAddress = "87.238.194.227";
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer();
	}
	
	
	void Update()
	{
		if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
		{
			isRefreshingHostList = false;
			MasterServer.ipAddress = "87.238.194.227";
		}
	}
	
	private void RefreshHostList()
	{
		if (!isRefreshingHostList)
		{
			isRefreshingHostList = true;
			MasterServer.RequestHostList(typeName);
		}
	}
	
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();
	}
	
	
	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, Vector3.up * 5, Quaternion.identity, 0);
	}
}
