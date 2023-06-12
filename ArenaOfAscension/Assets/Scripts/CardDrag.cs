using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public GameManager gameManager;
    public Card card;
    public CardBehaviour cardBehaviour;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject bg;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform parentToReturn;


    private void Awake()
    {
        card = gameObject.GetComponent<CardDisplay>().card;
        cardBehaviour = gameObject.GetComponent<CardBehaviour>();
        GameObject temp = GameObject.Find("Canvas");
        canvas = temp.GetComponent<Canvas>();
        gameManager = temp.GetComponent<GameManager>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
       
    }
    public void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (card.cardState == Card.CardState.inHand)
        {
            bg.SetActive(false);
           // Debug.Log("Begin Drag");
            canvasGroup.blocksRaycasts = false;
            parentToReturn = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
        }
        else
        {
            return;
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        
        if (card.cardState == Card.CardState.inHand)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
            
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        this.transform.SetParent(parentToReturn, false);

        if (cardBehaviour.Playable())
        {
            
            gameObject.GetComponent<CardBehaviour>().OnPlay();
        }

        if (card.cardState == Card.CardState.inArena)
        {
            
        }
        else
        {
            bg.SetActive(true);
        }
        
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
       // Debug.Log("On Drop");        
    }

}
