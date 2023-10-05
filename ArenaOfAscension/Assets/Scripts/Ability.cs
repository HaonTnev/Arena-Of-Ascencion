using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{ 
    private Deck deck;
    public delegate void abilities();

    // index 3
    public void HuninAbility()
    {
        Debug.Log("Hunin was played ");
        deck = GameObject.Find("Deck").GetComponent<Deck>();
        deck.DrawCard(1);
    }
}
    

