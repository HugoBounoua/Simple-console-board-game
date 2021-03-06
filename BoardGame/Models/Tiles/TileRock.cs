﻿using BoardGame.Enums;
using BoardGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Models.Tiles
{
    public class TileRock : Tile
    {
        /// <summary>
        /// The default constructor (notice it calls the base class constructor)
        /// </summary>
        /// <param name="position">The position of the tile</param>
        public TileRock(Position position) : base(position)
        {
            this.Type = EnumTileType.Rock;
        }

        /// <summary>
        /// The method that all Tile must implement to describe their own behavior
        /// It return a Tile :
        /// - Either itself, if no change has been made
        /// - Either a new Tile, depending on the transformation it got (condition of transformation in the ExecuteStep of the inherited Tile classes)
        /// </summary>
        /// <param name="neighbors">The list of neighbors of the tile</param>
        public override Tile ExecuteStep(List<Tile> neighbors)
        {
            // Become "Lava" if the "Rock" touches one
            if (neighbors.Any(n => n.Type == EnumTileType.Lava))
            {
                // 8 chances out of 10 to become "Lava" if the "Rock" touches one
                Random rand = new Random();
                if (rand.Next(10) < 8)
                    return TileFactory.CreateTileAt(this.Position, EnumTileType.Lava);
            }
            return this;
        }

        /// <summary>
        /// Display the Tile nicely
        /// </summary>
        /// <returns>The display of the tile</returns>
        public override string Display()
        {
            return "O";
        }
    }
}