using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    // The Tilemap component of the child object
    public Tilemap Tilemap { get; private set; }

    // The Piece component of the child object
    public Piece CurrentPiece { get; private set; }

    // The tetromino data for each tetromino
    [field: SerializeField] public TetrominoData[] Tetrominoes { get; set; }

    // The position where the tetrominoes will spawn
    [field: SerializeField] public Vector3Int SpawnPosition { get; set; }

    // The size of the board
    [field: SerializeField] public Vector2Int BoardSize { get; set; }
    
    // TODO:
    public ScoreManager ScoreManager { get; private set; }
    

    public RectInt Bounds
    {
        get
        {
            // Calculate the position of the board's bottom left corner.
            Vector2Int position = new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2);
            // Return a rectangle with the calculated position and the board's size.
            return new RectInt(position, BoardSize);
        }
    }

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>(); // Get the Tilemap component from the child object.
        CurrentPiece = GetComponentInChildren<Piece>(); // Get the Piece component from the child object.
        InitializeTetrominoes(); // Initialize the tetromino data for each tetromino.
    }

    private void InitializeTetrominoes()
    {
        for (int i = 0; i < Tetrominoes.Length; i++)
        {
            Tetrominoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnRandomPiece();
    }

    /// <summary>
    /// Spawns a random tetromino piece
    /// </summary>
    public void SpawnRandomPiece()
    {
        // Randomly choose a piece
        int randomIndex = Random.Range(0, Tetrominoes.Length);
        TetrominoData data = Tetrominoes[randomIndex];

        // Initialize the piece at the spawn position
        CurrentPiece.Initialize(this, SpawnPosition, data);

        // Set the piece on the board
        Set(CurrentPiece);
    }

    /// <summary>
    /// Sets the piece on the board
    /// </summary>
    /// <param name="piece"></param>
    public void Set(Piece piece)
    {
        // Loop through each cell in the piece
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            // Get the tile position
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;

            // Set the tile
            Tilemap.SetTile(tilePosition, piece.TetrominoData.Tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position; // Get the tile position
            Tilemap.SetTile(tilePosition, null); // Clear the tile
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position; // Get the tile position

            // Check if the tile position is already occupied
            if (Tilemap.HasTile(tilePosition))
            {
                return false;
            }

            // Check if the tile position is outside the bounds of the board
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;
        int linesCleared = 0;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                linesCleared++;
            }
            else
            {
                row++;
            }
        }
        
        // TODO: Updates current score if lines are cleared
        // if (linesCleared > 0)
        // {
        //     ScoreManager.AddScore(linesCleared);
        // }
    }

    private bool IsLineFull(int row)
    {
        RectInt boardBounds = Bounds;

        for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, row, 0);

            if (!Tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            Tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.Tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                Tilemap.SetTile(position, above);
            }

            row++;
        }
    }
    
    public bool CheckGameOver()
    {
        RectInt boardBounds = Bounds;
        int spawnRow = SpawnPosition.y;
        bool isGameOver = false;
        
        for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, spawnRow, 0);

            if (!Tilemap.HasTile(tilePosition)) continue;
            GameOver();
                
            isGameOver = true;
            break;
        }
        
        return isGameOver;
    }
    
    private void GameOver()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("GameOver");
    }
}

