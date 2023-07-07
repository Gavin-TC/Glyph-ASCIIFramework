using Glyph_Game_Engine.Utilities;

namespace Glyph_Game_Engine.Entities;

public class Entity
{
    public int x;
    public int y;

    public int dirX;
    public int dirY;
    
    public char symbol;

    private Map currentMap;
    private List<char> wallChars;
    private List<char> walkableChars;
    
    public Entity(int x, int y, char symbol, Map map, List<char> wallChars, List<char> walkableChars)
    {
        this.x = x;
        this.y = y;
        
        this.symbol = symbol;
        
        currentMap = map;
        this.wallChars = wallChars;
        this.walkableChars = walkableChars;
    }
    
    // This function will be called in the update loop during the game loop.
    public virtual void Update()
    {
        // Reset the direction so the entity doesn't slide around.
        dirX = 0;
        dirY = 0;
    }

    // Check if the direction the entity is attempting to go in is a walkable character.
    protected bool IsWalkable()
    {
        if (currentMap?.areaMap == null || walkableChars == null) { throw new UnsetValueException("currentMap or wallChars is null."); }
        Console.Write(currentMap.areaMap[y + dirY, x + dirX]);
        return walkableChars.Contains(currentMap.areaMap[y + dirY, x + dirX]);
    }
    
    public Position GetPosition()
    {
        return new Position(x, y);
    }
}