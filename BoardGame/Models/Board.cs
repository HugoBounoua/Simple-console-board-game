using BoardGame.Enums;
using BoardGame.Models;
using BoardGame.Models.Tiles;
using BoardGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardGame
{
    public class Board
    {
        /// <summary>
        /// The instance of the Board
        /// </summary>
        private static Board instance = null;
        public static Board Instance
        {
            get
            {
                if (instance == null)
                    instance = new Board(Constants.MaxHeight, Constants.MaxWidth);
                return instance;
            }
        }

        /// <summary>
        /// The height of the board
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The width of the board
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The list of tiles of the board
        /// </summary>
        public List<Tile> Tiles { get; set; }

        // Default constructor
        private Board(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Initialize the board with specified height and width
        /// </summary>
        /// <param name="height">The height of the board</param>
        /// <param name="width">The width of the board</param>
        public static void Init(int height, int width)
        {
            if (instance == null)
                instance = new Board(Math.Min(Constants.MaxHeight, height), Math.Min(Constants.MaxWidth, width));
            
            // Instantiate the Tile list
            Instance.Tiles = new List<Tile>();

            // Create walls on bottom line
            for (int x = 0; x < Instance.Width; x++)
                Instance.Tiles.Add(TileFactory.CreateTileAt(new Position(x, 0), EnumTileType.Wall));

            // Create walls on top line
            for (int x = 0; x < Instance.Width; x++)
                Instance.Tiles.Add(TileFactory.CreateTileAt(new Position(x, Instance.Height - 1), EnumTileType.Wall));

            // Create walls on first column
            for (int y = 0; y < Instance.Height; y++)
                Instance.Tiles.Add(TileFactory.CreateTileAt(new Position(0, y), EnumTileType.Wall));

            // Create walls on last column
            for (int y = 0; y < Instance.Height; y++)
                Instance.Tiles.Add(TileFactory.CreateTileAt(new Position(Instance.Width - 1, y), EnumTileType.Wall));

            // Create others Tiles (without the frame of 1 tile) randomly
            for (int x = 1; x < Instance.Width - 1; x++)
            {
                for (int y = 1; y < Instance.Height - 1; y++)
                {
                    Instance.Tiles.Add(TileFactory.CreateTileAt(new Position(x, y)));
                }
            }
        }

        /// <summary>
        /// Get the tile at a specific position
        /// </summary>
        /// <param name="x">The x coordonate</param>
        /// <param name="y">The y coordonate</param>
        /// <returns></returns>
        public Tile GetTileOrDefault(int x, int y)
        {
            // Get the Tile list or instantiate it
            if (Tiles == null)
                Tiles = new List<Tile>();

            Tile tile = Tiles.FirstOrDefault(t => t.Position != null && t.Position.IsAt(x, y));
            if (tile == null)
                tile = TileFactory.CreateTileAt(new Position(x, y));

            return tile;
        }

        /// <summary>
        /// Display the board
        /// </summary>
        public void Display()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"BOARD :{Environment.NewLine}");
            for (int x = 0; x < Instance.Width; x++)
            {
                StringBuilder line = new StringBuilder();
                for (int y = Instance.Height - 1; y >= 0; y--)
                {
                    line.Append(" " + GetTileOrDefault(x, y).Display());
                }
                Console.WriteLine(line);
            }
            Console.WriteLine($"BOARD :{Environment.NewLine}");
        }

        /// <summary>
        /// Execute next step for every tile
        /// </summary>
        public void NextStep()
        {
            for (int x = 0; x < Instance.Width; x++)
            {
                for (int y = Instance.Height - 1; y >= 0; y--)
                {
                    Tile tile = GetTileOrDefault(x, y);
                    Tile newTile = tile.ExecuteStep(getNeighbors(tile.Position));
                    Tiles[Tiles.IndexOf(tile)] = newTile;
                }
            }
        }

        /// <summary>
        /// Get neighbors of tile
        /// </summary>
        /// <param name="position">The position of the current tile</param>
        /// <returns>The list of neigbors of the tile</returns>
        private List<Tile> getNeighbors(Position position)
        {
            List<Tile> neighbors = new List<Tile>();
            // Left
            if(position.X > 1)
                neighbors.Add(GetTileOrDefault(position.X - 1, position.Y));
            // Top
            if (position.Y < Height - 1)
                neighbors.Add(GetTileOrDefault(position.X, position.Y + 1));
            // Right
            if (position.X < Width - 1)
                neighbors.Add(GetTileOrDefault(position.X + 1, position.Y));
            // Bottom
            if (position.Y > 1)
                neighbors.Add(GetTileOrDefault(position.X, position.Y - 1));
            return neighbors;
        }
    }
}
