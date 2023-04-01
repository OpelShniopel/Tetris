using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }

    public int RotationIndex { get; private set; }

    [field: SerializeField] public float StepDelay { get; set; } = 1f;
    [field: SerializeField] public float MoveDelay { get; set; } = 0.1f;
    [field: SerializeField] public float LockDelay { get; set; } = 0.5f;

    private float _stepTime;
    private float _moveTime;
    private float _lockTime;

    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData)
    {
        TetrominoData = tetrominoData;
        Board = board;
        Position = position;
        RotationIndex = 0;

        _stepTime = Time.time + StepDelay;
        _moveTime = Time.time + MoveDelay;
        _lockTime = 0f;

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

        _lockTime += Time.deltaTime;

        // Get the game inputs from the player and move the piece
        HandleRotationInputs();
        
        // Allow the player to hold movement keys but only after a move delay
        // so it does not move too fast
        if (Time.time > _moveTime)
        {
            HandleMoveInputs();
        }

        // Advance the piece to the next row every x seconds
        if (Time.time > _stepTime)
        {
            Step();
        }

        Board.Set(this);
    }

    private void HandleMoveInputs()
    {
        // Soft drop movement
        if (Input.GetKey(KeyCode.S) && Move(Vector2Int.down))
        {
            // Update the step time to prevent double movement
            _stepTime = Time.time + StepDelay;
        }
        
        // Hard drop the piece
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        // Left/right movement
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
    }

    private void Step()
    {
        _stepTime = Time.time + StepDelay;

        // Step down to the next row
        Move(Vector2Int.down);

        // Once the piece has been inactive for too long it becomes locked
        if (_lockTime >= LockDelay)
        {
            Lock();
        }
    }

    private void HandleRotationInputs()
    {
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
            _moveTime = Time.time + MoveDelay;
            _lockTime = 0f; // Reset
        }
       

        return isValidPosition;
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            // Loop until the piece cannot move down anymore
        }

        Lock();
    }

    private void Lock()
    {
        Board.Set(this);
        Board.ClearLines();
        
        if (!Board.CheckGameOver())
        {
            Board.SpawnRandomPiece();
        }
    }

    // SRS rotation system (Super Rotation System)
    private void Rotate(int direction)
    {
        int oldRotationIndex = RotationIndex;
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        if (TestWallKicks(RotationIndex, direction)) return;
        RotationIndex = oldRotationIndex;
        ApplyRotationMatrix(-direction);
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