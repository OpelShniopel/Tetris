using Tetris.Pieces;
using Tetris.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Tetris.Core
{
    public class Board : MonoBehaviour
    {
        // The tetromino data for each tetromino
        [field: SerializeField] public TetrominoData[] Tetrominoes { get; set; }

        // The position where the tetrominoes will spawn. Default is (-1, 8, 0).
        [field: SerializeField] public Vector3Int SpawnPosition { get; set; } = new(-1, 8, 0);

        // The size of the board. Default is 10x20.
        [field: SerializeField] public Vector2Int BoardSize { get; set; } = new(10, 20);

        [field: SerializeField] public HealthBar Health { get; set; }

        // The Tilemap component of the child object
        private Tilemap Tilemap { get; set; }

        // The Piece component of the child object
        private Piece CurrentPiece { get; set; }

        private RectInt Bounds
        {
            get
            {
                // Calculate the position of the board's bottom left corner.
                Vector2Int position = new(-BoardSize.x / 2, -BoardSize.y / 2);
                // Return a rectangle with the calculated position and the board's size.
                return new RectInt(position, BoardSize);
            }
        }

        /// <summary>
        ///     Awake is used to initialize any variables or game state before the game starts.
        /// </summary>
        private void Awake()
        {
            Tilemap = GetComponentInChildren<Tilemap>(); // Get the Tilemap component from the child object.
            CurrentPiece = GetComponentInChildren<Piece>(); // Get the Piece component from the child object.
            InitializeTetrominoes(); // Initialize the tetromino data for each tetromino.
        }

        private void Start()
        {
            SpawnRandomPiece();
        }

        private void InitializeTetrominoes()
        {
            for (int i = 0; i < Tetrominoes.Length; i++)
            {
                Tetrominoes[i].Initialize();
            }
        }

        /// <summary>
        ///     Spawns a random tetromino piece
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
        ///     Sets the piece on the board
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
                    linesCleared++;
                    LineClear(row);
                }
                else
                {
                    row++;
                }
            }

            if (linesCleared > 0)
            {
                ScoreManager.Instance.AddScore(linesCleared);
            }
        }

        private bool IsLineFull(int row)
        {
            RectInt boardBounds = Bounds;

            for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
            {
                Vector3Int tilePosition = new(columnIndex, row, 0);

                if (!Tilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }

            return true;
        }

        private void LineClear(int row)
        {
            RectInt bounds = Bounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new(col, row, 0);
                Tilemap.SetTile(position, null);
            }

            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new(col, row + 1, 0);
                    TileBase above = Tilemap.GetTile(position);

                    position = new Vector3Int(col, row, 0);
                    Tilemap.SetTile(position, above);
                }

                row++;
            }
        }

        private void ClearBoard()
        {
            RectInt boardBounds = Bounds;

            for (int rowIndex = boardBounds.yMin; rowIndex < boardBounds.yMax; rowIndex++)
            {
                for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
                {
                    Vector3Int position = new(columnIndex, rowIndex, 0);
                    Tilemap.SetTile(position, null);
                }
            }
        }

        public bool CheckGameOver()
        {
            if (!IsGameOver()) // Check if the game is over.
            {
                return false; // No game over.
            }

            GameOver(); // Game over.
            return true;
        }

        private bool IsGameOver()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            return sceneName switch
            {
                "LifeTetris" => IsLifeGameOver(),
                "EndlessTetris" => IsEndlessGameOver(),
                "Tetris" => IsNormalGameOver(),
                "MultiPlayerTetris" => IsNormalGameOver(),
                _ => false
            };
        }

        private bool IsLifeGameOver()
        {
            RectInt boardBounds = Bounds; // Get the boundaries of the board.
            int spawnRow = SpawnPosition.y; // Get the row index where the pieces spawn.

            for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
            {
                // Get the tile position at the spawn row and current column.
                Vector3Int tilePosition = new(columnIndex, spawnRow, 0);

                // If there's no tile at the tile position, skip to the next iteration.
                if (!Tilemap.HasTile(tilePosition))
                {
                    continue;
                }

                ClearBoard(); // Clear the board of all tiles.
                Health.Damage(); // Damage the player.

                return Health.Health < 1; // Return true if the player is out of health, else return false.
            }

            return false; // No game over.
        }

        private bool IsEndlessGameOver()
        {
            RectInt boardBounds = Bounds; // Get the boundaries of the board.
            int spawnRow = SpawnPosition.y; // Get the row index where the pieces spawn.

            for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
            {
                // Get the tile position at the spawn row and current column.
                Vector3Int tilePosition = new(columnIndex, spawnRow, 0);

                // If there's no tile at the tile position, skip to the next iteration.
                if (!Tilemap.HasTile(tilePosition))
                {
                    continue;
                }

                ClearBoard(); // Clear the board of all tiles.
            }

            return false; // No game over.
        }

        private bool IsNormalGameOver()
        {
            RectInt boardBounds = Bounds; // Get the boundaries of the board.
            int spawnRow = SpawnPosition.y; // Get the row index where the pieces spawn.

            for (int columnIndex = boardBounds.xMin; columnIndex < boardBounds.xMax; columnIndex++)
            {
                // Get the tile position at the spawn row and current column.
                Vector3Int tilePosition = new(columnIndex, spawnRow, 0);

                // If there's no tile at the tile position, skip to the next iteration.
                if (!Tilemap.HasTile(tilePosition))
                {
                    continue;
                }

                return true; // Game over.
            }

            return false; // No game over.
        }

        private static void GameOver()
        {
            // Load the Game Over scene
            SceneManager.LoadScene("GameOver");
        }
    }
}