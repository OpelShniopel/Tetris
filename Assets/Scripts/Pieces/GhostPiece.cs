using Tetris.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris.Pieces
{
    public class GhostPiece : MonoBehaviour
    {
        [field: SerializeField] public Tile GhostTile { get; set; }
        [field: SerializeField] public Board GameBoard { get; set; }
        [field: SerializeField] public Piece ActivePiece { get; set; }

        private Tilemap GhostTilemap { get; set; }
        private Vector3Int[] GhostCells { get; set; }
        private Vector3Int GhostPosition { get; set; }

        private void Awake()
        {
            GhostTilemap = GetComponentInChildren<Tilemap>();
            GhostCells = new Vector3Int[4];
        }

        private void LateUpdate()
        {
            ClearGhost();
            CopyActivePiece();
            DropGhost();
            SetGhost();
        }

        private void ClearGhost()
        {
            for (int i = 0; i < GhostCells.Length; i++)
            {
                Vector3Int tilePosition = GhostCells[i] + GhostPosition;
                GhostTilemap.SetTile(tilePosition, null);
            }
        }

        private void CopyActivePiece()
        {
            for (int i = 0; i < GhostCells.Length; i++)
            {
                GhostCells[i] = ActivePiece.Cells[i];
            }
        }

        private void DropGhost()
        {
            Vector3Int position = ActivePiece.Position;

            int current = position.y;
            int bottom = -GameBoard.BoardSize.y / 2 - 1;

            GameBoard.Clear(ActivePiece);

            for (int row = current; row >= bottom; row--)
            {
                position.y = row;
                if (GameBoard.IsValidPosition(ActivePiece, position))
                {
                    GhostPosition = position;
                }
                else
                {
                    break;
                }
            }

            GameBoard.Set(ActivePiece);
        }

        private void SetGhost()
        {
            for (int i = 0; i < GhostCells.Length; i++)
            {
                Vector3Int tilePosition = GhostCells[i] + GhostPosition;
                GhostTilemap.SetTile(tilePosition, GhostTile);
            }
        }
    }
}