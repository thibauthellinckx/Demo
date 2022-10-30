using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;


public class GameManager : NetworkBehaviour
{

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private NetworkVariable<int> player1Score = new NetworkVariable<int>();
    [SerializeField] private NetworkVariable<int> player2Score = new NetworkVariable<int>();
    [SerializeField] public TMP_Text player1ScoreUI;
    [SerializeField] public TMP_Text player2ScoreUI;
    [SerializeField] private GameObject CardGrid;
    [SerializeField] private GameObject CardGridRed;
    [SerializeField] private GameObject cardPlaceHolder;
    [SerializeField] private CardSO[] cards;
    [SerializeField] private CardSO[] bonusCards;
    [SerializeField] public List<Player> Players;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get{
            if(_instance == null) Debug.LogError("Game manager is null");
            return _instance;
        }
        private set{}
    }
   
    
    void Awake()
    {
        _instance = this;
        Players = new List<Player>();
        CardGrid = GameObject.Find("CardGrid");
        CardGridRed = GameObject.Find("OpponentCardGrid");
        player1ScoreUI = GameObject.Find("Score").GetComponent<TMP_Text>();
        player2ScoreUI = GameObject.Find("opponentScore").GetComponent<TMP_Text>();
        player1Score.OnValueChanged += updateScore;
        player2Score.OnValueChanged += updateScore2;
    }

    private void updateScore2(int previousValue, int newValue)
    {
        player2ScoreUI.text = newValue.ToString();
    }

    private void updateScore(int previousValue, int newValue)
    {
        player1ScoreUI.text = newValue.ToString();
    }

    public void RegisterPlayer(Player player)
    {
        switch (Players.Count)
        {
            case 0: player1 = player;
            break;

            case 1: player2 = player;
            break;
        }
        Players.Add(player);
        SetCards(player);
    }


    private void SetCards(Player player)
    {
        Debug.Log("test of setcards");
        Debug.Log(player.maxBonusCards);
        for (int i = 0; i < player.maxBonusCards; i++)
        {
        var randomInt = UnityEngine.Random.Range(0,bonusCards.Length-1);
        CardSO cardToAdd = bonusCards[randomInt];
        player.AddBonusCard(cardToAdd);
        }
    }

    public void DealPlaceHolderCard(CardSO cardToAdd)
    {
        player2Score.Value += cardToAdd.value;
        UpdateOpponentUI(cardToAdd);
    }
    public void DealCard(Player player, CardSO cardToAdd)
    {
        player.AddCard(cardToAdd);
        player1Score.Value += cardToAdd.value;
        UpdateCardUI(cardToAdd);
    }

    public CardSO GetCard()
    {
        Debug.Log(cards.Length);
        var randomInt = UnityEngine.Random.Range(0, cards.Length);
        Debug.Log(randomInt);

        CardSO cardToAdd = cards[randomInt];
        return cardToAdd;
    }

    public void UpdateOpponentUI(CardSO cardData)
    {
        var card = Instantiate(cardPlaceHolder,Vector3.zero,Quaternion.identity,CardGridRed.transform);
        card.GetComponent<CardUI>().SetData(cardData);

    }
    private void UpdateCardUI(CardSO cardData)
    {
        var card = Instantiate(cardPlaceHolder,Vector3.zero,Quaternion.identity,CardGrid.transform);
        card.GetComponent<CardUI>().SetData(cardData);
    }

}
