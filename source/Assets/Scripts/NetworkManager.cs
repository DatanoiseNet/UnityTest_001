using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    private const string typeName = "UniqueGameName";
    private const string gameName = "RoomName";

    private bool isRefreshingHostList = false;
    private HostData[] hostList;

    public GameObject playerPrefab;


	void Start()
	{
		MasterServer.ipAddress = "87.238.194.227"; // Different location than the default one
		MasterServer.port = 23466;
	}

    void OnGUI()
    {
		MasterServer.ipAddress = "87.238.194.227"; // Different location than the default one
		MasterServer.port = 23466;
		if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void StartServer()
    {
		MasterServer.ipAddress = "87.238.194.227"; // Different location than the default one
		MasterServer.port = 23466;
		Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
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
            hostList = MasterServer.PollHostList();
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
		MasterServer.ipAddress = "87.238.194.227"; // Different location than the default one
		MasterServer.port = 23466;
		Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
       // Network.Instantiate(playerPrefab, Vector3.up * 5, Quaternion.identity, 0);
		//Network.Instantiate(playerPrefab, new Vector3(0,0,0) , Quaternion.identity, 0);
		Network.Instantiate(playerPrefab, new Vector3(0,0,0) , Quaternion.Euler(-90.0f, 0,0), 0);

    }
}
