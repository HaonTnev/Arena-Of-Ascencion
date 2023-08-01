using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameoflife : MonoBehaviour
{
    [SerializeField] int fieldSize;
    [SerializeField] GameObject Zelle;
    public Dictionary<Vector2, GameObject> cells;


    void Awake()
    {
        cells = new Dictionary<Vector2, GameObject>();
        CreateField();
        
    }

    void CreateField()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Vector2Int tempVec2 = new Vector2Int(i,j);
                GameObject temp = Instantiate(Zelle,new Vector3Int(i , j, 0), Quaternion.identity);
                temp.name = new string(i + "," + j);
                temp.transform.SetParent(gameObject.transform);
                temp.GetComponentInChildren<Cell>().positioneee = tempVec2;
                cells.Add(temp.GetComponentInChildren<Cell>().positioneee, temp);
            }
        }

    }
}
