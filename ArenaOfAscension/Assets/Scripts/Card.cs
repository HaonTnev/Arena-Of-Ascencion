using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName ="Card",menuName = "Cards")]
public class Card : ScriptableObject
{
    //The States cards can be in to be checked in diffren scripts.
    //Probably best to implement heplerfunctions that return current state instead of GetComponent<Card> all the time.  
    public CardState cardState;
    public CardCategory cardCategory; 
    public Selection selection = Selection.notSelected;
 
    public enum CardState
    {
        inDeck,
        inHand,
        inArena,
        inDeadPile
    }
    
    public enum Selection
    {
        selected, 
        notSelected
    }

    public enum CardCategory
    {
        Spell,
        Item, 
        Rune,
        Unit
    }
    // data associated with each card
    public int cardID;
    public int cardCost;
    public int cardSTR;
    public int cardDEF;

    public Sprite Artwork;
    public Sprite Cardback;

    public string cardName;

    //The traits will probably be implemented via a list of bools to be set true according to the respective traits. 
    //This will enable changing the traits during runtime. But for now, given the stage of the project it's just saved as is.
    public string cardTraits;
    public bool Human;
    public bool Devout;


    //Flavour text
    public string cardDescription;

    //Not sure how the unique card abilities will be implemented. remains to be seen


}
