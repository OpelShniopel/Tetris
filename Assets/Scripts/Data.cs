using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private static readonly float Cos = Mathf.Cos(Mathf.PI / 2f);
    private static readonly float Sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = { Cos, Sin, -Sin, Cos };

    internal static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new()
    {
        {
            Tetromino.I,
            new Vector2Int[] { new(-1, 1), new(0, 1), new(1, 1), new(2, 1) }
        },
        {
            Tetromino.J,
            new Vector2Int[]
                { new(-1, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            Tetromino.L,
            new Vector2Int[] { new(1, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            Tetromino.O,
            new Vector2Int[] { new(0, 1), new(1, 1), new(0, 0), new(1, 0) }
        },
        {
            Tetromino.S,
            new Vector2Int[] { new(0, 1), new(1, 1), new(-1, 0), new(0, 0) }
        },
        {
            Tetromino.T,
            new Vector2Int[] { new(0, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            Tetromino.Z,
            new Vector2Int[] { new(-1, 1), new(0, 1), new(0, 0), new(1, 0) }
        }
    };

    private static readonly Vector2Int[,] WallKicksI =
    {
        {
            new(0, 0), new(-2, 0), new(1, 0), new(-2, -1),
            new(1, 2)
        },
        {
            new(0, 0), new(2, 0), new(-1, 0), new(2, 1),
            new(-1, -2)
        },
        {
            new(0, 0), new(-1, 0), new(2, 0), new(-1, 2),
            new(2, -1)
        },
        {
            new(0, 0), new(1, 0), new(-2, 0), new(1, -2),
            new(-2, 1)
        },
        {
            new(0, 0), new(2, 0), new(-1, 0), new(2, 1),
            new(-1, -2)
        },
        {
            new(0, 0), new(-2, 0), new(1, 0), new(-2, -1),
            new(1, 2)
        },
        {
            new(0, 0), new(1, 0), new(-2, 0), new(1, -2),
            new(-2, 1)
        },
        {
            new(0, 0), new(-1, 0), new(2, 0), new(-1, 2),
            new(2, -1)
        }
    };

    private static readonly Vector2Int[,] WallKicksJlostz =
    {
        {
            new(0, 0), new(-1, 0), new(-1, 1), new(0, -2),
            new(-1, -2)
        },
        {
            new(0, 0), new(1, 0), new(1, -1), new(0, 2),
            new(1, 2)
        },
        {
            new(0, 0), new(1, 0), new(1, -1), new(0, 2),
            new(1, 2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, 1), new(0, -2),
            new(-1, -2)
        },
        {
            new(0, 0), new(1, 0), new(1, 1), new(0, -2),
            new(1, -2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, -1), new(0, 2),
            new(-1, 2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, -1), new(0, 2),
            new(-1, 2)
        },
        {
            new(0, 0), new(1, 0), new(1, 1), new(0, -2),
            new(1, -2)
        }
    };

    internal static readonly Dictionary<Tetromino, Vector2Int[,]> WallKicks = new()
    {
        { Tetromino.I, WallKicksI },
        { Tetromino.J, WallKicksJlostz },
        { Tetromino.L, WallKicksJlostz },
        { Tetromino.O, WallKicksJlostz },
        { Tetromino.S, WallKicksJlostz },
        { Tetromino.T, WallKicksJlostz },
        { Tetromino.Z, WallKicksJlostz }
    };
}