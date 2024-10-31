using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TileSliderPuzzle.MVC.Enums;
using TileSliderPuzzle.MVC.Models;
using Microsoft.Maui.Controls;

namespace TileSliderPuzzle.MVC.Controllers
{
    public class TileController
    {
        public bool gameStarted = false;
        private readonly TilePuzzleModel? _model = null;
        private readonly ContentPage _view = null;

        public TileController(ContentPage view) 
        {
            _view = view;
            _model = new TilePuzzleModel();
            InitialiseGame();

        }

        private void InitialiseGame()
        {
            UpdateView(_model.GameGrid);
        }

        public string CheckIfPuzzleSolved()
        {
            bool result = _model.CheckIfPuzzleSolved() & gameStarted;
            return result ? "You Win!" : string.Empty;
        }

        public void MoveTile(Image image, int xRow, int xCol, MovementDirection direction = MovementDirection.None)
        {
            _model.MoveTile(_model.emptyTile, _model.GameGrid[xRow, xCol], direction);
            UpdateView(_model.GameGrid);
        }

        public void ResetPuzzle()
        {
            _model.NewGame();
            UpdateView(_model.GameGrid);
            gameStarted = true;
        }

        public string RevealPuzzle(bool clickedReveal)
        {
            if (!clickedReveal)
            {
                UpdateView(_model.GameGrid);
                return "";
            }
            else
            {
                UpdateView(_model.SolvedTileGrid);
                return "Game Paused - Displaying Solution \nClick on the Right Arrow button to unpause";
            }
        }

        public void OnSwipe(Image image, Grid tileGrid, MovementDirection direction)
        {
            int xRow = tileGrid.GetRow(image); int xCol = tileGrid.GetColumn(image);
            MoveTile(image, xRow, xCol, direction);
            UpdateView(_model.GameGrid);
        }

        public void OnTap(Image image, Grid tileGrid)
        {
            int xRow = tileGrid.GetRow(image); int xCol = tileGrid.GetColumn(image);
            MoveTile(image, xRow, xCol);
            UpdateView(_model.GameGrid);
        }

        public void UndoLastMove()
        {
            _model.UndoMove();
            UpdateView(_model.GameGrid);
        }

        public (string solvedStr, string gameStr) UpdateLabel(bool debugMode)
        {
            // This function displays the state of the Solved puzzle and Game puzzle 
            // in text to help check for any issue. The debugMode must be on for this 
            // function to execute

            string lblSolved_Text = "";
            string lblGame_Text = "";

            if ((debugMode == true))
            {
                Tile gGrid, sGrid;
                foreach (int sRow in Enum.GetValues(typeof(GridRow)))
                {
                    foreach (int sCol in Enum.GetValues(typeof(GridCol)))
                    {
                        sGrid = _model.SolvedTileGrid[sRow, sCol];
                        gGrid = _model.GameGrid[sRow, sCol];
                        lblSolved_Text = lblSolved_Text + $"{sGrid.ToString()}\t";
                        lblGame_Text = lblGame_Text + $"{gGrid.ToString()}\t";
                    }
                    lblSolved_Text = lblSolved_Text + "\n";
                    lblGame_Text = lblGame_Text + "\n";
                }
            }
            return (lblSolved_Text, lblGame_Text);
        }


        private void UpdateView(Tile[,] thisGrid)
        {
            foreach (int thisRow in Enum.GetValues(typeof(GridRow)))
            {
                foreach (int thisCol in Enum.GetValues(typeof(GridCol)))
                {
                    SetImageInCell(thisGrid[thisRow, thisCol]);
                }
            }
        }

        private void SetImageInCell(Tile thisTile)
        {
            var tileGrid = _view.FindByName<Grid>("tileGrid");
            var image = _view.FindByName<Image>(thisTile.Filename);
            switch (thisTile.Filename)
            {
                case "top_left":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "top_middle":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "top_right":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "centre_left":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "centre_middle":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "centre_right":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "bottom_left":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "bottom_middle":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
                case "bottom_right":
                    tileGrid.SetRow(image, (int)thisTile.Row);
                    tileGrid.SetColumn(image, (int)thisTile.Column);
                    break;
            }
        }
    }
}