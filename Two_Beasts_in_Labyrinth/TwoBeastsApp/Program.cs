using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoBeastsInLabyrinth
{
    /// <summary>
    /// Represents the four cardinal directions.
    /// </summary>
    public class Direction
    {
        public const int UP = 0;
        public const int RIGHT = 1;
        public const int DOWN = 2;
        public const int LEFT = 3;

        public static readonly Dictionary<int, char> Symbols = new Dictionary<int, char>
        {
            { UP, '^' },
            { RIGHT, '>' },
            { DOWN, 'v' },
            { LEFT, '<' }
        };

        public static readonly Dictionary<char, int> SymbolToDir = new Dictionary<char, int>
        {
            { '^', UP },
            { '>', RIGHT },
            { 'v', DOWN },
            { '<', LEFT }
        };

        // Direction vectors: (row_delta, col_delta)
        public static readonly Dictionary<int, (int, int)> Vectors = new Dictionary<int, (int, int)>
        {
            { UP, (-1, 0) },
            { RIGHT, (0, 1) },
            { DOWN, (1, 0) },
            { LEFT, (0, -1) }
        };

        public static int TurnRight(int direction)
        {
            return (direction + 1) % 4;
        }

        public static int TurnLeft(int direction)
        {
            return (direction - 1 + 4) % 4;
        }
    }

    /// <summary>
    /// Represents a beast in the labyrinth.
    /// </summary>
    public class Beast
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int DirectionValue { get; set; }
        public bool PreviousHadWallRight { get; set; }

        public Beast(int row, int col, int direction)
        {
            Row = row;
            Col = col;
            DirectionValue = direction;
            PreviousHadWallRight = true; // Initially assume wall on right
        }

        public (int, int) GetPosition()
        {
            return (Row, Col);
        }

        public char GetDirectionSymbol()
        {
            return Direction.Symbols[DirectionValue];
        }

        public void TurnRight()
        {
            DirectionValue = Direction.TurnRight(DirectionValue);
        }

        public void TurnLeft()
        {
            DirectionValue = Direction.TurnLeft(DirectionValue);
        }

        public void MoveForward()
        {
            var (deltaRow, deltaCol) = Direction.Vectors[DirectionValue];
            Row += deltaRow;
            Col += deltaCol;
        }

        public (int, int) GetPositionAhead()
        {
            var (deltaRow, deltaCol) = Direction.Vectors[DirectionValue];
            return (Row + deltaRow, Col + deltaCol);
        }

        public (int, int) GetPositionToRight()
        {
            int rightDirection = Direction.TurnRight(DirectionValue);
            var (deltaRow, deltaCol) = Direction.Vectors[rightDirection];
            return (Row + deltaRow, Col + deltaCol);
        }
    }

    /// <summary>
    /// Represents the labyrinth/maze with multiple beasts.
    /// </summary>
    public class Labyrinth
    {
        private int width;
        private int height;
        private char[,] mapData;
        private List<Beast> beasts;

        public Labyrinth(int width, int height, List<string> mapLines)
        {
            this.width = width;
            this.height = height;
            this.mapData = new char[height, width];
            this.beasts = new List<Beast>();

            // Copy map data
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    mapData[row, col] = mapLines[row][col];
                }
            }

            FindBeasts();
        }

        private void FindBeasts()
        {
            // Find all beasts in order (by row, then column)
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    char c = mapData[row, col];
                    if (Direction.SymbolToDir.ContainsKey(c))
                    {
                        int direction = Direction.SymbolToDir[c];
                        beasts.Add(new Beast(row, col, direction));
                        mapData[row, col] = '.'; // Clear beast from map
                    }
                }
            }
        }

        private bool IsWall(int row, int col)
        {
            if (row < 0 || row >= height || col < 0 || col >= width)
                return true;
            return mapData[row, col] == 'X';
        }

        private bool IsOccupiedByOtherBeast(int row, int col, Beast currentBeast)
        {
            foreach (var beast in beasts)
            {
                if (beast != currentBeast && beast.Row == row && beast.Col == col)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanMoveTo(int row, int col, Beast currentBeast)
        {
            // Can't move if it's a wall or occupied by another beast
            return !IsWall(row, col) && !IsOccupiedByOtherBeast(row, col, currentBeast);
        }

        public void PerformStep()
        {
            // Move each beast in order
            foreach (var beast in beasts)
            {
                var (rightRow, rightCol) = beast.GetPositionToRight();
                var (aheadRow, aheadCol) = beast.GetPositionAhead();

                bool hasWallRight = !CanMoveTo(rightRow, rightCol, beast);
                bool canGoForward = CanMoveTo(aheadRow, aheadCol, beast);

                // Decision based on current and previous state
                if (beast.PreviousHadWallRight && !hasWallRight)
                {
                    // Wall just disappeared on right - turn right to follow it
                    beast.TurnRight();
                }
                else if (canGoForward)
                {
                    // Can go forward - move forward
                    beast.MoveForward();
                }
                else if (!hasWallRight)
                {
                    // Can't go forward but space on right - turn right
                    beast.TurnRight();
                }
                else
                {
                    // Wall on right and ahead - turn left
                    beast.TurnLeft();
                }

                // Update state for next step
                beast.PreviousHadWallRight = hasWallRight;
            }
        }

        public void PrintMap()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Check if any beast is at this position
                    Beast? beastAtPos = beasts.FirstOrDefault(b => b.Row == row && b.Col == col);

                    if (beastAtPos != null)
                    {
                        Console.Write(beastAtPos.GetDirectionSymbol());
                    }
                    else
                    {
                        Console.Write(mapData[row, col]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Main program class.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Read input
            int width = int.Parse(Console.ReadLine()!);
            int height = int.Parse(Console.ReadLine()!);

            List<string> mapData = new List<string>();
            for (int i = 0; i < height; i++)
            {
                mapData.Add(Console.ReadLine()!);
            }

            // Create labyrinth
            Labyrinth labyrinth = new Labyrinth(width, height, mapData);

            // Simulate 20 steps
            for (int step = 1; step <= 20; step++)
            {
                labyrinth.PerformStep();
                Console.WriteLine($"{step}. krok");
                labyrinth.PrintMap();
            }
        }
    }
}
