namespace OO_programming;

/// <summary>
/// Employee pay calculation report.
/// </summary>
internal sealed record EmployeePayReport
{
    /// <summary>
    /// Gets the employee ID.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the employee full name.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    /// Gets the employee hours worked.
    /// </summary>
    public decimal HoursWorked { get; }

    /// <summary>
    /// Gets the employee hourly rate.
    /// </summary>
    public decimal HourlyRate { get; }

    /// <summary>
    /// Gets the employee tax threshold.
    /// </summary>
    public decimal TaxThreshold { get; }

    /// <summary>
    /// Gets the employee gross pay.
    /// </summary>
    public decimal GrossPay { get; }

    /// <summary>
    /// Gets the employee tax.
    /// </summary>
    public decimal Tax { get; }

    /// <summary>
    /// Gets the employee net pay.
    /// </summary>
    public decimal NetPay => GrossPay - Tax;

    /// <summary>
    /// Gets the employee superannuation.
    /// </summary>
    public decimal Superannuation { get; }

    /// <summary>
    /// Initializes this employee pay report instance.
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <param name="fullName">The employee full name</param>
    /// <param name="hoursWorked">The employee hours worked</param>
    /// <param name="hourlyRate">The employee hourly rate</param>
    /// <param name="taxThreshold">The employee tax threshold</param>
    /// <param name="grossPay">The employee gross pay</param>
    /// <param name="tax">The employee tax amount</param>
    /// <param name="superannuation">The employee superannuation</param>
    internal EmployeePayReport(
        int id,
        string fullName,
        decimal hoursWorked,
        decimal hourlyRate,
        decimal taxThreshold,
        decimal grossPay,
        decimal tax,
        decimal superannuation)
    {
        Id = id;
        FullName = fullName;
        HoursWorked = hoursWorked;
        HourlyRate = hourlyRate;
        TaxThreshold = taxThreshold;
        GrossPay = grossPay;
        Tax = tax;
        Superannuation = superannuation;
    }

    public override string ToString() =>
        $"""
        ID: {Id}
        Full Name: {FullName}
        Hours Worked: {HoursWorked}
        Hourly Rate: {HourlyRate}
        Tax Threshold: ?
        Gross Pay: {GrossPay}
        Tax: {Tax}
        Net Pay: {NetPay}
        Superannuation: {Superannuation}
        """;
}
