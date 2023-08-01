using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int positioneee;
    Gameoflife gol;
    List<GameObject> neighbors = new List<GameObject>();
    [SerializeField] public bool alive;
    
    bool esDarfGelebtWerden = false;
    IEnumerator DoEveryFiveSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            DoSomething();
        }
    }

   
    void DoSomething()
    {
        if (esDarfGelebtWerden)
        {
            CheckNeighbourState();
        }
        
    }

    void Start()
    {
       
        gol = GetComponentInParent<Gameoflife>();
        GetNeighbours(positioneee);
        alive = false;
        StartCoroutine(DoEveryFiveSeconds());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ErweckeSieZumLeben();
            esDarfGelebtWerden = true;
            
        }

        ChangeColorAccordingToState(alive);

    }

    void GetNeighbours(Vector2Int pos)
    {
        Vector2Int[] offsets = new Vector2Int[]
        {
            new Vector2Int(-1,-1),  // Bottom left
            new Vector2Int(1, 1),   // Top right
            new Vector2Int(-1, 1),  // Top left
            new Vector2Int(1, 1),   // Bottom right

            new Vector2Int(0, 1),   // Top
            new Vector2Int(0, -1),  // Bottom
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(1, 0),   // Right
            

        };

        foreach (Vector2Int cellToCheck in offsets)
        {
            Vector2Int posToCheck = positioneee + cellToCheck;
            if (DoesNeighbourexist(posToCheck))
            {
                gol.cells.TryGetValue(posToCheck, out GameObject neighbour);
                neighbors.Add(neighbour);
            }
        }
    }

    bool DoesNeighbourexist(Vector2Int posToCheck)
    {
        return gol.cells.ContainsKey(posToCheck);
    }


    int countOfAliveNeighbours;
    void CheckNeighbourState()
    {
        countOfAliveNeighbours = 0;
        foreach (GameObject item in neighbors)
        {
            if (item.GetComponent<Cell>().alive)
            {
                countOfAliveNeighbours ++;
            } 
        }

        if (alive&&countOfAliveNeighbours>3)
        {
            alive = false;
        }
        if (alive==false&&countOfAliveNeighbours==3)
        {
            alive = true;
        }
        if (alive&&countOfAliveNeighbours ==2 ||alive&&countOfAliveNeighbours==3)
        {
            alive = true;
        }
        if (alive && countOfAliveNeighbours<2)
        {
            alive = false;
        }
    }

    void ChangeColorAccordingToState(bool alive)
    {
        if (alive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }


    GameObject[] tote;
    void ErweckeSieZumLeben()
    {
        tote = FindedieToten();
        Debug.Log(tote.Length);

        float probalilit‰‰‰;
        for (int i = 0; i < 5; i++)
        {
            probalilit‰‰‰ = Random.Range(0, 1);

            if (probalilit‰‰‰ <= 0.5)
            {
                GameObject go = tote[Random.Range(0,tote.Length)];  
                go.GetComponent<Cell>().alive = true;
            }
        }
    }

    GameObject[] FindedieToten()
    {
        return GameObject.FindGameObjectsWithTag("TOOOT");  
         
    }
}
