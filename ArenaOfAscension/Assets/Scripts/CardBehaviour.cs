using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardBehaviour : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameManager    gameManager;
    public                   Card           card;
    [SerializeField] private Arena          arena;
    [SerializeField] private CardDisplay    cardDisplay;
    [SerializeField] private Ability ability;

    // public event Action onCardIsPlayedFromHand;

    public GameObject currentTile;
    public Image artwork;

    public bool isHovered = false;
    public bool wasUsedThisTurn = false;
    

    //Set refrences to GameObjects and scripts that cannot be set in the inspector
    private void Awake()
    {
        cardDisplay = gameObject.GetComponent<CardDisplay>();
        arena = GameObject.Find("Arena").GetComponent<Arena>();
        card = gameObject.GetComponent<CardDisplay>().card;
        GameObject temp = GameObject.Find("Canvas");
        gameManager = temp.GetComponent<GameManager>();

    }

    void Start()
    {
        ability = new Ability(); 
        //EventManager.onCardIsPlayedFromHand += 
    }

    // Update is called once per frame
    void Update()
    {
        Playable();
        cardDisplay.CheckCardState();
    }


    //Check if card is hovered
    public void OnMouseOver()
    {
        isHovered = true;
    }

    public void OnMouseExit()
    {
        isHovered = false;
    }

    //Check if card is playable
    public bool Playable()
    {
        if (card.cardState == Card.CardState.inHand && card.cardCost <= gameManager.currentMana)
        {
            return true;
        }
        else return false;
    }


    //Check if a card is selectable
    public bool Selectable()
    {
        if (card.cardState == Card.CardState.inArena && 
            gameManager.turn == GameManager.Turn.playerTurn)
        {
                return true;
        }
        else return false;
    }

    //Perform everything associated with playing the card from hand.
    //To be optimized
    public void OnPlay()
    {

        gameManager.PayMana(card.cardCost);
        currentTile = transform.parent.gameObject;
      
            currentTile.GetComponent<ArenaTile>().occupiedByFriend = true;
            currentTile.GetComponent<BoxCollider2D>().enabled = false;
        

        card.cardState = Card.CardState.inArena;
        GetTilePosition(currentTile);
        artwork.transform.localPosition = new Vector3(0,0,0);

        
        switch (cardDisplay.cardID)
        {
            case 0: break;
            case 1: break;
            case 2: break;
            case 3: ability.HuninAbility(); break;
            case 4: break;
            case 5: break;
            case 6: ability.PriestPlayAbility(); break;
        }

    }

  

    //Get position of the card in the arena by feeding in parent tile go.
    public Vector2Int GetTilePosition(GameObject go)
    {
       Vector2Int pos =  transform.parent.gameObject.GetComponent<ArenaTile>().arenaPos;

        return pos;
    }

    //Show if neighbours are eligable or not
    public void HighlightNeighbors()
    {  
        List<GameObject> neighbors = transform.parent.gameObject.GetComponent<ArenaTile>().GetNeighboringTiles(GetTilePosition(currentTile));
       
        foreach (GameObject neighbor in neighbors)
        {
           Image imageToHighligt =  neighbor.GetComponent<Image>();
            if (neighbor.GetComponent<ArenaTile>().occupiedByFriend== false)
            {
                imageToHighligt.color = Color.green;
            }
            else
            {
                imageToHighligt.color = Color.red;
            }            
        }       
    }

    // Reset the highlighted tiles to base state
    public void ResetHighlight()
    {
        List<GameObject> neighbors = transform.parent.gameObject.GetComponent<ArenaTile>().GetNeighboringTiles(GetTilePosition(currentTile));

        foreach (GameObject neighbor in neighbors)
        {
            Image imageToReset = neighbor.GetComponent<Image>();

            imageToReset.color = Color.white;
        }
    }

 

    public GameObject SelectCard()
    {
        if (Selectable() == true)
        {
            GameObject selectedCard;
            card.selection = Card.Selection.selected;
            selectedCard = gameObject;
            return selectedCard;
        }
        else return null;
    }

    //Set new tile as parent, reset old tile.
    //To be optimized 
    public void MoveSelectedCardToEligableNeighbour(GameObject eligableNeighbour)
    {
        GameObject cameFrom = currentTile;
        if (wasUsedThisTurn==false)
        {
            if (eligableNeighbour.GetComponent<ArenaTile>().occupiedByFriend == false && eligableNeighbour.GetComponent<ArenaTile>().occupiedByFoe == false)
            {
                gameObject.transform.SetParent(eligableNeighbour.transform, false);
                eligableNeighbour.GetComponent<ArenaTile>().occupiedByFriend = true;
                eligableNeighbour.GetComponent<BoxCollider2D>().enabled = false;
                cameFrom.GetComponent<ArenaTile>().occupiedByFriend = false;
                cameFrom.GetComponent<BoxCollider2D>().enabled = true;
                currentTile = eligableNeighbour;
                card.selection = Card.Selection.notSelected;
            }
            if (eligableNeighbour.GetComponent<ArenaTile>().occupiedByFoe == true)
            {
                CardDoDamage(eligableNeighbour);
            }

            wasUsedThisTurn = true;
        }


    }
    public void CardDoDamage(GameObject occupiedTile)
    {
        int dEFToCompareTo = occupiedTile.GetComponentInChildren<EnemyDisplay>().health;
        int sTRToCompareTo = occupiedTile.GetComponentInChildren<EnemyDisplay>().enemy.enemyATK;

        if (sTRToCompareTo > card.cardDEF)
        {
            
            GameObject toKill = gameObject;
            occupiedTile.GetComponentInChildren<EnemyBehaviour>().Kill(toKill);
            occupiedTile.GetComponentInChildren<EnemyDisplay>().health = 
                occupiedTile.GetComponentInChildren<EnemyBehaviour>().
                TakeDamage(occupiedTile.GetComponentInChildren<EnemyBehaviour>().currentHealth, card.cardSTR);
        }
        else
        {
            occupiedTile.GetComponentInChildren<EnemyDisplay>().health =
                occupiedTile.GetComponentInChildren<EnemyBehaviour>().
                TakeDamage(occupiedTile.GetComponentInChildren<EnemyBehaviour>().currentHealth, card.cardSTR);

        }
        card.selection = Card.Selection.notSelected;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectCard();
    }
}
