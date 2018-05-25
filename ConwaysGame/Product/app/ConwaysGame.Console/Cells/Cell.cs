using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ConwaysGame.Cells
{
    public class Cell
    {
        public bool Live { get; set; }

        public void ToggleCell()
        {
            Live = !Live;
        }
    }
}