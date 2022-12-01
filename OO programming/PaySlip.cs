namespace OO_programming;

/// <summary>
/// Class a capture details accociated with an employee's pay slip record
/// </summary>
public sealed record PaySlip
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
    public TaxThresholdOption TaxThreshold { get; }

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
    public decimal NetPay { get; }

    /// <summary>
    /// Gets the employee superannuation.
    /// </summary>
    public decimal Superannuation { get; }

    /// <summary>
    /// Initializes this employee pay slip.
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <param name="fullName">The employee full name</param>
    /// <param name="hoursWorked">The employee hours worked</param>
    /// <param name="hourlyRate">The employee hourly rate</param>
    /// <param name="taxThreshold">The employee tax threshold</param>
    /// <param name="grossPay">The employee gross pay</param>
    /// <param name="tax">The employee tax amount</param>
    /// <param name="netPay">The employee net pay</param>
    /// <param name="superannuation">The employee superannuation</param>
    public PaySlip(
        int id,
        string fullName,
        decimal hoursWorked,
        decimal hourlyRate,
        TaxThresholdOption taxThreshold,
        decimal grossPay,
        decimal tax,
        decimal netPay,
        decimal superannuation)
    {
        Id = id;
        FullName = fullName;
        HoursWorked = hoursWorked;
        HourlyRate = hourlyRate;
        TaxThreshold = taxThreshold;
        GrossPay = grossPay;
        Tax = tax;
        NetPay = netPay;
        Superannuation = superannuation;
    }

    /// <summary>
    /// Initializes this employee pay slip.
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <param name="fullName">The employee full name</param>
    /// <param name="hoursWorked">The employee hours worked</param>
    /// <param name="hourlyRate">The employee hourly rate</param>
    /// <param name="taxThreshold">The employee tax threshold</param>
    /// <param name="grossPay">The employee gross pay</param>
    /// <param name="tax">The employee tax amount</param>
    /// <param name="superannuation">The employee superannuation</param>
    public PaySlip(
        int id,
        string fullName,
        decimal hoursWorked,
        decimal hourlyRate,
        TaxThresholdOption taxThreshold,
        decimal grossPay,
        decimal tax,
        decimal superannuation) :
        this(
            id,
            fullName,
            hoursWorked,
            hourlyRate,
            taxThreshold,
            grossPay,
            tax,
            grossPay - tax,
            superannuation)
    { }

    public override string ToString() =>
        $"""
        ID: {Id}
        Full Name: {FullName}
        Hours Worked: {HoursWorked:0.00}
        Hourly Rate: ${HourlyRate:0.00}
        Tax Threshold: {TaxThreshold}
        Gross Pay: ${GrossPay:0.00}
        Tax: ${Tax:0.00}
        Net Pay: ${NetPay:0.00}
        Superannuation: ${Superannuation:0.00}
        """;
}
