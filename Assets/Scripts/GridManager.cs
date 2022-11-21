using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int[,] Grid;
    public GameObject Cell;
    public Transform Cam;
    public float offset;
    int col, row;
    // Start is called before the first frame update
    void Start()
    {
        col = 9;
        row = 5;
        Grid = new int[col, row];
        CreateGrid();
    }

    private void CreateGrid() {
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                SpawnTile(x, y);
            }
        }

        Cam.transform.position = new Vector3(col/2f - 0.5f, row/2f + 0.5f, -10);
    }

    private void SpawnTile(int x, int y)
    {
        var spawnedCell = Instantiate(Cell, transform.position, Quaternion.identity);
        spawnedCell.name = "Cell " + x + ":" + y;
        spawnedCell.transform.position = new Vector3(transform.position.x + x * offset, transform.position.y + y * offset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
