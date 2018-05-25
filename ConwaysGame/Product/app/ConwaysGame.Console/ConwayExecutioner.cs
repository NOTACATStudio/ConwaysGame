using System;
using System.Collections.Generic;
using System.Linq;
using ConwaysGame.Cells;

namespace ConwaysGame.Console
{
    class ConwayExecutioner
    {
        //private static int _xLength = 75;
        //private static int _yLength = 50;
        private static readonly int _xLength = 115;
        private static readonly int _yLength = 50;

        public bool[,] SetupView()
        {
            var random = new Random();
            bool[,] cells = new bool[_xLength, _yLength];
            for (var y = 0; y < _yLength; y++)
            {
                for (var x = 0; x < _xLength; x++)
                {
                    cells[x, y] = new bool();
                    cells[x, y] = RandLive(random);
                }
            }

            //SetupCellsForGame(cells);
            return cells;
        }

        public bool[,] SetupBlankCells()
        {
            return new bool[_xLength, _yLength];
        }

        public bool RandLive(Random random)
        {
            var ran = random.Next(2);
            var blah = ran > 0;
            return blah;
        }

        public void SetupCellsForGame(bool[,] cells)
        {
            cells[2, 1] = true;
            cells[1, 2] = true;
            cells[1, 3] = true;
            cells[2, 4] = true;
            cells[3, 2] = true;
            cells[3, 3] = true;

            cells[40, 40] = true;
            cells[41, 41] = true;
            cells[41, 42] = true;
            cells[42, 40] = true;
            cells[42, 41] = true;
            //
            cells[30, 30] = true;
            cells[31, 31] = true;
            cells[31, 32] = true;
            cells[32, 30] = true;
            cells[32, 31] = true;

            cells[20, 20] = true;
            cells[21, 21] = true;
            cells[21, 22] = true;
            cells[22, 20] = true;
            cells[22, 21] = true;

            cells[1, 1] = true;
            cells[1, 2] = true;
            cells[1, 3] = true;

            cells[2, 2] = true;
        }

        public void ExecuteConway(bool[,] cells)
        {

            var oldCells = SetupBlankCells();

            PrintScreen(cells);
            System.Console.Read(); //Show initial start screen and wait for input
            System.Console.Clear();

            var iterator = 1;
            while (true)
            {
                var newCells = new bool[_xLength, _yLength];
                for (var y = 0; y < _yLength; y++)
                {
                    for (var x = 0; x < _xLength; x++)
                    {
                        SetCellState(cells, x, y, newCells);
                    }
                }

                if (CheckForFinish(cells, newCells, oldCells, iterator))
                {
                    break;
                }

                oldCells = cells;
                cells = newCells;

                PrintScreen(cells);
                System.Console.WriteLine(iterator);
                System.Threading.Thread.Sleep(50);
                System.Console.Clear();
                iterator++;
            }
            System.Console.Read();
        }

        private void PrintScreen(bool[,] cells)
        {
            var outPutString = "";
            for (var y = 0; y < _yLength; y++)
            {
                for (var x = 0; x < _xLength; x++)
                {
                    outPutString += $"{PrintCell(cells, x, y)}";
                }
                outPutString += "\r\n";
            }
            System.Console.WriteLine(outPutString);
        }

        private bool CheckForFinish(bool[,] cells, bool[,] newCells, bool[,] oldCells, int iterator)
        {
            if (cells.Rank == newCells.Rank
                && Enumerable.Range(0, cells.Rank).All(dimension => cells.GetLength(dimension) == newCells.GetLength(dimension))
                && cells.Cast<bool>().SequenceEqual(newCells.Cast<bool>()))
            {
                return PrintFinalState(cells, iterator);
            }
            else if (newCells.Rank == oldCells.Rank
                     && Enumerable.Range(0, newCells.Rank).All(dimension => newCells.GetLength(dimension) == oldCells.GetLength(dimension))
                     && newCells.Cast<bool>().SequenceEqual(oldCells.Cast<bool>()))
            {
                return PrintFinalState(cells, iterator);
            }

            return false;
        }

        private bool PrintFinalState(bool[,] cells, int iterator)
        {
            PrintScreen(cells);
            System.Console.WriteLine("FINAL STATE. Total iterations: " + iterator);
            return true;
        }

        public void SetCellState(bool[,] cells, int x, int y, bool[,] newCells)
        {
            var count = 0;
            int xMinus1;
            int yMinus1;
            int xPlus1;
            int yPlus1;
            var cell = cells[x, y];
            try
            {
                //calculate wrapping
                if (x - 1 >= 0)
                    xMinus1 = x - 1;
                else
                    xMinus1 = _xLength - 1;

                if (y - 1 >= 0)
                    yMinus1 = y - 1;
                else
                    yMinus1 = _yLength - 1;

                if (x + 1 < _xLength)
                    xPlus1 = x + 1;
                else
                    xPlus1 = 0;

                if (y + 1 < _yLength)
                    yPlus1 = y + 1;
                else
                    yPlus1 = 0;

                //count alive neighbors
                if (xMinus1 >= 0 && cells[xMinus1, y] != null && cells[xMinus1, y])
                    count++;
                if (xMinus1 >= 0 && yMinus1 >= 0 && cells[xMinus1, yMinus1] != null && cells[xMinus1, yMinus1])
                    count++;
                if (yMinus1 >= 0 && cells[x, yMinus1] != null && cells[x, yMinus1])
                    count++;
                if (xPlus1 <= _xLength - 1 && yMinus1 >= 0 && cells[xPlus1, yMinus1] != null && cells[xPlus1, yMinus1])
                    count++;
                if (xPlus1 <= _xLength - 1 && cells[xPlus1, y] != null && cells[xPlus1, y])
                    count++;
                if (xPlus1 <= _xLength - 1 && yPlus1 <= _yLength - 1 && cells[xPlus1, yPlus1] != null && cells[xPlus1, yPlus1])
                    count++;
                if (yPlus1 <= _yLength - 1 && cells[x, yPlus1] != null && cells[x, yPlus1])
                    count++;
                if (xMinus1 >= 0 && yPlus1 <= _yLength - 1 && cells[xMinus1, yPlus1] != null && cells[xMinus1, yPlus1])
                    count++;
            }
            catch
            {
                //Errors? THIS. IS. Conway's game of life!
            }

            //newCells[x, y] = true;
            if (cell)
            {
                if (count < 2)
                    newCells[x, y] = false;
                else if (count >= 4)
                    newCells[x, y] = false;
                else
                    newCells[x, y] = true;
            }
            else if (count == 3)
            {
                newCells[x, y] = true;
            }
            else
            {
                newCells[x, y] = false;
            }
        }

        public char PrintCell(bool[,] cells, int x, int y)
        {
            return cells[x, y] ? Convert.ToChar(2) : ' ';
        }
    }
}
