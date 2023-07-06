using Glyph_Game_Engine.Entities;

namespace Glyph_Game_Engine.Utilities;

public class Map
{
    public int? width, height;
    public char[,]? areaMap;
    public char[,]? entityMap;
    
    public List<Entity>? _entities;
    
    public Map(char[,] contents, List<Entity>? entities)
    {
        _entities = entities;

        areaMap = contents;
        entityMap = contents;
        width = areaMap.GetLength(1);
        height = areaMap.GetLength(0);
    }

    public void GenerateMap()
    {
        throw new NotImplementedException("GenerateMap() isn't implemented yet, please provide a map to the object.");
    }
}