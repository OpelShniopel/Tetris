using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }
    public int RotationIndex {  get; private set; }
    
    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData)
    {
        TetrominoData = tetrominoData;
        Board = board;
        Position = position;
        RotationIndex = 0;
        
        if (Cells == null)
        {
            Cells = new Vector3Int[TetrominoData.Cells.Length]; // 4 when using the default tetrominoes
        }
        
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

        // Rotate the piece
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
        
        Board.Set(this);
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
            // Do nothing
        }
    }

    // SRS rotation system (Super Rotation System)
    private void Rotate(int direction)
    {
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);
        
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
                    x = Mathf.CeilToInt(cell.x * Data.RotationMatrix[0] * direction + cell.y * Data.RotationMatrix[1] * direction);
                    y = Mathf.CeilToInt(cell.x * Data.RotationMatrix[2] * direction + cell.y * Data.RotationMatrix[3] * direction);
                    break;
                
                default:
                    x = Mathf.RoundToInt(cell.x * Data.RotationMatrix[0] * direction + cell.y * Data.RotationMatrix[1] * direction);
                    y = Mathf.RoundToInt(cell.x * Data.RotationMatrix[2] * direction + cell.y * Data.RotationMatrix[3] * direction);
                    break;
            }
            
            Cells[i] = new Vector3Int(x, y, 0);
        }
    }

    // Wrap the input between the min and max values
    private static int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }

        return min + (input - min) % (max - min);
    }
}
