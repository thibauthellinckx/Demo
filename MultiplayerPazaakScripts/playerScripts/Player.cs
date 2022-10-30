using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{

[SerializeField] private int _maxBonusCards = 4;
[SerializeField] public int maxBonusCards{get{return _maxBonusCards;} private set{_maxBonusCards = value;}}
[SerializeField] private bool IsTurn;
[SerializeField] private List<CardSO> Cards;
[SerializeField] private List<CardSO> BonusCards;

    void Start()
    {
        Cards = new List<CardSO>();
        GameManager.Instance.RegisterPlayer(this);
    }


    [ServerRpc]
    public void AskForCardServerRpc()
    {
        AskForCardClientRpc();
    }
    [ClientRpc]
    private void AskForCardClientRpc()
    {
        CardSO card = GameManager.Instance.GetCard();
        if(!IsOwner)
        {
        GameManager.Instance.DealPlaceHolderCard(card);
        return;
        }
        GameManager.Instance.DealCard(this, card);
    }

    public void AddBonusCard(CardSO card)
    {
        BonusCards.Add(card);
    }
    public void AddCard(CardSO card)
    {
        if(!IsOwner)return;
        Cards.Add(card);
    }


    
}
