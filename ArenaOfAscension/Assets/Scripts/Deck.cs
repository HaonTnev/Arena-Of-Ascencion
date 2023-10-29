using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;


    //Get references to the card prefabs.
    public GameObject pesant, berserk, shieldmaid, hunin, valkyrie, priest, item;

    //Create Decklist 
    public List<GameObject> startingDecklist    = new List<GameObject>();
    public List<GameObject> playingDecklist     = new List<GameObject>();
    public List<GameObject> handList            = new List<GameObject>();

    private GameObject handArea;
    private int deckSize;
    // Start is called before the first frame update
    void Awake()
    {
        handArea = GameObject.Find("Hand");
        //Add the Prefabs to the List.

        startingDecklist.Add(pesant);
        startingDecklist.Add(berserk);
        startingDecklist.Add(shieldmaid);
        startingDecklist.Add(hunin);
        startingDecklist.Add(valkyrie);
        startingDecklist.Add(priest);
        startingDecklist.Add(item);
        // Debug.Log("before shuffle:" + string.Join(",", Decklist));
        Shuffle(startingDecklist);
       // Debug.Log("after shuffle:" + string.Join(",", Decklist));

        deckSize = startingDecklist.Count;

        //Create a stack of card prefabs (the standard deck) at the location of this gameObject. 
        for (int i = 0; i < deckSize; i++)
        {
            //set zpos so only the top card is hit wit DrawCard. there is certainly a more elegant way to do this. 
            float zpos = 1*i;
            GameObject card =
            Instantiate(startingDecklist[i], new Vector3(0,0,zpos), Quaternion.identity);
            card.transform.SetParent(gameObject.transform, false);
            playingDecklist.Add(card);
            
        }

        

    }

    public void AddCardToDeck()
    {

    }

    public GameObject GetCardToDraw()
    {
        List<GameObject> listToDrawFrom = GetDeck(playingDecklist);

        
        
            if (listToDrawFrom.Count == 0)
            {
                Debug.LogWarning("Deck is empty!");
                return null;
            }

            int randomIndex = Random.Range(0, playingDecklist.Count);
            GameObject cardToDraw = playingDecklist[randomIndex];
            playingDecklist.Remove(cardToDraw);
            handList.Add(cardToDraw);
            
           
            return cardToDraw;
           
        

        
    }
    public void DrawCard(int drawamount)
    {
        for (int i = 0; i < drawamount; i++)
        {

             GameObject cardToDraw = GetCardToDraw();

                 if (cardToDraw != null)
                 {
                         cardToDraw.transform.SetParent(handArea.transform, false);
     
                        cardToDraw.GetComponent<CardDisplay>().card.cardState = Card.CardState.inHand;
                 }
                 else
                 {
                        Debug.Log("Deck is Empty");
                        gameManager.currentPlayerHealth -= 1;
                 }
        }

    }
    public List<GameObject> GetDeck(List<GameObject> decklist)
    {
        return decklist;
    }
    public void Shuffle<T>(List<T> listToShuffle)
    {
       // Debug.Log("before shuffle:" + string.Join(",", Decklist));

        for (int i = 0; i < listToShuffle.Count-1; i++)
        {
            T temp = listToShuffle[i];
            int randomizer = Random.Range(i, listToShuffle.Count);
            listToShuffle[i] = listToShuffle[randomizer];
            listToShuffle[randomizer] = temp;
        }

    }
}
