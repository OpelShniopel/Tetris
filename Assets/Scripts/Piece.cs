using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }
    public int RotationIndex { get; private set; }

    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData)
    {
        TetrominoData = tetrominoData;
        Board = board;
        Position = position;
        RotationIndex = 0;

        Cells ??= new Vector3Int[TetrominoData.Cells.Length]; // Initialize the cells array if it is null

        for (int i = 0; i < TetrominoData.Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)TetrominoData.Cells[i];
        }
    }

    private void Update()
    {
        Board.Clear(this);

        // Move the piece left or right
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }

        // Move the piece down
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
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

        Board.Set(this);
    }

    private void Lock()
    {
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
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
            continue;
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