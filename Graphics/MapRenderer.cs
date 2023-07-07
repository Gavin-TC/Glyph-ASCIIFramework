using System.Diagnostics.Tracing;
using System.Formats.Asn1;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Channels;
using Glyph_Game_Engine.Entities;
using Glyph_Game_Engine.Utilities;

namespace Glyph_Game_Engine.Graphics;
public class MapRenderer
{
    private readonly int screenWidth, screenHeight;
    // Screen buffer
    private char[,]? areaBuffer;
    private char[,]? entityBuffer;
    private Map map { get; set; }

    // Chars that are considered walls and therefore cannot be walked into.
    public List<char> wallChars = new List<char>();
    // Chars that are considered walkable and therefore can be walked into.
    public List<char> walkableChars = new List<char>();

    public MapRenderer(int screenWidth, int screenHeight, Map map, List<char>? wallChars, List<char>? walkableChars)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.map = map;
        areaBuffer = this.map.areaMap;
        entityBuffer = this.map.entityMap;
        
        if (wallChars != null) { this.wallChars = wallChars; }

        if (walkableChars != null) { this.walkableChars = walkableChars; }

        // This hides the cursor and hides the 'refreshing' of the screen.
        Console.CursorVisible = false;
    }
    
    public void AddChar(char character)
    {
        wallChars.Add(character);
    }
    
    public void SetMap(Map mapObject)
    {
        this.map = mapObject;
    }

    // Currently, the Entity only has access to the areaMap as it's initialized.
    // This means that the Entity doesn't see what's currently on the map, but what the map starts with.
    // TODO: Make the Entity class able to check if the current instance of the map being rendered is a walkable tile.
    public void RenderMap(List<Entity> entities)
    {
        if (map?.areaMap == null) { throw new UnsetValueException("Map has not been set."); }

        // This only renders the part of the map that fits within the screenWidth/Height.
        for (int y = 0; y < screenHeight; y++)
        {
            
            for (int x = 0; x < screenWidth; x++)
            {
                bool entityRendered = false;

                foreach (var entity in entities)
                {
                    if (entity.x == x && entity.y == y)
                    {
                        Console.Write(entity.symbol);
                        entityRendered = true;
                        break; // Exit the loop once an entity is rendered for this tile
                    }
                }

                if (!entityRendered)
                {
                    Console.Write(areaBuffer[y, x]);
                };
            }
            Console.WriteLine();
        }
    }

    public void RenderEntities(List<Entity> entities)
    {
        if (map?.entityMap == null || map?.areaMap == null) { throw new UnsetValueException("Map has not been set."); }
        if (entities == null) { throw new UnsetValueException("Entities list is null."); }

        entityBuffer = map.entityMap;

        // This only renders the entities in the visible screen.
        for (int y = 0; y < screenHeight; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                foreach (var entity in entities)
                {
                    // If there's an entity within the screen, print it at its location.
                    if (entity.x == x && entity.y == y)
                    {
                        entityBuffer[y, x] = entity.symbol;
                    }
                }

                Console.WriteLine("-ENTITY MAP: " + map.entityMap[y, x] + "-");
                entityBuffer[y, x] = map.entityMap[y, x];
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