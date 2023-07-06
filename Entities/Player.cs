

using Glyph_Game_Engine.Utilities;

namespace Glyph_Game_Engine.Entities;

// Default player class.
public class Player : Entity
{
    private readonly int health;

    private readonly Map currentMap;
    
    private readonly List<char> wallChars;
    private readonly List<char> walkableChars;

    // Player needs access to the map and wallChars (chars that count as walls).
    public Player(int x, int y, int health, Map currentMap, List<char> wallChars, List<char> walkableChars)
        : base(x, y, '@', currentMap, wallChars, walkableChars)
    {
        this.x = x;
        this.y = y;
        
        this.health = health;

        this.currentMap = currentMap;
        this.wallChars = wallChars;
        this.walkableChars = walkableChars;
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

        if (IsWalkable())
        {
            x += dirX;
            y += dirY;
            Console.Write("             ");
        }
        else
        {
            Console.Write("NOT WALKABLE!");
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