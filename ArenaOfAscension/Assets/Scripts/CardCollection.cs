using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    // create List of all Cards. At the moment only the starter deck.

    public static List<Card> cardCollection = new List<Card>();

    private void Awake()
    {
        //cardCollection.Add(new Card(0, 0, 1, 1, null, "Peasant","Human","Just a lowly Peasant"));
        //cardCollection.Add(new Card(1, 1, 3, 1, null, "Berserker", "Human, Warrior", "Packs a punch. SOME KIND OF BATTLECRY???"));
        //cardCollection.Add(new Card(2, 2, 2, 2, null, "Shieldmaid", "Human, Warrior", "Alrounder"));
        //cardCollection.Add(new Card(3, 2, 1, 3, null, "Valkyrie", "Divine, Warrior", "Shields up!"));
        //cardCollection.Add(new Card(4, 1, 2, 0, null, "Valkyrie Spear", "Weapon", "Divine only"));
        //cardCollection.Add(new Card(5, 1, 0, 2, null, "Nose Helmet", "Armour", "Warrior only"));
        

    }


}
