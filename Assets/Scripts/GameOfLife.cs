using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2Int gridSize;
    [Range(0.00001f, 3f)] private float stepsRate;
    [SerializeField] private bool randomizeAtStart;

    private Vector2 cellSize;
    private CellController[,] cells;

    void Start()
    {
        Camera cam = Camera.main;
        cam.transform.position = new Vector3(gridSize.x / 2, gridSize.y / 2, cam.transform.position.z);

        var cellSpriteRenderer = cellPrefab.GetComponent<SpriteRenderer>();
        cellSize = new Vector2(cellSpriteRenderer.sprite.bounds.size.x, cellSpriteRenderer.sprite.bounds.size.y);

        CreateGrid(gridSize.x, gridSize.y);
        // I could've set the randomization at the grid creation for better run time (going through the grid only once)
        //  but I'd rather have the two functions seperated
        if (randomizeAtStart)
        {
            RandomizeGridState();
        }
    }

    void Update()
    {
        // StartCourotine
        // // 
    }

    private void CreateGrid(int width, int height)
    {
        cells = new CellController[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellPos = new Vector2(x * cellSize.x, y * cellSize.y);
                GameObject cell = Instantiate(cellPrefab, cellPos, Quaternion.identity);
                cells[x, y] = cell.GetComponent<CellController>();
            }
        }
    }

    private void RandomizeGridState()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                bool newState = Random.Range(0, 2) == 0;
                cells[x, y].SetState(newState);
            }
        }
    }

    // Maybe add enumarator to run through the grid
}
