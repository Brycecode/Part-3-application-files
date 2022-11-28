using System;

namespace OO_programming;

/// <summary>
/// Base class to hold all Pay calculation functions
/// Default class behaviour is tax calculated with tax threshold applied
/// </summary>
internal abstract class PayCalculator
{
    private readonly Employee employee;
    private readonly decimal hoursWorked;

    /// <summary>
    /// Initializes this pay calculator instance.
    /// </summary>
    /// <param name="employee">The employee</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    private protected PayCalculator(Employee employee, decimal hoursWorked)
    {
        this.employee = employee;
        this.hoursWorked = hoursWorked;
    }

    /// <summary>
    /// Calculates the pay for the provided employee.
    /// </summary>
    /// <returns>The pay</returns>
    private protected abstract decimal CalculatePay();

    /// <summary>
    /// Calculates tax amount for the provided employee.
    /// </summary>
    /// <returns>The tax</returns>
    private protected abstract decimal CalculateTax();

    /// <summary>
    /// Calculates superannuation for the provided employee.
    /// </summary>
    /// <returns>Superannuation</returns>
    private protected abstract decimal CalculateSuperannuation();

    internal EmployeePayReport CreateReport() => new(employee.Id, employee.FullName, hoursWorked, employee.HourlyRate, 0, CalculatePay(), CalculateTax(), CalculateSuperannuation());

    internal static PayCalculator CreateNew(Employee employee, decimal hoursWorked) => employee.TaxThreshold switch
    {
        TaxThreshold.Y => new PayCalculatorWithThreshold(employee, hoursWorked),
        TaxThreshold.N => new PayCalculatorNoThreshold(employee, hoursWorked),
        _ => throw new InvalidOperationException(),
    };
}

/// <summary>
/// Extends PayCalculator class handling No tax threshold
/// </summary>
file sealed class PayCalculatorNoThreshold : PayCalculator
{
    /// <summary>
    /// Initializes this pay calculator with no threshold.
    /// </summary>
    /// <param name="employee">The employee record</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    internal PayCalculatorNoThreshold(Employee employee, decimal hoursWorked) : base(employee, hoursWorked)
    {
    }

    private protected override decimal CalculatePay() => 0;

    private protected override decimal CalculateSuperannuation() => 0;

    private protected override decimal CalculateTax() => 0;
}

/// <summary>
/// Extends PayCalculator class handling With tax threshold
/// </summary>
file sealed class PayCalculatorWithThreshold : PayCalculator
{
    /// <summary>
    /// Initializes this pay calculator with threshold.
    /// </summary>
    /// <param name="employee">The employee record</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    internal PayCalculatorWithThreshold(Employee employee, decimal hoursWorked) : base(employee, hoursWorked)
    {
    }

    private protected override decimal CalculatePay() => 0;

    private protected override decimal CalculateSuperannuation() => 0;

    private protected override decimal CalculateTax() => 0;
}
