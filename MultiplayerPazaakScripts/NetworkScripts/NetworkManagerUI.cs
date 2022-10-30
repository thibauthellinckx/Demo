using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    // public TMP_InputField password;
    public TMP_InputField nameInputField;
    public Transform[] spawnpoints;

    private static Dictionary<ulong,PlayerData> clientData;

    private void Start()
    {
       NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
       NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
       NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        if(NetworkManager.Singleton.IsServer)
        {
            clientData.Remove(clientId);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
    }

    private void HandleServerStarted()
    {

    }

    public void Host()
    {
        clientData = new Dictionary<ulong, PlayerData>();
        GetPayloadData();
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }
    public void Client()
    {
        GetPayloadData();
        NetworkManager.Singleton.StartClient();

    }

    private void GetPayloadData()
    {
        var payload = JsonUtility.ToJson(new ConnectionPayload()
        {
            // password = password.text,
            playerName = nameInputField.text
        });
        byte[] payloadBytes = Encoding.ASCII.GetBytes(payload);

        NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        string payload = Encoding.ASCII.GetString(request.Payload);
        var ConnectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
        spawnPos = spawnpoints[NetworkManager.Singleton.ConnectedClients.Count].position;
        spawnRot = spawnpoints[NetworkManager.Singleton.ConnectedClients.Count].rotation;

        clientData[request.ClientNetworkId] = new PlayerData(ConnectionPayload.playerName);
        Debug.Log(request.ClientNetworkId);
        Debug.Log(ConnectionPayload.playerName);
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = spawnPos;
        response.Rotation = spawnRot;
    }



}
