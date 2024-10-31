using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileSliderPuzzle.MVC.Enums;

namespace TileSliderPuzzle.MVC.Models
{
    public class Tile(GridRow row, GridCol column, string filename)
    {
        public GridRow Row { get; set; } = row;
        public GridCol Column { get; set; } = column;
        public string Filename { get; set; } = filename.ToLower();
        public override string ToString() => $"[{(int)Row},{(int)Column}] - {Filename}";
    }
}
