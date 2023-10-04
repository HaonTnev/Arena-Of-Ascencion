using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{ 
    private Deck deck;
    public delegate void abilities();
    public List<abilities> abilityObject = new List<abilities>();

    public void CreateList()
    {
        deck = GameObject.Find("Deck").GetComponent<Deck>();

        abilityObject.Add(HuninAbility); // Index 0
        abilityObject.Add(HuninAbility); // Index 1 
        abilityObject.Add(HuninAbility); // Index 2 
        abilityObject.Add(HuninAbility); //Index 3
        abilityObject.Add(HuninAbility); // Index 4 
        abilityObject.Add(HuninAbility); // Index 4 
        abilityObject.Add(HuninAbility); // Index 4 
        abilityObject.Add(HuninAbility); // Index 4 
        abilityObject.Add(HuninAbility); // Index 4 
        abilityObject.Add(HuninAbility); // Index 4 
        Debug.Log("List was created "+abilityObject.Count);
        
    }

    // index 3
    public void HuninAbility()
    {
        Debug.Log("Hunin was played ");
            deck.DrawCard(1);
    }
}
    

