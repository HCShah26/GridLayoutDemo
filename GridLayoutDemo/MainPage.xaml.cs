using System.Diagnostics;

namespace GridLayoutDemo
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

       void OnSwiped(object sender, SwipedEventArgs e)
        {
            //Debug.WriteLine("****************************************");
            
            var image = (Image)sender;
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
        }
    }

}
