namespace OO_programming;

/// <summary>
/// Class representing the tax threshold.
/// </summary>
public sealed record TaxThreshold
{
    /// <summary>
    /// The tax lower bound.
    /// </summary>
    public decimal LowerBound { get; }
    /// <summary>
    /// The tax upper bound.
    /// </summary>
    public decimal UpperBound { get; }
    /// <summary>
    /// Gets the coefficient A from the weekly tax owing formula.
    /// </summary>
    public decimal A { get; }
    /// <summary>
    /// Gets the coefficient B from the weekly tax owing formula.
    /// </summary>
    public decimal B { get; }

    /// <summary>
    /// Initializes this tax threshold record.
    /// </summary>
    /// <param name="lowerBound">The tax lower bound</param>
    /// <param name="upperBound">The tax upper bound</param>
    /// <param name="a">Coefficient A</param>
    /// <param name="b">Coefficient B</param>
    public TaxThreshold(decimal lowerBound, decimal upperBound, decimal a, decimal b)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
        A = a;
        B = b;
    }
}
