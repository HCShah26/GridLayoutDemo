using TileSliderPuzzle.MVC.Enums;
using TileSliderPuzzle.MVC.Models;
using TileSliderPuzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;


namespace TileSliderPuzzle.MVC.Contollers
{
    
    public class TilePuzzleController()
    {

        private readonly TilePuzzleModel? _model = null;
        private readonly ContentPage _view = null;

        public TilePuzzleController(ContentPage view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            //_view = ContentPage TileSliderPuzzle.MainPage();
            _model = new TilePuzzleModel();
            //UpdateView(_model.GameGrid);
            InitialiseGame();
        }

        //public TilePuzzleController()
        //{
        //    _model = new TilePuzzleModel();
        //    InitialiseGame();
        //}

        //public TilePuzzleController(ContentPage view) : 
        //{
        //    _view = view ?? throw new ArgumentNullException(nameof(view));
        //}

        private void InitialiseGame()
        {
            _model.NewGame();
            UpdateView(_model.GameGrid);
        }

        public void ResetPuzzle()
        {
            _model.NewGame();
            UpdateView(_model.GameGrid);
        }

        public void RevealPuzzle(bool clickedReveal)
        {
            if (!clickedReveal)
            {
                UpdateView(_model.GameGrid);
                //lblWinStatus.Text = "";
            }
            else
            {
                UpdateView(_model.SolvedTileGrid);
                //lblWinStatus.Text = "Game Paused - Displaying Solution \nClick on the Right Arrow button to unpause";
            }
        }

        public void OnSwipe(int row, int col, MovementDirection direction)
        {
            //if (_model.IsMovable(row, col, direction))
            //{
            //    if (_model.CheckIfPuzzleSolved())
            //    {
            //        DisplayWinMessage();
            //    }
            //}
        }

        public void UndoLastMove()
        {
            _model.UndoMove();
            UpdateView(_model.GameGrid);
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

        private void DisplayWinMessage()
        {

        }
    }
}
