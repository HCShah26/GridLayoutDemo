using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileSliderPuzzle.MVC.Enums
{
    public enum TileGrid
    {
        Top_Left = 0, // Will always be empty tile in our solution
        Top_Middle = 1,
        Top_Right = 2,
        Centre_Left = 3,
        Centre_Middle = 4,
        Centre_Right = 5,
        Bottom_Left = 6,
        Bottom_Middle = 7,
        Bottom_Right = 8
    }

    public enum GridRow
    {
        Top = 0,
        Centre = 1, 
        Bottom = 2
    }

    public enum GridCol
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    public enum MovementDirection
    {
        None = 0,
        Right = 1,
        Left = 2,
        Up = 4,
        Down = 8
    }
}
