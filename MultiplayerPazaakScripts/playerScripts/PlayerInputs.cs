using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : NetworkBehaviour
{
    [SerializeField] private Button dealBtn;
    private Player player;

    void Start()
    {
        if(!IsLocalPlayer)
        {
            this.enabled = false;
        }
        player = GetComponent<Player>();
        dealBtn = GameObject.Find("Deal").GetComponent<Button>();
        dealBtn.onClick.AddListener(() =>
            {
                if(!IsOwner)return;
                player.AskForCardServerRpc();
            });  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
