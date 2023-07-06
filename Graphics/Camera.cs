using Glyph_Game_Engine.Graphics;

namespace Glyph_Game_Engine.Graphics;

public class Camera
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }

    public Camera(int x, int y, int width, int height, int mapWidth, int mapHeight)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        MapWidth = mapWidth;
        MapHeight = mapHeight;
    }

    public void Initialize(Player player, int x, int y)
    {
        player.px = x;
        player.py = y;
    }

    public void Update(Player player)
    {
        // Center the camera on the player
        X = player.px - (Width / 2);
        Y = player.py - (Height / 2);

        // Make sure the camera doesn't go off the edges of the map
        X = Math.Max(0, Math.Min(MapWidth - Width, X));
        Y = Math.Max(0, Math.Min(MapHeight - Height, Y));
    }

    public void Draw(char[,] map, Player player, EntityRenderer entityRenderer)
    {
        // Draw the part of the map that's within the camera's view

        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < Height; y++)
        {
            
            for (int x = 0; x < Width; x++)
            {
                int mapX = Math.Max(0, Math.Min(MapWidth - 1, X + x));
                int mapY = Math.Max(0, Math.Min(MapHeight - 1, Y + y));

                char mapChar = map[mapY, mapX];

                // If the player is at the current mapX/Y, place the character there
                if (player.px == mapX && player.py == mapY)
                {
                    Console.Write('＠');
                }
                else
                {
                    Console.Write(mapChar);
                }
            }

            Console.WriteLine();
        }

        //mapRenderer.DrawMap();
    }
}