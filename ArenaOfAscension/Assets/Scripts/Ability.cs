using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{ 
    private Deck deck;

    /// <summary>
    /// BattleCry
    /// </summary>

    // No Battlecry. CardID 0
    public void PesantAbility()
    {
        Debug.Log("Pesant kann nix!");
    }
    // Draw Card. CardID 1
    public void BerserkAbility()
    {
        Debug.Log("Berserk kann nix!");
    }
    // Draw Card. CardID 2
    public void ShieldmaidAbility()
    {
        Debug.Log("Shieldmaid kann noch nix!");
    }

    // Draw Card. CardID 3
    public void HuninAbility()
    {
        Debug.Log("Hunin was played ");
        deck = GameObject.Find("Deck").GetComponent<Deck>();
        deck.DrawCard(1);
    }
    // Draw Card. CardID 4
    public void MuninAbility()
    {
        Debug.Log("Munin kann nix!");
    }
    // Draw Card. CardID 5
    public void ValkyrieAbility()
    {
        Debug.Log("Valkyrie kann nix!");
    }
 
    //IncreaseDevoutSTR. CardID 6
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
                 //   Debug.Log(unit.name + unit.GetComponent<CardDisplay>().cardSTR);
                    unit.GetComponent<CardDisplay>().SetCardValues();
                }

            }
        }
    }


    /// <summary>
    /// deathCry
    /// </summary>

    //DecreaseDevoutSTR. CardID 6
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


