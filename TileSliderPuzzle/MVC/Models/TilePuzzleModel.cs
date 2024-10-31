using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileSliderPuzzle.MVC.Enums;

namespace TileSliderPuzzle.MVC.Models
{
    class TilePuzzleModel
    {
        public Tile[,] GameGrid { get; private set; }
        public Tile[,] SolvedTileGrid { get; private set; }

        public Tile emptyTile; // Maintains location of empty tile

        private static readonly Random _random = new Random();

        private Stack<Dictionary<Tile, Tile>> moveHistory = new Stack<Dictionary<Tile, Tile>>();

        public TilePuzzleModel()
        {
            GameGrid = new Tile[3,3];
            SolvedTileGrid = ResetTileGrid();
            GameGrid = ResetTileGrid();
        }

        public bool CheckIfPuzzleSolved()
        {
            bool result = true;

            foreach (GridRow row in Enum.GetValues(typeof(GridRow)))
            {
                foreach (GridCol col in Enum.GetValues(typeof(GridCol)))
                {
                    result = result && (GameGrid[(int)row, (int)col].ToString() == SolvedTileGrid[(int)row, (int)col].ToString());
                }
            }
            return result;
        }

        private MovementDirection GetValidDirection(Tile currentTile, Tile targetTile)
        {
            if (targetTile.Row + 1 == currentTile.Row)
            { return MovementDirection.Down; }
            else
            if (targetTile.Row - 1 == currentTile.Row)
            { return MovementDirection.Up; }
            else
            if (targetTile.Column + 1 == currentTile.Column) 
            { return MovementDirection.Right; }
            else
            if (targetTile.Column - 1 == currentTile.Column)
            { return MovementDirection.Left; }
            else
            { 
              // Can't get to this point
              return MovementDirection.None; 
            }
        }

        private void MoveOnValidDirection(Tile currentTile, Tile targetTile, MovementDirection direction)
        {
            //This function will move a tile if the move is valid
            switch (direction)
            {
                case MovementDirection.Left:
                    if (targetTile.Row == currentTile.Row && targetTile.Column - 1 == currentTile.Column)
                    {
                        SwapTiles(currentTile, targetTile);
                    }
                    break;
                case MovementDirection.Right:
                    if (targetTile.Row == currentTile.Row && targetTile.Column + 1 == currentTile.Column)
                    {
                        SwapTiles(currentTile, targetTile);
                    }
                    break;
                case MovementDirection.Up:
                    if (targetTile.Row - 1 == currentTile.Row && targetTile.Column == currentTile.Column)
                    {
                        SwapTiles(currentTile, targetTile);
                    }
                    break;
                case MovementDirection.Down:
                    if (targetTile.Row + 1 == currentTile.Row && targetTile.Column == currentTile.Column)
                    {
                        SwapTiles(currentTile, targetTile);
                    }
                    break;
            }

        }

        public void MoveTile(Tile currentTile, Tile targetTile, MovementDirection direction = MovementDirection.None)
        {
            //First save current state in moveHistory
            SaveCurrentState(currentTile, targetTile);

            if (direction == MovementDirection.None)
            {
                MoveOnValidDirection(currentTile, targetTile, GetValidDirection(currentTile,targetTile));
            }
            else
            {
                MoveOnValidDirection(currentTile, targetTile, direction);     
            }
        }

        public void NewGame()
        {
            // This function sets up a new game by moving the tiles in a random order.

            moveHistory.Clear(); //Clear previous history from the stack

            // Starting point before setting random moves
            GameGrid = ResetTileGrid();


            // Move tile randomly but for valid moves only (You need this to maintain the order of the tiles)
            MoveTilesRandomly();
        }

