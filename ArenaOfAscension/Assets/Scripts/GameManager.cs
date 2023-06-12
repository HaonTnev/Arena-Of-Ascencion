using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Deck deck;
    [SerializeField] private Card card;
    [SerializeField] private Arena arena;
    [SerializeField] private CardBehaviour cardBehaviour;
    [SerializeField] private EnemyBehaviour enemyBehaviour;

    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    delegate void TestDelegate();
    TestDelegate turnSwitch;

    public TextMeshProUGUI manaText;
    public int maxMana = 3;
    public int currentMana;

    public TextMeshProUGUI playerHealthText;
    public int maxPlayerHealth;
    public int currentPlayerHealth;
    //Is equal to all cards def. When a card dies it's health is subtracted.
    //If health reaches zero, so if no unit is left, the player looses.
    //This is the current loseCon
    //If no enemy is left, the player wins.
    //This is the current winCon

    public Turn turn;

    public enum Turn
    {
        playerTurn, 
        enemyTurn
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
        deck = GameObject.Find("Deck").GetComponent<Deck>(); ;
        turnSwitch = enemyTurn;
        GameStarts();
        //deck.DrawCard(3);
        //Draw functions correctly if used by the end turn button but not at the start of the game
    }

    void enemyTurn()
    {

    }

    public void Win()
    {
        winWindow.SetActive(true);
    }
    public void Lose()
    {
        loseWindow.SetActive(true);
    }
    int SetMaxPlayerHealth(List<GameObject> decklist)
    {
        int toReturn = 0;
        int index = 0;
        foreach (GameObject card in decklist)
        {           
            GameObject cardToGetValueFrom = decklist[index];
            int cardValue = cardToGetValueFrom.GetComponent<CardDisplay>().card.cardDEF;
            toReturn = toReturn + cardValue;            
            index++;
        }
        return toReturn;
    }
    public void StartChallenge()
    {
        startWindow.SetActive(false);
        deck.DrawCard(3);
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMana();
        DisplayHealth();
        if (currentPlayerHealth<=0)
        {
            Lose();
        }
    }

    public void GameStarts()
    {
        turn = Turn.playerTurn;
        currentMana = maxMana;
        maxPlayerHealth = SetMaxPlayerHealth(deck.playingDecklist);
        currentPlayerHealth = maxPlayerHealth;
        

    }


    public int PayMana (int manaCost)
    {
        return (currentMana -= manaCost);
    }

    private void DisplayHealth()
    {
        playerHealthText.text = currentPlayerHealth + "/" + maxPlayerHealth.ToString();
    }
    private void DisplayMana()
    {
        manaText.text = currentMana + "/" + maxMana.ToString();
    }

     public void EndTurn()
    {
        turn = Turn.enemyTurn;
    }

     public void EnemyTurn()
    {
       // Debug.Log("Enemy turn starts.");
        enemyBehaviour.EnemyMove();

        //Debug.Log("Enemy turn ends.");
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        turn = Turn.playerTurn;
        deck.DrawCard(1);
        currentMana = maxMana;

    }
}
