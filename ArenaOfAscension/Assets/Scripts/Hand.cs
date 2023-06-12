using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public Card card;
    public Deck deck;
    public static List<GameObject> HandList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        //HandList.Insert(0, Deck.Decklist[0]);
        //for (int j = 0; j < Hand.HandList.Count; j++)
        //{
        //    Debug.Log(Hand.HandList[j].name);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectCard()
    {
        if (card.cardState == Card.CardState.inDeck)
        {
            //cardDisplay.DrawCard();
        }
        if (card.cardState == Card.CardState.inHand)
        {
           
        }
    }
}
