namespace Glyph_Game_Engine.Exceptions;
public class UnsetValueException : Exception
{
    public UnsetValueException(string message) 
        : base(message)
    {
    }
}