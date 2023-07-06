using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Glyph_Game_Engine.Utilities;
namespace Glyph_Game_Engine.Entities;

// Default player class.
public class Player : Entity
{
    private readonly int health;

    private readonly char[,]? currentMap;
    private readonly List<char>? wallChars;

    // Player needs access to the map and wallChars (chars that count as walls).
    public Player(int x, int y, int health, char[,] map, List<char> wallChars) : base(x, y, '@', map, wallChars)
    {
        this.x = x;
        this.y = y;
        
        this.health = health;

        currentMap = map;
        this.wallChars = wallChars;
    }

    public void HandleInput(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W or ConsoleKey.UpArrow: // Up
                dirY = -1;
                break;
            
            case ConsoleKey.S or ConsoleKey.DownArrow: // Down
                dirY = 1;
                break;
            
            case ConsoleKey.A or ConsoleKey.LeftArrow: // Left
                dirX = -1;
                break;
            
            case ConsoleKey.D or ConsoleKey.RightArrow: // Right
                dirX = 1;
                break;
        }
    }
    
    public override void Update()
    {
        //HandleInput();
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            HandleInput(keyInfo);
        }

        if (!IsObstructed())
        {
            x += dirX;
            y += dirY;
        }

        // Reset the direction so the player doesn't slide around.
        dirX = 0;
        dirY = 0;
    }

    public int GetHealth()
    {
        return health;
    }

}