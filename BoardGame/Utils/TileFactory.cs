using BoardGame.Enums;
using BoardGame.Models;
using BoardGame.Models.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardGame.Utils
{
    public static class TileFactory
    {
        /// <summary>
        /// Create a Tile with a random type (not Unknown) at a specified position
        /// </summary>
        /// <param name="position">The position of the Tile</param>
        /// <returns>The newly created Tile</returns>
        public static Tile CreateTileAt(Position position, EnumTileType tileType = EnumTileType.Unkown)
        {
            if(tileType == EnumTileType.Unkown)
            {
                EnumTileType[] tileTypes = ((EnumTileType[])Enum.GetValues(typeof(EnumTileType))).Where(t => (EnumTileType)t != EnumTileType.Unkown).ToArray();
                Random random = new Random();
                tileType = (EnumTileType)tileTypes.GetValue(random.Next(tileTypes.Length));
            }

            return tileType.CreateNewTileFromType(position);
        }

        /// <summary>
        /// Instantiate a Tile of a specific type
        /// </summary>
        /// <param name="type">The type of the new Tile to create</param>
        /// <param name="position">The position of the Tile</param>
        /// <returns>The newly create Tile</returns>
        public static Tile CreateNewTileFromType(this EnumTileType type, Position position)
        {
            if (type == EnumTileType.Rock)
                return new TileRock(position);
            else if (type == EnumTileType.Ice)
                return new TileIce(position);
            else if (type == EnumTileType.Lava)
                return new TileLava(position);
            else if (type == EnumTileType.Wall)
                return new TileWall(position);
            throw new InvalidOperationException($"Unable to create a Tile of type [{type.ToString()}].");
        }
    }
}
