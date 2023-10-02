using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Norse_Hunin : MonoBehaviour
{

    private CardDisplay cardDisplay;
    [SerializeField] private GameManager gameManager;
    CardBehaviour cardBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        cardDisplay = gameObject.GetComponent<CardDisplay>();
        cardBehaviour = gameObject.GetComponent<CardBehaviour>();



        EventManager.onCardIsPlayedFromHand += HuninDraw;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HuninDraw()
    {
        Debug.Log("Hunin Draw was called");
        gameManager.DRAWFUNCTION(1);

    }
}
