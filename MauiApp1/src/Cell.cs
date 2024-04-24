using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace MauiApp1.src
{
    class Cell
    {

        private bool isAlive;
        private int y;
        private int x;

        public Cell(bool state, int y, int x)
        {
            isAlive = state;
            this.y = y;
            this.x = x;
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
        public bool getIsAlive()
        {
            return isAlive;
        }
        public static Cell alive(int columnIndex, int rowIndex)
        {
            return new Cell(true, columnIndex, rowIndex);
        }

        public static Cell dead(int columnIndex, int rowIndex)
        {
            return new Cell(false, columnIndex, rowIndex);
        }
        public void flip()
        {
            isAlive = !isAlive;
        }
    }
}