        public void SetGameMoveTile(MovementDirection direction)
        {
            Tile nextTile = emptyTile; // Incase the move is not valid, it will swap the same GridCell

            int randomValidMoves = 0;

            // ToDo: Confirm and check if this code is needed
            string image = emptyTile.Filename; // Redundant code not needed
            int nextRow = -1; // Setting as an invalid value to begin with
            int nextCol = -1; // Setting as an invalid value to begin with

            switch (direction)
            {
                case MovementDirection.Left:
                    nextCol = (int)emptyTile.Column > 0 ? (int)emptyTile.Column - 1 : -1;
                    if (nextCol > -1)
                    {
                        nextRow = (int)emptyTile.Row;
                        nextTile = GameGrid[nextRow, nextCol];
                    }
                    break;
                case MovementDirection.Right:
                    nextCol = (int)emptyTile.Column < 2 ? (int)emptyTile.Column + 1 : -1;
                    if (nextCol > -1)
                    {
                        nextRow = (int)emptyTile.Row;
                        nextTile = GameGrid[nextRow, nextCol];
                    }
                    break;
                case MovementDirection.Up:
                    nextRow = (int)emptyTile.Row > 0 ? (int)emptyTile.Row - 1 : -1;
                    if (nextRow > -1)
                    {
                        nextCol = (int)emptyTile.Column;
                        nextTile = GameGrid[nextRow, nextCol];
                    }
                    break;
                case MovementDirection.Down:
                    nextRow = (int)emptyTile.Row < 2 ? (int)emptyTile.Row + 1 : -1;
                    if (nextRow > -1)
                    {
                        nextCol = (int)emptyTile.Column;
                        nextTile = GameGrid[nextRow, nextCol];
                    }
                    break;
            }

            if (nextRow > -1 && nextCol > -1)
            {
                SwapTiles(emptyTile, nextTile);
                randomValidMoves++;
            }
        }

        private static MovementDirection GetRandomMovement()
        {
            Array values = Enum.GetValues(typeof(MovementDirection));
            return (MovementDirection)values.GetValue(_random.Next(values.Length));
        }

        private void MoveTilesRandomly()
        {
            int randomMoves;

            randomMoves = _random.Next(4, 100);

            for (int i = 1; i <= randomMoves; i++)
            {
                //Select random movement
                SetGameMoveTile(GetRandomMovement());
            }
        }

        private Tile[,] ResetTileGrid()
        {
            Tile[,] resetTileGrid;
            resetTileGrid = new Tile[3, 3];
            string fileName = "";

            // This function sets the solved puzzle state
            foreach (GridRow SolvedRow in Enum.GetValues(typeof(GridRow)))
            {
                foreach (GridCol SolvedCol in Enum.GetValues(typeof(GridCol)))
                {
                    fileName = $"{SolvedRow}_{SolvedCol}";
                    resetTileGrid[(int)SolvedRow, (int)SolvedCol] = new Tile(SolvedRow, SolvedCol, fileName);
                }
            }
            emptyTile = resetTileGrid[0, 0];
            return resetTileGrid;
        }

        private void SaveCurrentState(Tile currentTile, Tile nextTile)
        {
            var currentState = new Dictionary<Tile, Tile>();
            currentState[currentTile] = nextTile;
            moveHistory.Push(currentState);
        }
        private void SwapTiles(Tile currentTile, Tile nextTile )
        {
            // This function swaps the two tiles passed as currentTile and nextTile
            // currentTile should alway be the empty tile!!!


            // Copy of current tile
            Tile copyTile = currentTile; 

            // Start the swap
            currentTile = new Tile(nextTile.Row, nextTile.Column, currentTile.Filename);
            nextTile = new Tile(copyTile.Row, copyTile.Column, nextTile.Filename);

            GameGrid[(int)currentTile.Row, (int)currentTile.Column] = currentTile;
            GameGrid[(int)nextTile.Row, (int)nextTile.Column] = nextTile;
            emptyTile = currentTile;
        }

        public void UndoMove()
        {
            Tile currentTile;
            Tile nextTile;

            if (moveHistory.Count > 0)
            {


                var previousState = moveHistory.Pop();

                foreach (var entry in previousState)
                {
                    currentTile = entry.Key;
                    nextTile = entry.Value;
                    GameGrid[(int)currentTile.Row, (int)currentTile.Column] = currentTile;
                    GameGrid[(int)nextTile.Row, (int)nextTile.Column] = nextTile;
                    emptyTile = currentTile;
                }
            }
        }


    }
}
