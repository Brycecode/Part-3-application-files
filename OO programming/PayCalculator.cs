using System;
using System.Collections.Generic;
using System.Linq;

namespace OO_programming;

/// <summary>
/// Base class to hold all Pay calculation functions
/// Default class behaviour is tax calculated with tax threshold applied
/// </summary>
public abstract class PayCalculator
{
    private readonly Employee employee;
    private readonly decimal hoursWorked;
    private readonly IReadOnlyCollection<TaxThreshold> taxThresholds;

    private TaxThreshold GetTaxThreshold()
    {
        var amount = CalculatePay();
        return taxThresholds.First(tax => tax.LowerBound <= amount && amount <= tax.UpperBound);
    }

    /// <summary>
    /// Initializes this pay calculator.
    /// </summary>
    /// <param name="employee">The employee</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    /// <param name="taxThresholds">The tax threshold records</param>
    private protected PayCalculator(Employee employee, decimal hoursWorked, IReadOnlyCollection<TaxThreshold> taxThresholds)
    {
        this.employee = employee;
        this.hoursWorked = hoursWorked;
        this.taxThresholds = taxThresholds;
    }

    /// <summary>
    /// Calculates the pay for the provided employee.
    /// </summary>
    /// <returns>The pay</returns>
    private decimal CalculatePay() => employee.HourlyRate * hoursWorked;

    /// <summary>
    /// Calculates tax amount for the provided employee.
    /// </summary>
    /// <returns>The tax</returns>
    private decimal CalculateTax()
    {
        var taxThreshold = GetTaxThreshold();
        var x = (Math.Floor(CalculatePay()) + .99m);
        // ax - b
        return (taxThreshold.A * x) - taxThreshold.B;
    }

    /// <summary>
    /// Calculates superannuation for the provided employee.
    /// </summary>
    /// <returns>Superannuation</returns>
    private decimal CalculateSuperannuation() => CalculatePay() * 0.105m; // gross_pay * 10.5%

    /// <summary>
    /// Creates a pay slip record for an employee.
    /// </summary>
    /// <returns>The pay slip record</returns>
    public PaySlip CreatePaySlip() => new(employee.Id, employee.GetFullName(), hoursWorked, employee.HourlyRate, employee.TaxThreshold, CalculatePay(), CalculateTax(), CalculateSuperannuation());

    /// <summary>
    /// Creates a new pay calculator based on the tax threshold of the provided employee.
    /// </summary>
    /// <param name="employee">The employee</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    /// <returns>The appropriate pay calculator</returns>
    /// <exception cref="InvalidOperationException">If the provided tax threshold option is not defined</exception>
    public static PayCalculator CreateNew(Employee employee, decimal hoursWorked) => employee.TaxThreshold switch
    {
        TaxThresholdOption.Y => new PayCalculatorWithThreshold(employee, hoursWorked, "taxrate-withthreshold".ReadCsv<TaxThreshold>().ToArray()),
        TaxThresholdOption.N => new PayCalculatorNoThreshold(employee, hoursWorked, "taxrate-nothreshold".ReadCsv<TaxThreshold>().ToArray()),
        _ => throw new InvalidOperationException(),
    };
}

/// <summary>
/// Extends PayCalculator class handling No tax threshold
/// </summary>
file sealed class PayCalculatorNoThreshold : PayCalculator
{
    /// <summary>
    /// Initializes this pay calculator without threshold.
    /// </summary>
    /// <param name="employee">The employee</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    /// <param name="taxThresholds">The tax threshold records</param>
    public PayCalculatorNoThreshold(Employee employee, decimal hoursWorked, IReadOnlyCollection<TaxThreshold> taxThresholds) : base(employee, hoursWorked, taxThresholds)
    {
    }
}

/// <summary>
/// Extends PayCalculator class handling With tax threshold
/// </summary>
file sealed class PayCalculatorWithThreshold : PayCalculator
{
    /// <summary>
    /// Initializes this pay calculator with threshold.
    /// </summary>
    /// <param name="employee">The employee</param>
    /// <param name="hoursWorked">The number of hours worked</param>
    /// <param name="taxThresholds">The tax threshold records</param>
    public PayCalculatorWithThreshold(Employee employee, decimal hoursWorked, IReadOnlyCollection<TaxThreshold> taxThresholds) : base(employee, hoursWorked, taxThresholds)
    {
    }
}
