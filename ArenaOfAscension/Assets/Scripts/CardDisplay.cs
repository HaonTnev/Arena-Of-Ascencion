using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
/// <summary>
/// The CardDisplay once, when instaniated,
/// calls the values of the Card scriptable object and transfers the data onto itself.
/// 
/// </summary>
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

    // Start is called before the first frame update
    void Start()
    {
        cardBehaviour = gameObject.GetComponent<CardBehaviour>();
        SetCardValues();
        FindPlayAreas();
        
        
       
    }

    void Update()
    {
        ResetSelection();
        
    }

    void FindPlayAreas()
    {
        deadPile =  GameObject.Find("DeadPile");
        handArea =  GameObject.Find("Hand");
        deckArea =  GameObject.Find("Deck");
        arena =     GameObject.Find("Arena");
    }
    void SetCardValues()
    {
        costText.text = card.cardCost.ToString(); 
        sTRText.text = card.cardSTR.ToString();
        dEFText.text = card.cardDEF.ToString();

        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;

        cardArtwork.sprite = card.Artwork;
        card.cardState = Card.CardState.inDeck;
       // Debug.Log(gameObject.name + ": state " + card.cardState);
    }

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
        cardArtwork.color = Color.blue;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            card.selection = Card.Selection.notSelected;
            Debug.Log("Selection reset");
        }
    }

}
