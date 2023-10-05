using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{ 
    private Deck deck;

    /// <summary>
    /// BattleCry
    /// </summary>
    // Draw Card
    public void HuninAbility()
    {
        Debug.Log("Hunin was played ");
        deck = GameObject.Find("Deck").GetComponent<Deck>();
        deck.DrawCard(1);
    }

    //IncreaseDevoutSTR
    public void PriestPlayAbility()
    {
        GameObject[] unitsInArena = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject unit in unitsInArena)
        {
            if (unit.GetComponent<CardDisplay>().Devout == true)
            {
                for (int i = 0; i < 1; i++)
                {
                    unit.GetComponent<CardDisplay>().cardSTR = unit.GetComponent<CardDisplay>().cardSTR + 1;
                    Debug.Log(unit.name + unit.GetComponent<CardDisplay>().cardSTR);
                    unit.GetComponent<CardDisplay>().SetCardValues();
                }

            }
        }
    }
    /// <summary>
    /// deathCry
    /// </summary>
    public void PriestDeathAbility()
    {
        GameObject[] unitsInArena = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject unit in unitsInArena)
        {
            if (unit.GetComponent<CardDisplay>().Devout == true)
            {
                for (int i = 0; i < 1; i++)
                {
                    unit.GetComponent<CardDisplay>().cardSTR = unit.GetComponent<CardDisplay>().cardSTR - 1;
                    Debug.Log(unit.name + unit.GetComponent<CardDisplay>().cardSTR);
                    unit.GetComponent<CardDisplay>().SetCardValues();
                }

            }
        }
    }
}


