using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.src
{
    class Board
    {
        public Cell[,] cells { get; set; }
        int size;

        public Board(Cell[,] cells, int size)
        {
            this.cells = cells;
            this.size = size;
        }
        private int normalizeIndex(int index)
        {

            return (index + size) % size; //  magic to make the board "infinite".
        }

        private int getNumOfNeighbours(Cell cell)
        {
            int numOfNeighbours = 0;
            /*  od -1 +1 do +1 -1
             *  n n n
             *  n c n 
             *  n n n
             * */
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int normalizedY = normalizeIndex(cell.getY() + i);
                    int normalizedX = normalizeIndex(cell.getX() + j);
                    if (cells[normalizedY, normalizedX].getIsAlive() && normalizedY != 0 && normalizedX != 0)
                    {
                        numOfNeighbours++;
                    }
                }
            }
            return numOfNeighbours;
        }

        private bool updateCell(Cell cell)
        {
            bool isAlive = cell.getIsAlive();
            int neighbours = getNumOfNeighbours(cell);
            if (cell.getIsAlive())
            {
                switch (neighbours)
                {
                    // Any live cell with two or three live neighbours lives on to the next generation.
                    case 2:
                    case 3:
                        return isAlive;
                    // Any live cell with fewer than two live neighbours dies, as if caused by under-population.
                    // Any live cell with more than three live neighbours dies, as if by over-population.
                    default:
                        return !isAlive;
                }
            }
            else
            {
                // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                if (neighbours == 3)
                {
                    return !isAlive;
                }
            }
            return isAlive;
        }

        public void nextGeneration()
        {
            Cell[,] newBoard = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bool updatedState = updateCell(cells[i, j]);
                    Cell updatedCell = new Cell(updatedState, i, j);
                    newBoard[i, j] = updatedCell;
                }
            }
            cells = newBoard;
        }

    }

}