namespace Glyph_Game_Engine.Utilities;
public class UnsetValueException : Exception
{
    public UnsetValueException(string message) 
        : base(message)
    {
    }
}