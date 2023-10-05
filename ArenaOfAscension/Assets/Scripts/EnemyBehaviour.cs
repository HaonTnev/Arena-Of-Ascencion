using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager    gameManager;
    [SerializeField] private ArenaTile      arenaTile;
    [SerializeField] private EnemyDisplay   enemyDisplay;
    [SerializeField] private Enemy          enemy;

    [SerializeField] private GameObject deadPile;
    private bool isHovered = false;
    [SerializeField] GameObject info;
    public GameObject currentTile;
    public int currentHealth;
    private Ability ability;


    // Start is called before the first frame update
    void Start()
    {
        ability = new Ability();
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>(); 
        deadPile = GameObject.Find("DeadPile");
        enemyDisplay = gameObject.GetComponent<EnemyDisplay>();
        enemy = gameObject.GetComponent<EnemyDisplay>().enemy;
        currentHealth = enemyDisplay.health;
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTile = transform.parent.gameObject;
        ShowEnemyInfo();
        KillEnemy();
    }


    //If enemy is hoverd show his current stats
    public void OnMouseOver()
    {
        isHovered = true;
    }
    public void OnMouseExit()
    {
        isHovered = false;
    }    
    public void ShowEnemyInfo()
    {
        if (isHovered == true)
        {
            info.SetActive(true);
        }
        if (isHovered != true)
        {
            info.SetActive(false);
        }
    }

    public void KillEnemy()
    {
        if (currentHealth <= 0)
        {
            gameManager.Win();
        }
    }


    public Vector2Int GetTilePosition(GameObject go)
    {
        Vector2Int pos = transform.parent.gameObject.GetComponent<ArenaTile>().arenaPos;

        return pos;
    }
    public void EnemyMove()
    {
        GameObject cameFrom = currentTile;
        List<GameObject> neighbours = 
                currentTile.GetComponent<ArenaTile>().GetNeighboringTiles(GetTilePosition(currentTile));
        int randomIndex = Random.Range(0, neighbours.Count);
        GameObject toMoveTo = neighbours[randomIndex];
      //  Debug.Log("Troll wants to move to: " + toMoveTo.name);
        if (toMoveTo.GetComponent<ArenaTile>().occupiedByFoe == false&& toMoveTo.GetComponent<ArenaTile>().occupiedByFriend == false)
        {
            gameObject.transform.SetParent(toMoveTo.transform, false);
            toMoveTo.GetComponent<ArenaTile>().occupiedByFoe = true;
            cameFrom.GetComponent<ArenaTile>().occupiedByFoe = false;
          //  Debug.Log(toMoveTo.name + " was not occupied. Troll moved to" + toMoveTo.name);
        }
        else
        {
          //  Debug.Log(toMoveTo.name + " was occupied.");
            EnenmyDoDamage(toMoveTo);

            if (toMoveTo.GetComponent<ArenaTile>().occupiedByFriend==false)
            {
                gameObject.transform.SetParent(toMoveTo.transform, false);
                toMoveTo.GetComponent<ArenaTile>().occupiedByFoe = true;
                cameFrom.GetComponent<ArenaTile>().occupiedByFoe = false;
            }
            
        }



    }

    public void EnenmyDoDamage(GameObject occupiedTile)
    {
       int dEFToCompareTo = occupiedTile.GetComponentInChildren<CardDisplay>().card.cardDEF;
       int sTRToCompareTo = occupiedTile.GetComponentInChildren<CardDisplay>().card.cardSTR;

        if (dEFToCompareTo < enemy.enemyATK)
        {
            Debug.Log(occupiedTile.GetComponentInChildren<CardDisplay>().card.cardName + " was too weak and died");
            GameObject toKill = occupiedTile.GetComponentInChildren<CardDisplay>().gameObject;
            Kill(toKill);
            enemyDisplay.health = TakeDamage(currentHealth, sTRToCompareTo);
        }
        else
        {
            
            enemyDisplay.health = TakeDamage(currentHealth, sTRToCompareTo);
            Debug.Log(occupiedTile.GetComponentInChildren<CardDisplay>().card.cardName + " was strong and did damage to troll.");
        }

            

    }

    public void Kill(GameObject toKill)
    {
        toKill.GetComponentInParent<ArenaTile>().occupiedByFriend = false;
        toKill.transform.SetParent(deadPile.transform, false);
        toKill.GetComponent<CardDisplay>().card.cardState = Card.CardState.inDeadPile;
        gameManager.currentPlayerHealth = gameManager.currentPlayerHealth - toKill.GetComponent<CardDisplay>().card.cardSTR;

        switch (toKill.GetComponent<CardDisplay>().card.cardID)
        {
            case 0: break;
            case 1: break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: ability.PriestDeathAbility(); break;
        }
        
    }

    public int TakeDamage(int health, int cardDMG)
    {
        int temp = health - cardDMG;
        currentHealth =  temp;
        return temp;

    }
}
