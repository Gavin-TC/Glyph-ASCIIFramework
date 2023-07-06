using System.Formats.Asn1;
using System.Runtime.InteropServices;
using Glyph_Game_Engine.Entities;
using Glyph_Game_Engine.Exceptions;
using Glyph_Game_Engine.Utilities;

namespace Glyph_Game_Engine.Graphics;
public class MapRenderer
{
    private readonly int screenWidth, screenHeight;
    // Screen buffer
    private char[,]? buffer;
    private Map map { get; set; }

    public List<char> wallChars = new List<char>();

    public MapRenderer(int screenWidth, int screenHeight, Map map, List<char>? wallChars)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.map = map;
        buffer = this.map.areaMap;
        
        if (wallChars != null)
        {
            this.wallChars = wallChars;
        }

        // This hides the cursor and hides the 'refreshing' of the screen.
        Console.CursorVisible = false;
    }
    
    public void AddChar(char character)
    {
        wallChars.Add(character);
    }
    
    // This is done to dynamically change the map. 
    public void SetMap(Map mapObject)
    {
        this.map = mapObject;
    }

    public void RenderMap()
    {
        if (map?.areaMap == null) { throw new UnsetValueException("Map has not been set."); }
        
        buffer = map.areaMap;
        
        // This only renders the part of the map that fits within the screenWidth/Height.
        for (int y = 0; y < screenHeight; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                Console.Write(buffer[y, x]);
            }
            Console.WriteLine();
        }
    }

    public void RenderEntities(List<Entity> entities)
    {
        if (map?.entityMap == null || map?.areaMap == null) { throw new UnsetValueException("Map has not been set."); }
        if (entities == null) { throw new UnsetValueException("Entities list is null."); }

        // This only renders the entities in the visible screen.
        for (int y = 0; y < screenHeight; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                foreach (var entity in entities)
                {
                    if (entity.x == x && entity.y == y)
                    {
                        map.entityMap[y, x] = entity.symbol;
                    }
                    else
                    {
                        if (!wallChars.Contains(map.entityMap[y, x]))
                        {
                            map.entityMap[y, x] = ' ';
                        }
                    }
                }
            }
        }
    }

    // <summary>
    // Effectively clears screen by putting cursor to origin.
    // This means the next print will appear over the last, basically overwriting.
    // This method may cause some issues later on, though.
    // </summary>
    public void ClearScreen()
    {
        Console.SetCursorPosition(0, 0);
    }
}