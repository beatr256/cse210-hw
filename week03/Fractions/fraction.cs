using System;

public class Fraction
{
    // Private attributes
    private int _top;
    private int _bottom;

    // Constructors
    public Fraction()  // No parameters, default to 1/1
    {
        _top = 1;
        _bottom = 1;
    }

    public Fraction(int top)  // One parameter, denominator = 1
    {
        _top = top;
        _bottom = 1;
    }

    public Fraction(int top, int bottom)  // Two parameters
    {
        _top = top;
        _bottom = bottom;
    }

    // Getters
    public int GetTop()
    {
        return _top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    // Setters
    public void SetTop(int top)
    {
        _top = top;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Method to return fraction as a string
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Method to return decimal value
    public double GetDecimalValue()
    {
        return (double)_top / (double)_bottom;
    }
}
