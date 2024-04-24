using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.src
{
    class GameOfLife
    {
        Board board { get; set;}

        public GameOfLife(Board board)
        {
            this.board = board;
        }

        public Board getBoard() { return board; }
        public void setBoard(Board board) {  this.board = board; }
        public void play()
        {
            this.board.nextGeneration();
            int size = this.board.cells.Length;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(this.board.cells[i, j]);
                }
                Console.Write("\n");
            }
        }
        public void editCell(int row, int col) 
        {
            this.board.cells[row,col].flip();
        }
        /*
        static void Main(string[] args)
        {
            Cell[,] cells ={    { new Cell(false,0,0), new Cell(false,0,1), new Cell(false,0,2) },
                                { new Cell(false,1,0), new Cell(false,1,1), new Cell(false,1,2) },
                                { new Cell(false,2,0), new Cell(false,2,1), new Cell(false,2,2) } };
            GameOfLife game = new GameOfLife(new Board(cells));
            for (int i = 0;i < 4;i++)
            {
                game.play();
            }
        }
        */
    }
    
    



}
