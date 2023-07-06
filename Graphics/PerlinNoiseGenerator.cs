using System;


namespace Glyph_Game_Engine.Graphics;

// Don't use this, not complete.
public class PerlinNoiseGenerator
{
    private readonly int[] _permutation = new int[256];

    public PerlinNoiseGenerator(int seed)
    {
        Random random = new Random(seed);

        for (int i = 0; i < 256; i++)
        {
            _permutation[i] = i;
        }

        // Shuffle the permutation array using Fisher-Yates algorithm
        for (int i = 0; i < 256; i++)
        {
            int j = random.Next(256);
            (_permutation[i], _permutation[j]) = (_permutation[j], _permutation[i]);
        }
    }

    public float Generate(float x, float y)
    {
        int xi = (int)Math.Floor(x) & 255;
        int yi = (int)Math.Floor(y) & 255;

        float xf = x - (int)Math.Floor(x);
        float yf = y - (int)Math.Floor(y);

        float u = Fade(xf);
        float v = Fade(yf);

        int aa = _permutation[_permutation[xi] + yi];
        int ab = _permutation[_permutation[xi] + yi + 1];
        int ba = _permutation[_permutation[xi + 1] + yi];
        int bb = _permutation[_permutation[xi + 1] + yi + 1];

        float x1 = Lerp(Gradient(aa, xf, yf), Gradient(ba, xf - 1, yf), u);
        float x2 = Lerp(Gradient(ab, xf, yf - 1), Gradient(bb, xf - 1, yf - 1), u);

        return Lerp(x1, x2, v);
    }

    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    private float Gradient(int hash, float x, float y)
    {
        int h = hash & 7;
        float u = h < 4 ? x : y;
        float v = h < 4 ? y : x;
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }
}