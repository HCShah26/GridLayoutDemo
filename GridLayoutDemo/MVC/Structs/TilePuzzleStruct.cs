using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileSliderPuzzle.MVC.Enums;

namespace TileSliderPuzzle.MVC.Structs
{
    public struct GridTile
    {
        public GridTile(GridRow row, GridCol column, string file) 
        {
            Row = row;
            Column = column;
            Filename = file.ToLower();
        }

        public GridRow Row { get; }
        public GridCol Column { get; }

        public string Filename { get; }
        public override string ToString() => $"[{(int)Row},{(int)Column}] - {Filename}";
    }
}
