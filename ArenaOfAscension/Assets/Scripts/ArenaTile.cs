using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArenaTile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] private GameManager    gameManager;
    [SerializeField] private Card           card;
    [SerializeField] private Arena          arena;
    [SerializeField] private CardBehaviour  cardBehaviour;


    [SerializeField] private Image  image;

    public Vector2Int arenaPos;
    public bool occupiedByFriend = false; 
    public bool occupiedByFoe = false;

    private void Start()
    {
        image = GetComponent<Image>();
        arena = this.GetComponentInParent<Arena>();
    }

    #region Event Functions
   
    public void OnPointerEnter(PointerEventData eventData)
    {        
       // image.color = Color.green;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       // image.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        CardDrag d = eventData.pointerDrag.GetComponent<CardDrag>();
        cardBehaviour = d.gameObject.GetComponent<CardBehaviour>();
        
        if (cardBehaviour.Playable()== true)
        {
            d.parentToReturn = this.transform;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        List<GameObject> neighbours = GetNeighboringTiles(arenaPos);
        foreach (GameObject tile in neighbours)
        {
            if (IsNeighboursChildSelected(tile) == true && occupiedByFriend == false || IsNeighboursChildSelected(tile) == true && occupiedByFoe == true)
            {
                tile.GetComponentInChildren<CardBehaviour>().MoveSelectedCardToEligableNeighbour(gameObject);
            }
        }
    }
    #endregion
    #region Position Functions
    public List<GameObject> GetNeighboringTiles(Vector2Int position)
    {
        List<GameObject> neighbors = new List<GameObject>();

        // Define neighboring offsets
        // Will probably be defined in the Card: Scriptable Object at some point
        // to allow difftent movement patterns of various cards
        Vector2Int[] offsets = new Vector2Int[]
        {
            new Vector2Int(0, 1),   // Top
            new Vector2Int(0, -1),  // Bottom
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(1, 0)    // Right
        };

        // Calculate neighboring positions
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int neighborPos = position + offset;
            if (arena.IsPosValid(neighborPos))
            {
                GameObject neighborTile = GetTile(neighborPos);

                neighbors.Add(neighborTile);
            }
        }

        return neighbors;
    }
    

    public GameObject GetTile(Vector2Int pos)
    {
        if (arena.arenaTiles.TryGetValue(pos, out GameObject tile))
        {
            return tile;
        }
        else
        {
            Debug.LogWarning("tile not found at position:" + pos);
            return null;
        }
    }

    public bool IsNeighboursChildSelected(GameObject neighbour)
    {
      
            if (neighbour.GetComponentInChildren<CardBehaviour>() != null)
            {
                if (neighbour.GetComponentInChildren<CardBehaviour>().card.selection == Card.Selection.selected)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        
    }

    #endregion
}