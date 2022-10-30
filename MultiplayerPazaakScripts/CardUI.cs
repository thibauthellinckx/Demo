using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    private CardSO card;
    [SerializeField] private TMP_Text valueInputField;
    
    public void SetData(CardSO _card)
    {
        card = _card;
        valueInputField.text = card.value.ToString();
    }
}
