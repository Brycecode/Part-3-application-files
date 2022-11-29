using FluentAssertions;
using NUnit.Framework;
using OO_programming;

namespace Tests;

/// <summary>
/// Integration tests that ensure the pay calculator is correctly reading from the CSV data files,
/// filtering the correct tax threshold to apply, and calculating the pay slip correctly.
/// </summary>
public sealed class PayCalculatorTests
{
    private const decimal HoursWorked = 40m;

    private readonly Employee employeeWithTaxThreshold = new(1, "John", "Smith", 24, TaxThresholdOption.Y);
    private readonly Employee employeeNoTaxThreshold = new(2, "Alexander", "Johnathan", 27, TaxThresholdOption.N);

    [Test]
    public void PayCalculator_CalculatesPaySlip_WithTaxThreshold()
    {
        const decimal grossPay = 960m;
        const decimal taxThreshold = 865m;
        const decimal tax = 147.924323m;
        const decimal superannuation = 100.8m;

        var expectedPaySlip =
            new PaySlip(
                employeeWithTaxThreshold.Id,
                employeeWithTaxThreshold.GetFullName(),
                HoursWorked,
                employeeWithTaxThreshold.HourlyRate,
                taxThreshold,
                grossPay,
                tax,
                superannuation);

        var calculator = PayCalculator.CreateNew(employeeWithTaxThreshold, HoursWorked);
        calculator.CreatePaySlip().Should().BeEquivalentTo(expectedPaySlip);
    }

    [Test]
    public void PayCalculator_CalculatesPaySlip_NoTaxThreshold()
    {
        const decimal grossPay = 1080m;
        const decimal taxThreshold = 932m;
        const decimal tax = 311.02835m;
        const decimal superannuation = 113.4m;

        var expectedPaySlip =
            new PaySlip(
                employeeNoTaxThreshold.Id,
                employeeNoTaxThreshold.GetFullName(),
                HoursWorked,
                employeeNoTaxThreshold.HourlyRate,
                taxThreshold,
                grossPay,
                tax,
                superannuation);

        var calculator = PayCalculator.CreateNew(employeeNoTaxThreshold, HoursWorked);
        calculator.CreatePaySlip().Should().BeEquivalentTo(expectedPaySlip);
    }
}
