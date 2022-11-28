namespace OO_programming;

/// <summary>
/// Record holding employee data.
/// </summary>
internal sealed record Employee
{
    /// <summary>
    /// Gets the ID of this employee.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the full name of this employee.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets the first name of this employee.
    /// </summary>
    internal string FirstName { get; }

    /// <summary>
    /// Gets the last name of this employee.
    /// </summary>
    internal string LastName { get; }

    /// <summary>
    /// Gets the hourly rate of this employee.
    /// </summary>
    internal decimal HourlyRate { get; }

    /// <summary>
    /// Gets the tax threshold for this employee.
    /// </summary>
    internal TaxThresholdOption TaxThreshold { get; }

    /// <summary>
    /// Initializes this employee record.
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <param name="firstName">The employee first name</param>
    /// <param name="lastName">The employee last name</param>
    /// <param name="hourlyRate">The employee hourly rate</param>
    /// <param name="taxThreshold">The employee tax threshold</param>
    public Employee(
        int id,
        string firstName,
        string lastName,
        decimal hourlyRate,
        TaxThresholdOption taxThreshold)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        HourlyRate = hourlyRate;
        TaxThreshold = taxThreshold;
    }

    public override string ToString() => $"{Id} - {FullName}";
}
