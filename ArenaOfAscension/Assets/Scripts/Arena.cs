using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Arena : MonoBehaviour
{
    public Dictionary<Vector2, GameObject> arenaTiles;
    private RectTransform rectTransform;

    [SerializeField] int arenaSizeX;
    [SerializeField] int arenaSizeY;
    [SerializeField] GameObject arenaTile;
    [SerializeField] GameObject ENEMY;

    Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = gameObject.transform.localPosition;
        rectTransform = GetComponent<RectTransform>();
        arenaTiles = new Dictionary<Vector2, GameObject>();
        CreateArena();
    }
    void Start()
    {

    }
    void SpawnEnemy(GameObject enemyToSpawn, GameObject parent)
    {
        Instantiate(enemyToSpawn, parent.transform);
        parent.GetComponent<ArenaTile>().occupiedByFoe = true;


    }
    private void CreateArena()
    {
        GameObject parent = gameObject;
        for (int i = 0; i < arenaSizeX; i++)
        {
            for (int j = 0; j < arenaSizeY; j++)
            {
                Vector2Int arenaPos = new Vector2Int(i, j);
                GameObject tile =
                Instantiate(arenaTile, transform);
                tile.transform.SetParent(parent.transform, false);
                tile.transform.position = new Vector3(parent.transform.position.x +i, parent.transform.position.y+ j, parent.transform.position.z);  
                tile.name = arenaPos.ToString();
                tile.GetComponent<ArenaTile>().arenaPos = new Vector2Int(i, j);
                arenaTiles.Add(arenaPos, tile);
                
            }
        }
        
        SpawnEnemy(ENEMY, GetRandomTile());
        gameObject.transform.localPosition = defaultPos;
    }
    public GameObject GetRandomTile()
    {
        if (arenaTiles.Count == 0)
        {
            Debug.LogWarning("Arena tiles list is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, arenaTiles.Count);
        return arenaTiles[new Vector2Int(Random.Range(0, arenaSizeX), Random.Range(0, arenaSizeY))];
    }
    public bool IsPosValid (Vector2Int pos)
    {
      return arenaTiles.ContainsKey(pos);
    }

}
