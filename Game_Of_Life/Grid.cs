﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life
{
    class Grid
    {
        GridSquare[,] GridSquares;
        GridSquare[,] ShadowGridSquares;
        public class GridSquare
        {
            bool on = false;
            int LiveNeighbors = 0;

            public GridSquare()
            {

            }

            public bool IsOn
            {
                get { return on; }
                set { on = value; }
            }

            public int Count
            {
                get { return LiveNeighbors; }
                set { LiveNeighbors = value; }
            }

            public void Reset()
            {
                on = false;
                LiveNeighbors = 0;
            }

            public void ToggleState()
            {
                on = !on;
            }
        }


        public Grid (int x, int y)
        {
            NewGrid(x, y);
        }
        public GridSquare this[int x, int y]
        {
            get { return GridSquares[x,y]; }
            //set { GridSquares[x, y] = value; }
        }
        public int Width
        {
            get { return GridSquares.GetLength(0); }
        }

        public int Height
        {
            get { return GridSquares.GetLength(1); }
        }

        public void Reset()
        {
            for (int y = 0; y < GridSquares.GetLength(1); y++)
            {
                for (int x = 0; x < GridSquares.GetLength(0); x++)
                {
                    GridSquares[x, y].Reset() ;
                }
            }

        }

        public void NewGrid(int width, int height)
        {
            GridSquare[,] temp = new GridSquare[width, height];
            ShadowGridSquares = new GridSquare[width, height];
            for (int y = 0; y < temp.GetLength(1); y++)
            {
                for (int x = 0; x < temp.GetLength(0); x++)
                {
                    if(GridSquares != null && GridSquares[x, y] != null)
                    {
                        temp[x, y] = GridSquares[x, y];
                    }
                    else
                    {
                        temp[x, y] = new GridSquare();
                        ShadowGridSquares[x, y] = new GridSquare();
                    }
                    
                }
            }
            
            GridSquares = temp;
            
        }

        public void CalculateNext()
        {
            for (int y = 0; y < GridSquares.GetLength(1); y++)
            {
                for (int x = 0; x < GridSquares.GetLength(0); x++)
                {
                    int count = 0;
                    for (int sy = y - 1; sy < y + 2; sy++)
                    {
                        for (int sx = x - 1; sx < x + 2; sx++)
                        {

                            if (sx > -1 && sy > -1 && sx < GridSquares.GetLength(0) && sy < GridSquares.GetLength(1))
                            {
                                if (GridSquares[sx, sy].IsOn && !(sx == x && sy == y))
                                {
                                    count++;
                                }

                            }

                        }
                    }
                    switch (count)
                    {
                        case 2:
                            if (GridSquares[x, y].IsOn)
                            {
                                ShadowGridSquares[x, y].IsOn = true;
                            }
                            else
                            {
                                ShadowGridSquares[x, y].IsOn = false;
                            }
                            break;
                        case 3:

                            ShadowGridSquares[x, y].IsOn = true;

                            break;
                        default:
                            ShadowGridSquares[x, y].IsOn = false;
                            break;
                    }

                }
            }
            for (int y = 0; y < GridSquares.GetLength(1); y++)
            {
                for (int x = 0; x < GridSquares.GetLength(0); x++)
                {
                    GridSquares[x, y].IsOn = ShadowGridSquares[x, y].IsOn;
                }
            }


        }
    }
}
