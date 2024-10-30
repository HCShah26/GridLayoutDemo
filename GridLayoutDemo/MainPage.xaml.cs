using TileSliderPuzzle.MVC.Enums;
using TileSliderPuzzle.MVC.Models;
using System.Diagnostics;
using TileSliderPuzzle.MVC.Structs;
using System.Security.Cryptography.X509Certificates;




namespace TileSliderPuzzle
{

    public partial class MainPage : ContentPage
    {
        public int randomValidMoves = 0;
        private bool clickedReveal = false;
        private static readonly Random _random = new Random();
        private static TilePuzzleModel _model;
        public MainPage()
        {
            InitializeComponent();
            _model = new TilePuzzleModel();
        }

        private void DisplayGridM(GridTile[,] thisGrid)
        {
            foreach (int thisRow in Enum.GetValues(typeof(GridRow)))
            {
                foreach(int thisCol in Enum.GetValues( typeof(GridCol)))
                {
                    SetImageInCell(thisGrid[thisRow, thisCol]);
                }
            }
        }

        private void SetImageInCell(GridTile thisTile)
        {
            switch (thisTile.Filename)
            {
                case "top_left":
                    tileGrid.SetRow(image1, (int)thisTile.Row);
                    tileGrid.SetColumn(image1, (int)thisTile.Column);
                    break;
                case "top_middle":
                    tileGrid.SetRow(image2, (int)thisTile.Row);
                    tileGrid.SetColumn(image2, (int)thisTile.Column);
                    break;
                case "top_right":
                    tileGrid.SetRow(image3, (int)thisTile.Row);
                    tileGrid.SetColumn(image3, (int)thisTile.Column);
                    break;
                case "centre_left":
                    tileGrid.SetRow(image4, (int)thisTile.Row);
                    tileGrid.SetColumn(image4, (int)thisTile.Column);
                    break;
                case "centre_middle":
                    tileGrid.SetRow(image5, (int)thisTile.Row);
                    tileGrid.SetColumn(image5, (int)thisTile.Column);
                    break;
                case "centre_right":
                    tileGrid.SetRow(image6, (int)thisTile.Row);
                    tileGrid.SetColumn(image6, (int)thisTile.Column);
                    break;
                case "bottom_left":
                    tileGrid.SetRow(image7, (int)thisTile.Row);
                    tileGrid.SetColumn(image7, (int)thisTile.Column);
                    break;
                case "bottom_middle":
                    tileGrid.SetRow(image8, (int)thisTile.Row);
                    tileGrid.SetColumn(image8, (int)thisTile.Column);
                    break;
                case "bottom_right":
                    tileGrid.SetRow(image9, (int)thisTile.Row);
                    tileGrid.SetColumn(image9, (int)thisTile.Column);
                    break;
            }
        }
        private void ResetPuzzle()
        {
            
            _model.NewGame();
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            lblSolved.Text = "";
            lblGame.Text = "";
            GridTile gGrid, sGrid;
            foreach (int sRow in Enum.GetValues(typeof(GridRow)))
            {
                foreach (int sCol in Enum.GetValues(typeof(GridCol)))
                {
                    sGrid = _model.SolvedTileGrid[sRow, sCol];
                    gGrid = _model.GameGrid[sRow, sCol];
                    lblSolved.Text = lblSolved.Text + $"{sGrid.ToString()}\t";
                    lblGame.Text = lblGame.Text + $"{gGrid.ToString()}\t";
                }
                lblSolved.Text = lblSolved.Text + "\n";
                lblGame.Text = lblGame.Text + "\n";
            }
        }

        void onRandomizeClicked(object sender, EventArgs e)
        {
            ResetPuzzle();
            DisplayGridM(_model.GameGrid);
            
            lblWinStatus.Text = "";
        }


        public void MoveTile(Image image, int xRow, int xCol, int mtRow, int mtCol)
        {
            tileGrid.SetColumn(image, mtCol);
            tileGrid.SetRow(image, mtRow);
            tileGrid.SetColumn(image1, xCol);
            tileGrid.SetRow(image1, xRow);
        }

        //private bool CheckTile(int xRow, int xCol, Image correctImage)
        //{
        //    Image image;
        //    image = GetElementAt(xRow, xCol);
        //    return image.Source == correctImage.Source;
        //}

        private void CheckIfPuzzleSolved()
        {
            bool result = _model.CheckIfPuzzleSolved();
            lblWinStatus.Text = result ? "You Win!" : string.Empty;
        }

        //public Image GetElementAt(int row, int col)
        //{
        //    foreach (var child in tileGrid.Children)
        //    {
        //        if (tileGrid.GetRow(child) == row && tileGrid.GetColumn(child) == col)
        //        {
        //            return (Image)child; // Found image, exiting loop
        //        }
        //    }
        //    return null; // Returns null as no image found at specified location (This will never happen!)
        //}

        void onPrevClicked(object sender, EventArgs e) 
        {
            _model.UndoMove();
            DisplayGridM(_model.GameGrid);
            UpdateLabel();
        }
        void onNextClicked(object sender, EventArgs e) 
        {
            clickedReveal = !clickedReveal;

            if (!clickedReveal)
            {
                DisplayGridM(_model.GameGrid);
                lblWinStatus.Text = "";
            }
            else
            {
                DisplayGridM(_model.SolvedTileGrid);
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
                DisplayGridM(_model.GameGrid);
                UpdateLabel();
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
                DisplayGridM(_model.GameGrid);
                UpdateLabel();
                CheckIfPuzzleSolved();
            }
        }
    }

}
