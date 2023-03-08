using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap Tilemap { get; private set; } // The Tilemap component of the child object
    public Piece CurrentPiece { get; private set; } // The Piece component of the child object
    [field: SerializeField] public TetrominoData[] Tetrominoes { get; set; } // The tetromino data for each tetromino
    [field: SerializeField] public Vector3Int SpawnPosition { get; set; } // The position where the tetrominoes will spawn
    [field: SerializeField] public Vector2Int BoardSize { get; set;}
    
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2);
            return new RectInt(position, BoardSize);
        }
    }
    
    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>(); // Get the Tilemap component from the child object
        CurrentPiece = GetComponentInChildren<Piece>(); // Get the Piece component from the child object

        // Initialize the tetromino data for each tetromino
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
        int randomIndex = Random.Range(0, Tetrominoes.Length);
        TetrominoData data = Tetrominoes[randomIndex];
        
        CurrentPiece.Initialize(this, SpawnPosition, data); // Initialize the piece
        Set(CurrentPiece); // Set the piece on the board
    }

    /// <summary>
    /// Sets the piece on the board
    /// </summary>
    /// <param name="piece"></param>
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position; // Get the tile position
            Tilemap.SetTile(tilePosition, piece.TetrominoData.Tile); // Set the tile
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
}