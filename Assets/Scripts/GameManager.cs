using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOfLife gameOfLife;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private bool randomizeAtStart;

    void Start()
    {
        // For answer 3 - here you can load the data from server.
        gameOfLife.ResetGameOfLife(gridSize, randomizeAtStart);
    }

    public void ResetGameOfLife(Vector2Int newGridSize, bool randomize)
    {
        gameOfLife.ResetGameOfLife(newGridSize, randomize);
    }
}
