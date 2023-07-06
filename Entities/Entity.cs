using Glyph_Game_Engine.Utilities;
using Glyph_Game_Engine.Exceptions;

namespace Glyph_Game_Engine.Entities;

public class Entity
{
    public int x;
    public int y;

    public int dirX;
    public int dirY;
    
    public char symbol;

    private char[,] currentMap;
    private List<char> wallChars;
    
    public Entity(int x, int y, char symbol, char[,] map, List<char> wallChars)
    {
        this.x = x;
        this.y = y;
        
        this.symbol = symbol;
        
        currentMap = map;
        this.wallChars = wallChars;
    }
    
    // This function will be called in the update loop during the game loop.
    public virtual void Update()
    {
        // Reset the direction so the entity doesn't slide around.
        dirX = 0;
        dirY = 0;
    }

    // Check if the direction the entity is attempting to go in isn't a wallChar.
    public bool IsObstructed()
    {
        if (currentMap == null || wallChars == null) { throw new UnsetValueException("currentMap or wallChars is null."); }
        return wallChars.Contains(currentMap[y + dirY, x + dirX]);
    }
    
    public Position GetPosition()
    {
        return new Position(x, y);
    }
}