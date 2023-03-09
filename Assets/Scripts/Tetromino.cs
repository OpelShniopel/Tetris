using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino // The type of the tetromino shape (I, J, L, O, S, T, Z)
{
    I,
    J,
    L,
    O,
    S,
    T,
    Z
}

[Serializable]
public struct TetrominoData
{
    [field: SerializeField] public Tetromino Type { get; set; } // The type of the tetromino
    [field: SerializeField] public Tile Tile { get; set; } // The tile used to render the tetromino
    public Vector2Int[] Cells { get; private set; } // The cells of the tetromino
    public Vector2Int[,] WallKicks { get; private set; } // The wall kicks for the tetromino

    // Initialize the tetromino data
    public void Initialize()
    {
        Cells = Data.Cells[Type]; // Get the cells from the data
        WallKicks = Data.WallKicks[Type]; // Get the wall kicks from the data
    }
}