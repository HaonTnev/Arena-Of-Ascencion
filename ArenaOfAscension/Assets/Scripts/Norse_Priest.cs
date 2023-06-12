using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Norse_Priest : MonoBehaviour
{
    
   private CardDisplay cardDisplay;
    CardBehaviour cardBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        cardDisplay = gameObject.GetComponent<CardDisplay>();
        cardBehaviour = gameObject.GetComponent<CardBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cardDisplay.card.cardState == Card.CardState.inArena)
        {
            IncreaseDevoutSTR();
        }
    }

    public void IncreaseDevoutSTR()
    {
        GameObject [] unitsInArena = GameObject.FindGameObjectsWithTag("Card");

        foreach ( GameObject unit  in unitsInArena)
        {
            if (unit.GetComponent<CardDisplay>().card.Devout == true)
            {
                for (int i = 0; i < 1; i++)
                {
                    unit.GetComponent<CardDisplay>().card.cardSTR = unit.GetComponent<CardDisplay>().card.cardSTR + 1;
                    Debug.Log(unit.name + unit.GetComponent<CardDisplay>().card.cardSTR);
                } 

            }
        }
    }
}
