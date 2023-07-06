using System.Diagnostics;

namespace Glyph_Game_Engine.Graphics;

// Don't use this, not complete.
// Uses Perlin noise to generate a map
public class MapGenerator
{
    public float[,] CreateNoiseMap(int height, int width)
    {
        float scale = 0.1f;
        int octaves = 4;
        float persistence = 0.5f;
        float lacunarity = 2f;
        int seed = 12345; // Set your desired seed value here

        float[,] noiseMap = new float[width, height];
        
        PerlinNoiseGenerator generator = new PerlinNoiseGenerator(seed);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseValue = 0f;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x * scale * frequency;
                    float sampleY = y * scale * frequency;
                    float octaveValue = generator.Generate(sampleX, sampleY) * 2f - 1f;
                    noiseValue += octaveValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                noiseMap[x, y] = noiseValue;
            }
        }

        return noiseMap;
    }

    public char[,] GenerateMap(int height, int width, float threshold)
    {
        float[,] noiseMap = CreateNoiseMap(height, width);
        char[,] map = new char[height, width];

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (noiseMap[x, y] < threshold)
                {
                    map[x, y] = ' ';
                }
                else
                {
                    map[x, y] = '#';
                }
            }
        }

        return map;
    }
}