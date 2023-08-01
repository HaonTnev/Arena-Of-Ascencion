using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// The CardDisplay once, when instaniated,
/// calls the values of the Card scriptable object and transfers the data onto itself.
/// This is neccecary because changes of the scriptable object during runtime are saved onto it. 
/// So the CardDisplay is responsible for showing the cards specifications 
/// meaning that if these change due to other cards effects they are changed here. 
/// </summary>
/// 
public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Hand hand;
    public Deck deck;
    public CardBehaviour cardBehaviour;

    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject bg;

    private GameObject handArea;
    private GameObject arena;
    private GameObject deckArea;
    private GameObject deadPile;

    public TextMeshProUGUI costText, nameText, sTRText, dEFText, traitsText, descriptionText;

    public Image cardArtwork;
 
    public int cardID;
    public int cardCost;
    public int cardSTR;
    public int cardDEF;
    public string cardName;
    public bool Human;
    public bool Devout;


    // Start is called before the first frame update
    void Start()
    {       
        // set values according to card
        cardID = card.cardID;
        cardCost = card.cardCost;
        cardSTR = card.cardSTR;
        cardDEF = card.cardDEF;
        cardName = card.cardName;
        Human = card.Human;
        Devout = card.Devout;

        cardBehaviour = gameObject.GetComponent<CardBehaviour>();
        SetCardValues();
        FindPlayAreas();
        card.cardState = Card.CardState.inDeck;

    }

    void Update()
    {
        ResetSelection();
       // SetCardValues();
        
    }

    void FindPlayAreas()
    {
        deadPile =  GameObject.Find("DeadPile");
        handArea =  GameObject.Find("Hand");
        deckArea =  GameObject.Find("Deck");
        arena =     GameObject.Find("Arena");
    }

    //Should be called from whereever changes to card values are made, so the UI updates.
    public void SetCardValues()
    {
        costText.text = cardCost.ToString(); 
        sTRText.text = cardSTR.ToString();
        SetSTRTextColor();
        dEFText.text = cardDEF.ToString();
        SetDEFTextColor();

        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;

        cardArtwork.sprite = card.Artwork;

    }


    // Tbh I have no Idea what this does anymore :,)
    public void CheckCardState()
    {

        if (card.cardState == Card.CardState.inDeck)
        {
            cardBack.SetActive(true);
        }
        else if (card.cardState == Card.CardState.inHand)
        {
            cardBack.SetActive(false);
        }
        else if (card.cardState == Card.CardState.inArena)
        {
            if (card.selection == Card.Selection.selected)
            {
                HighlightCard();
            }
            if (card.selection == Card.Selection.notSelected)
            {
                ResetHighlightCard();
            }

            if (cardBehaviour.isHovered == true)
            {
                cardBehaviour.HighlightNeighbors();
            }
            else
            {
                cardBehaviour.ResetHighlight();
            }
        }
        else
        {
            bg.SetActive(true);
        }

    }



    public void HighlightCard()
    {
        cardArtwork.color = Color.gray;
    }
    public void ResetHighlightCard()
    {
        cardArtwork.color = Color.white;
    }
    IEnumerator WaitForDraw()
    {
        yield return new WaitForSeconds(.5f);
    }
    private void OnMouseDown()
    {
        
        if (card.cardState == Card.CardState.inDeck)
        {
           // DrawCard();
        }
        if (card.cardState == Card.CardState.inHand)
        {
            

        }
    }

    void DiscardCard()
    {
            gameObject.transform.SetParent(deadPile.transform, false);
            gameObject.transform.position = deadPile.transform.position;
            card.cardState = Card.CardState.inDeadPile;            
    }

    public void ResetSelection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            card.selection = Card.Selection.notSelected;
            ResetHighlightCard();
            Debug.Log("Selection reset");
        }
    }


    private void SetSTRTextColor()
    {
        if (cardSTR > card.cardSTR)
        {
            sTRText.color = Color.green;
        }
        else if (cardSTR < card.cardSTR)
        {
            sTRText.color = Color.red;
        }
        else
        {
            sTRText.color = Color.white;
        }
    }
    private void SetDEFTextColor()
    {
        if (cardDEF > card.cardDEF)
        {
            dEFText.color = Color.green;
        }
        else if (cardDEF < card.cardDEF)
        {
            dEFText.color = Color.red;
        }
        else
        {
            dEFText.color = Color.white;
        }
    }
}
