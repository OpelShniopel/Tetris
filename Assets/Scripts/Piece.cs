using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }
    public int RotationIndex { get; private set; }
    public float GravityTimer { get; private set; } // time frame after which move block one block down
    public float GravityTimerLeft { get; private set; }


    public float stepDelay = 1f;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float lockTime;

    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData, float gravityTimer)
    {
        stepTime = s
        TetrominoData = tetrominoData;
        Board = board;
        Position = position;
        RotationIndex = 0;
        GravityTimer = gravityTimer;
        stepTime = Time.time + this.stepDelay;
        lockTime = 0f;

        // Initialize the cells array if it is null
        Cells ??= new Vector3Int[TetrominoData.Cells.Length];

        for (int i = 0; i < TetrominoData.Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)TetrominoData.Cells[i];
        }
    }

    private void Update()
    {
        Board.Clear(this);

        lockTime += Time.deltaTime;

        // automatically move block one square down after set amount of time
        if (GravityTimerLeft <= .0f)
        {
            Move(Vector2Int.down);
            GravityTimerLeft = GravityTimer;
        }

        GravityTimerLeft -= Time.deltaTime;

        // Get the game inputs from the player and move the piece
        GameInputs();

        if(Time.time >= this.stepTime)
        {
            Step();
        }

        Board.Set(this);
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;
        Move(Vector2Int.down);
        if(this.lockTime >= this.lockTime)
        {
            Lock();
        }
    }

    private void GameInputs()
    {
        // Move the piece to the left
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        // Move the piece to the right
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }

        // Move the piece down
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        // Hard drop the piece
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        // Rotate the piece to the left
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        // Rotate the piece to the right
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
    }

    private bool Move(Vector2Int direction)
    {
        Vector3Int newPosition = Position;
        newPosition.x += direction.x;
        newPosition.y += direction.y;

        bool isValidPosition = Board.IsValidPosition(this, newPosition);

        if (isValidPosition)
        {
            Position = newPosition;
        }

        return isValidPosition;
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            // Loop until the piece can't move down anymore
            continue;
        }
        Lock();
    }

    private void Lock()
    {
        Board.Set(this);
        Board.ClearLines();
        Board.SpawnRandomPiece();
    }

    // SRS rotation system (Super Rotation System)
    private void Rotate(int direction)
    {
        int oldRotationIndex = RotationIndex;
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        // if (TestWallKicks(RotationIndex, direction)) return;
        // RotationIndex = oldRotationIndex;
        // ApplyRotationMatrix(-direction);
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Vector3 cell = Cells[i];

            int x, y;

            switch (TetrominoData.Type)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt(cell.x * Data.RotationMatrix[0] * direction +
                                        cell.y * Data.RotationMatrix[1] * direction);
                    y = Mathf.CeilToInt(cell.x * Data.RotationMatrix[2] * direction +
                                        cell.y * Data.RotationMatrix[3] * direction);
                    break;

                default:
                    x = Mathf.RoundToInt(cell.x * Data.RotationMatrix[0] * direction +
                                         cell.y * Data.RotationMatrix[1] * direction);
                    y = Mathf.RoundToInt(cell.x * Data.RotationMatrix[2] * direction +
                                         cell.y * Data.RotationMatrix[3] * direction);
                    break;
            }

            Cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < TetrominoData.WallKicks.GetLength(1); i++)
        {
            Vector2Int wallKick = TetrominoData.WallKicks[wallKickIndex, i];

            if (Move(wallKick))
            {
                return true;
            }
        }

        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, TetrominoData.WallKicks.GetLength(0));
    }

    private static int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }

        return min + (input - min) % (max - min);
    }
}