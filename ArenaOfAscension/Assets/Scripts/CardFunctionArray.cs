using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunctionArray : MonoBehaviour
{

    public List<Component> cardAbilities;
    
    [SerializeField] private Component Norse_priest;

    // Start is called before the first frame update
    void Start()
    {
        cardAbilities.Add(Norse_priest);

        Debug.Log(cardAbilities.Count);
        Debug.Log(cardAbilities.IndexOf(Norse_priest));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
