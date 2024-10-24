
using System.Diagnostics;


namespace GridLayoutDemo
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            DisplayGrid();
        }

        void onRandomizeClicked(object sender, EventArgs e) 
        { 
            Debug.WriteLine("Randomise clicked"); 
        }
        void onPrevClicked(object sender, EventArgs e) 
        { 
            Debug.WriteLine("Previous clicked"); 
        }
        void onNextClicked(object sender, EventArgs e) 
        { 
            Debug.WriteLine("Next clicked"); 
        }

       void OnSwiped(object sender, SwipedEventArgs e)
       {
            //Debug.WriteLine("****************************************");
            
            var image = (Image)sender;
            var emptyImage = Pos00;
            int xRow = myGrid.GetRow(image); int xCol = myGrid.GetColumn(image);
            int mtRow = myGrid.GetRow(emptyImage); int mtCol = myGrid.GetColumn(emptyImage);
            //Debug.WriteLine(myGrid.GetColumn(image));
            
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    if (xRow == mtRow && xCol - 1 == mtCol)
                    {
                        myGrid.SetColumn(image, mtCol);
                        myGrid.SetRow(image, mtRow);
                        myGrid.SetColumn(emptyImage, xCol);
                        myGrid.SetRow(emptyImage, xRow);
                    }
                    break;
                case SwipeDirection.Right:
                    if (xRow == mtRow && xCol + 1 == mtCol)
                    {
                        myGrid.SetColumn(image, mtCol);
                        myGrid.SetRow(image, mtRow);
                        myGrid.SetColumn(emptyImage, xCol);
                        myGrid.SetRow(emptyImage, xRow);
                    }
                    break;
                case SwipeDirection.Up:
                    if (xRow == mtRow +1 && xCol == mtCol)
                    {
                        myGrid.SetColumn(image, mtCol);
                        myGrid.SetRow(image, mtRow);
                        myGrid.SetColumn(emptyImage, xCol);
                        myGrid.SetRow(emptyImage, xRow);
                    }
                    break;   
                case SwipeDirection.Down:
                    if (xRow == mtRow -1 && xCol == mtCol)
                    {
                        myGrid.SetColumn(image, mtCol);
                        myGrid.SetRow(image, mtRow);
                        myGrid.SetColumn(emptyImage, xCol);
                        myGrid.SetRow(emptyImage, xRow);
                    }
                    break;  
            }
            DisplayGrid();
        }

        void DisplayGrid()
        {
          
            foreach(var child in myGrid.Children) 
            {
                int childRow = myGrid.GetRow(child);
                int childCol = myGrid.GetColumn(child);
                Image gridImage = (Image)child;
                Debug.WriteLine($" {gridImage.GetType().GetProperty("Source").GetValue(gridImage)?.ToString() ?? string.Empty}, Row = {childRow}, Column = {childCol}");

            }

            Image thisImage = (Image)myGrid.Children[0];
            Debug.WriteLine($"{thisImage.GetType().GetProperty("Source").GetValue(thisImage)?.ToString() ?? string.Empty}");

                //, Row = { Grid.GetRow(child)}, Col = { Grid.GetColumn(child)}"
            
        }
    }

}
