using TileSliderPuzzle.MVC.Enums;
using TileSliderPuzzle.MVC.Models;
using System.Diagnostics;
using TileSliderPuzzle.MVC.Structs;
using System.Security.Cryptography.X509Certificates;
using TileSliderPuzzle.MVC.Contollers;




namespace TileSliderPuzzle
{

    public partial class MainPage : ContentPage
    {
        public int randomValidMoves = 0;
        private bool clickedReveal = false;
        private static readonly Random _random = new Random();
        private static TilePuzzleModel _model;
        private readonly TilePuzzleController _controller;

        public MainPage()
        {
            InitializeComponent();
            //_model = new TilePuzzleModel();
            _controller = new TilePuzzleController(this);
        }

        private void DisplayGrid(Tile[,] thisGrid)
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
            switch (thisTile.Filename)
            {
                case "top_left":
                    tileGrid.SetRow(top_left, (int)thisTile.Row);
                    tileGrid.SetColumn(top_left, (int)thisTile.Column);
                    break;
                case "top_middle":
                    tileGrid.SetRow(top_middle, (int)thisTile.Row);
                    tileGrid.SetColumn(top_middle, (int)thisTile.Column);
                    break;
                case "top_right":
                    tileGrid.SetRow(top_right, (int)thisTile.Row);
                    tileGrid.SetColumn(top_right, (int)thisTile.Column);
                    break;
                case "centre_left":
                    tileGrid.SetRow(centre_top, (int)thisTile.Row);
                    tileGrid.SetColumn(centre_top, (int)thisTile.Column);
                    break;
                case "centre_middle":
                    tileGrid.SetRow(centre_middle, (int)thisTile.Row);
                    tileGrid.SetColumn(centre_middle, (int)thisTile.Column);
                    break;
                case "centre_right":
                    tileGrid.SetRow(centre_right, (int)thisTile.Row);
                    tileGrid.SetColumn(centre_right, (int)thisTile.Column);
                    break;
                case "bottom_left":
                    tileGrid.SetRow(bottom_left, (int)thisTile.Row);
                    tileGrid.SetColumn(bottom_left, (int)thisTile.Column);
                    break;
                case "bottom_middle":
                    tileGrid.SetRow(bottom_middle, (int)thisTile.Row);
                    tileGrid.SetColumn(bottom_middle, (int)thisTile.Column);
                    break;
                case "bottom_right":
                    tileGrid.SetRow(bottom_right, (int)thisTile.Row);
                    tileGrid.SetColumn(bottom_right, (int)thisTile.Column);
                    break;
            }
        }
        //private void ResetPuzzle()
        //{
            
        //    _model.NewGame();
        //    UpdateLabel();
        //}

        //private void UpdateLabel()
        //{
        //    lblSolved.Text = "";
        //    lblGame.Text = "";
        //    Tile gGrid, sGrid;
        //    foreach (int sRow in Enum.GetValues(typeof(GridRow)))
        //    {
        //        foreach (int sCol in Enum.GetValues(typeof(GridCol)))
        //        {
        //            sGrid = _model.SolvedTileGrid[sRow, sCol];
        //            gGrid = _model.GameGrid[sRow, sCol];
        //            lblSolved.Text = lblSolved.Text + $"{sGrid.ToString()}\t";
        //            lblGame.Text = lblGame.Text + $"{gGrid.ToString()}\t";
        //        }
        //        lblSolved.Text = lblSolved.Text + "\n";
        //        lblGame.Text = lblGame.Text + "\n";
        //    }
        //}

        void onNewGameClicked(object sender, EventArgs e)
        {
            _controller.ResetPuzzle();
            //DisplayGrid(_model.GameGrid);
            
            //lblWinStatus.Text = "";
        }

        //public void MoveTile(Image image, int xRow, int xCol, int mtRow, int mtCol)
        //{
        //    tileGrid.SetColumn(image, mtCol);
        //    tileGrid.SetRow(image, mtRow);
        //    tileGrid.SetColumn(top_left, xCol);
        //    tileGrid.SetRow(top_left, xRow);
        //}

        private void CheckIfPuzzleSolved()
        {
            bool result = _model.CheckIfPuzzleSolved();
            lblWinStatus.Text = result ? "You Win!" : string.Empty;
        }

        void onPrevClicked(object sender, EventArgs e) 
        {
            _controller.UndoLastMove();
            //_model.UndoMove();
            //DisplayGrid(_model.GameGrid);
            //UpdateLabel();
        }
        void onHintClicked(object sender, EventArgs e) 
        {
            clickedReveal = !clickedReveal;

            _controller.RevealPuzzle(clickedReveal);

            if (!clickedReveal)
            {
                //DisplayGrid(_model.GameGrid);
                lblWinStatus.Text = "";
            }
            else
            {
                //DisplayGrid(_model.SolvedTileGrid);
                lblWinStatus.Text = "Game Paused - Displaying Solution \nClick on the Right Arrow button to unpause";
            }
        }

       void OnSwiped(object sender, SwipedEventArgs e)
       {
            if (!clickedReveal)
            {
                var image = (Image)sender;
                int xRow = tileGrid.GetRow(image); int xCol = tileGrid.GetColumn(image);
                _model.MoveTile(_model.emptyTile, _model.GameGrid[xRow, xCol], (MovementDirection)e.Direction);
                DisplayGrid(_model.GameGrid);
                //UpdateLabel();
                CheckIfPuzzleSolved();
            }
        }

        void OnTapped(object sender, TappedEventArgs e)
        {
            //This function will check the following:
            // 1) if Tile tapped is not an empty tile
            // 2) determine the next postion to move

            if (!clickedReveal)
            {
                var image = (Image)sender;
                int xRow = tileGrid.GetRow(image); int xCol = tileGrid.GetColumn(image);
                _model.MoveTile(_model.emptyTile, _model.GameGrid[xRow, xCol]);
                DisplayGrid(_model.GameGrid);
                //UpdateLabel();
                CheckIfPuzzleSolved();
            }
        }
    }

}
