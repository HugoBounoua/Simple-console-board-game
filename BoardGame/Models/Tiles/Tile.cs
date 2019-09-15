using BoardGame.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.Models.Tiles
{
    public abstract class Tile
    {
        /// <summary>
        /// Position of the tile
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// The type of tile
        /// </summary>
        public EnumTileType Type { get; set; } = EnumTileType.Unkown;

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="position">The position of the tile</param>
        /// <param name="type">The type of tile</param>
        public Tile(Position position)
        {
            // Assignations with default values verifications
            this.Position = position ?? throw new ArgumentNullException("Tile constructor : the position cannot be null.");
        }

        /// <summary>
        /// The method that all Tile must implement to describe their own behavior
        /// It return a Tile :
        /// - Either itself, if no change has been made
        /// - Either a new Tile, depending on the transformation it got (condition of transformation in the ExecuteStep of the inherited Tile classes)
        /// </summary>
        /// <param name="neighbors">The list of neighbors of the tile</param>
        public abstract Tile ExecuteStep(List<Tile> neighbors);

        /// <summary>
        /// Display the Tile nicely
        /// </summary>
        /// <returns>The display of the tile</returns>
        public abstract string Display();
    }
}