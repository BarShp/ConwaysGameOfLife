using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2Int gridSize;
    // 128/96
    [SerializeField] private bool randomizeAtStart;
    [SerializeField, Range(0.01f, 2f)] private float stepsRate;

    private Camera mainCam;
    private Vector2 cellSize;
    private CellController[,] cells;
    private bool play = true;
    private float stepCooldown;
    private Collider2D lastHitCell;

    void Start()
    {
        mainCam = Camera.main;
        mainCam.transform.position = new Vector3(gridSize.x / 2, gridSize.y / 2, mainCam.transform.position.z);

        stepCooldown = stepsRate;

        var cellSpriteRenderer = cellPrefab.GetComponent<SpriteRenderer>();
        cellSize = new Vector2(cellSpriteRenderer.sprite.bounds.size.x, cellSpriteRenderer.sprite.bounds.size.y);

        CreateGrid(gridSize.x, gridSize.y);
        // I could've set the randomization at the grid creation for better run time (going through the grid only once)
        //  but I'd rather have the two functions seperated
        if (randomizeAtStart)
        {
            RandomizeGridState();
        }
        else
        {
            play = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            play = !play;
            Debug.Log($"Pause: {!play}");
        }

        if (Input.GetButton("SwitchCellState"))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider != lastHitCell)
            {
                lastHitCell = hit.collider;
                var cell = lastHitCell.GetComponent<CellController>();
                cell.SwitchState();
            }
        }
        if (Input.GetButtonUp("SwitchCellState"))
        {
            lastHitCell = null;
        }

        if (!play) return;

        if (stepCooldown <= 0)
        {
            stepCooldown = stepsRate;
            RenderNextStep();
        }
        else
        {
            stepCooldown -= Time.deltaTime;
        }
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
        foreach (CellController cell in cells)
        {
            bool newState = Random.Range(0, 2) == 0;
            cell.NewState = newState;
            cell.StepNext();
        }
    }

    private void RenderNextStep()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                CellController cell = cells[x, y];
                cell.NewState = CalculateCellsNewState(cell.IsAlive, x, y);
            }
        }
        foreach (CellController cell in cells)
        {
            cell.StepNext();
        }
    }

    private bool CalculateCellsNewState(bool isAlive, int x, int y)
    {
        int liveNeighborsCount = CountLiveNeighbors(x, y);

        if (isAlive)
        {
            // lower than 2 / higher than 3 - false
            return liveNeighborsCount == 2 || liveNeighborsCount == 3;
        }

        return liveNeighborsCount == 3;

        // One liner for fun -
        // int liveNeighborsCount = CountLiveNeighbors(x, y);
        // return isAlive ? (liveNeighborsCount == 2 || liveNeighborsCount == 3) : liveNeighborsCount == 3;
    }

    private int CountLiveNeighbors(int x, int y)
    {
        int count = 0;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                // Wrapping the grid's x and y
                // -1  -> turns to ->  (gridSize.x-1)
                // (gridSize.x+1)  -> turns to ->  1
                int col = (x + i + gridSize.x) % gridSize.x;
                int row = (y + j + gridSize.y) % gridSize.y;

                if (!(col == x && row == y) &&
                    cells[col, row].IsAlive)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
