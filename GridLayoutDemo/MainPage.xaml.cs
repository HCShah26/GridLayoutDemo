using TileSliderPuzzle.MVC.Enums;
using TileSliderPuzzle.MVC.Controllers;
using TileSliderPuzzle.MVC.Models;

using System.Diagnostics;

namespace TileSliderPuzzle
{

    public partial class MainPage : ContentPage
    {
        private bool debugMode = false;
        private bool clickedReveal = false;
        private readonly TileController _controller;
        public MainPage()
        {
            InitializeComponent();
            _controller = new TileController(this);
        }

        private void UpdateLabel()
        {
            var result = _controller.UpdateLabel(debugMode);
            lblSolved.Text = result.solvedStr;
            lblGame.Text = result.gameStr;
        }

        void onNewGameClicked(object sender, EventArgs e)
        {
            _controller.ResetPuzzle();
            
            lblWinStatus.Text = "";
        }


        private async void CheckIfPuzzleSolved()
        {
            lblWinStatus.Text = _controller.CheckIfPuzzleSolved();
            if (lblWinStatus.Text == "You Win!")
            {
                
                //await ConfettiEffect();
                for (int i = 0; i < 10; i++)
                {
                    await CelebrateWithTextBoxScale();
                    await CelebrateSolveWithColor();
                }
            }
        }

        void onPrevClicked(object sender, EventArgs e) 
        {
            _controller.UndoLastMove();
            UpdateLabel();
        }
        void onRevealClicked(object sender, EventArgs e) 
        {
            clickedReveal = !clickedReveal;
            lblWinStatus.Text = _controller.RevealPuzzle(clickedReveal);
        }

       async void OnSwiped(object sender, SwipedEventArgs e)
       {
            if (!clickedReveal)
            {
                await ShakeEffect((Image)sender);
                _controller.OnSwipe((Image)sender, tileGrid, (MovementDirection)e.Direction);
                UpdateLabel();
                CheckIfPuzzleSolved();
            }
        }

        async void OnTapped(object sender, TappedEventArgs e)
        {
            //This function will check the following:
            // 1) if Tile tapped is not an empty tile
            // 2) determine the next postion to move

            if (!clickedReveal)
            {
                await ShakeEffect((Image)sender);
                _controller.OnTap((Image)sender, tileGrid);
                UpdateLabel();
                CheckIfPuzzleSolved();
            }
        }

        private async Task ShakeEffect(Image image)
        {
            await image.TranslateTo(-10, 0, 50); // Move left
            await image.TranslateTo(10, 0, 50); // Move right
            await image.TranslateTo(-5, 0, 50); // Move left slightly
            await image.TranslateTo(0, 0, 50); // Return to center
        }

        private async Task ConfettiEffect()
        {
            for (int i = 0; i < 10; i++) // Create multiple small shapes
            {
                var confetti = new BoxView { Color = Colors.Orange, WidthRequest = 10, HeightRequest = 10 };
                tileGrid.Children.Add(confetti);
                await confetti.TranslateTo(100, 200, 500); // Move the confetti downward
                tileGrid.Children.Remove(confetti);
            }
        }

        private async Task CelebrateWithTextBoxScale()
        {
            // Display the TextBox if hidden
            lblWinStatus.IsVisible = true;

            // Scale up and then scale back down
            await lblWinStatus.ScaleTo(2, 200, Easing.CubicInOut); // Slight zoom in
            await lblWinStatus.ScaleTo(1.0, 200, Easing.CubicInOut); // Zoom back to original size
        }

        private async Task CelebrateSolveWithColor()
        {
            var originalColor = this.BackgroundColor;
            this.BackgroundColor = Colors.LightGreen; // Success color
            await Task.Delay(300); // Short delay to show effect
            this.BackgroundColor = originalColor; // Revert to original color
        }
    }

}
