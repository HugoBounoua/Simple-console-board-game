using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.Models
{
    public class Position
    {
        /// <summary>
        /// The coordonates x and y (respectively horizontal and vertical)
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// A display method to nicely output the coordonates
        /// </summary>
        public string Display
        {
            get
            {
                return $"[{X.ToString("##")},{Y.ToString("##")}]";
            }
       }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Check if the position is equals to the Tuple(x,y)
        /// </summary>
        /// <param name="x">The horizontal position</param>
        /// <param name="y">The vertical position</param>
        /// <returns>True if coordonates are correct</returns>
        public bool IsAt(int x, int y)
        {
            return this.X == x && this.Y == y;
        }
    }
}
