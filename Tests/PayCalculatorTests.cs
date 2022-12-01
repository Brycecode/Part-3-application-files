using FluentAssertions;
using NUnit.Framework;
using OO_programming;
using System.Collections.Generic;

namespace Tests;

/// <summary>
/// Integration tests that ensure the pay calculator is correctly reading from the CSV data files,
/// filtering the correct tax threshold to apply, and calculating the pay slip correctly.
/// </summary>
public sealed class PayCalculatorTests
{
    private static IEnumerable<TestCaseData> GetData_WithThreshold() => new TestCaseData[]
    {
        new(new Employee(
            id: 1,
            firstName: "Alexander",
            lastName: "Johnathan",
            hourlyRate: 359m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 1,
                fullName: "Alexander Johnathan",
                hoursWorked: 1m,
                hourlyRate: 359m,
                TaxThresholdOption.Y,
                grossPay: 359m,
                tax: 0,
                superannuation: 37.695m)),
        new(new Employee(
            id: 2,
            firstName: "Rudolph",
            lastName: "Sims",
            hourlyRate: 40m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 2,
                fullName: "Rudolph Sims",
                hoursWorked: 10m,
                hourlyRate: 40m,
                TaxThresholdOption.Y,
                grossPay: 400m,
                tax: 7.8419m, // 0.19 * 400.99 - 68.3462
                superannuation: 42.000m)),
        new(new Employee(
            id: 3,
            firstName: "Bob",
            lastName: "Mark",
            hourlyRate: 5.48m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 3,
                fullName: "Bob Mark",
                hoursWorked: 100,
                hourlyRate: 5.48m,
                TaxThresholdOption.Y,
                grossPay: 548m,
                tax: 47.0129m, // 0.29 * 548.99 - 112.1942
                superannuation: 57.54m)),
        new(new Employee(
            id: 4,
            firstName: "Alexander",
            lastName: "Johnathan",
            hourlyRate: 350m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 4,
                fullName: "Alexander Johnathan",
                hoursWorked: 2,
                hourlyRate: 350m,
                TaxThresholdOption.Y,
                grossPay: 700m,
                tax: 78.8614m, // 0.21 * 700.99 - 68.3465
                superannuation: 73.5m)),
        new(new Employee(
            id: 5,
            firstName: "Luke",
            lastName: "Markus",
            hourlyRate: 86m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 5,
                fullName: "Luke Markus",
                hoursWorked: 10m,
                hourlyRate: 86m,
                TaxThresholdOption.Y,
                grossPay: 860m,
                tax: 113.71991m, // 0.219 * 860.99 - 74.8369
                superannuation: 90.3m)),
        new(new Employee(
            id: 6,
            firstName: "Samuel",
            lastName: "Sang",
            hourlyRate: 12.80m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 6,
                fullName: "Samuel Sang",
                hoursWorked: 100m,
                hourlyRate: 12.80m,
                TaxThresholdOption.Y,
                grossPay: 1280m,
                tax: 259.188323m, // 0.3477 * 1280.99 - 186.2119
                superannuation: 134.4m)),
        new(new Employee(
            id: 7,
            firstName: "Alexander",
            lastName: "Johnathan",
            hourlyRate: 23m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 7,
                fullName: "Alexander Johnathan",
                hoursWorked: 100m,
                hourlyRate: 23m,
                TaxThresholdOption.Y,
                grossPay: 2300,
                tax: 611.09115m, // 0.345 * 2300.99 - 182.7504
                superannuation: 241.5m)),
        new(new Employee(
            id: 8,
            firstName: "Khaled",
            lastName: "Scribble",
            hourlyRate: 34.61m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 8,
                fullName: "Khaled Scribble",
                hoursWorked: 100m,
                hourlyRate: 34.61m,
                TaxThresholdOption.Y,
                grossPay: 3461m,
                tax: 1063.5796m, // 0.39 * 3461.99 - 286.5965
                superannuation: 363.405m)),
        new(new Employee(
            id: 9,
            firstName: "Peter",
            lastName: "Twain",
            hourlyRate: 36m,
            TaxThresholdOption.Y),
            new PaySlip(
                id: 9,
                fullName: "Peter Twain",
                hoursWorked: 100m,
                hourlyRate: 36m,
                TaxThresholdOption.Y,
                grossPay: 3600m,
                tax: 1128.9457m, // 0.47 * 3600.99 - 563.5196
                superannuation: 378m)),
    };

    private static IEnumerable<TestCaseData> GetData_NoThreshold() => new TestCaseData[]
    {
        new(new Employee(
            id: 1,
            firstName: "John",
            lastName: "Smith",
            hourlyRate: 44m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 1,
                fullName: "John Smith",
                hoursWorked: 2m,
                hourlyRate: 44m,
                TaxThresholdOption.N,
                grossPay: 88m,
                tax: 16.7181m, // 0.19 * 88.99 - 0.19
                superannuation: 9.24m)),
        new(new Employee(
            id: 2,
            firstName: "Max",
            lastName: "Lewis",
            hourlyRate: 36m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 2,
                fullName: "Max Lewis",
                hoursWorked: 10m,
                hourlyRate: 36m,
                TaxThresholdOption.N,
                grossPay: 360m,
                tax: 80.796552m, // 0.2348 * 360.99 - 3.9639
                superannuation: 37.8m)),
        new(new Employee(
            id: 3,
            firstName: "George",
            lastName: "Bush",
            hourlyRate: 37.2m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 3,
                fullName: "George Bush",
                hoursWorked: 10m,
                hourlyRate: 37.2m,
                TaxThresholdOption.N,
                grossPay: 372m,
                tax: 83.58511m, // 0.219 * 372.99 + 1.9003
                superannuation: 39.06m)),
        new(new Employee(
            id: 4,
            firstName: "Kant",
            lastName: "Smear",
            hourlyRate: 90m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 4,
                fullName: "Kant Smear",
                hoursWorked: 10m,
                hourlyRate: 90m,
                TaxThresholdOption.N,
                grossPay: 900m,
                tax: 248.844523m, // 0.3477 * 900.99 - 64.4297
                superannuation: 94.5m)),
        new(new Employee(
            id: 5,
            firstName: "Slope",
            lastName: "Cantalope",
            hourlyRate: 39.14m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 5,
                fullName: "Slope Cantalope",
                hoursWorked: 50m,
                hourlyRate: 39.14m,
                TaxThresholdOption.N,
                grossPay: 1957m,
                tax: 613.59335m, // 0.345 * 1957.99 - 61.9132
                superannuation: 205.485m)),
        new(new Employee(
            id: 6,
            firstName: "Barack",
            lastName: "Obama",
            hourlyRate: 31.11m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 6,
                fullName: "Barack Obama",
                hoursWorked: 100m,
                hourlyRate: 31.11m,
                TaxThresholdOption.N,
                grossPay: 3111m,
                tax: 1063.6668m, // 0.39 * 3111.99 - 150.0093
                superannuation: 326.655m)),
        new(new Employee(
            id: 7,
            firstName: "W",
            lastName: "Sauce",
            hourlyRate: 45m,
            TaxThresholdOption.N),
            new PaySlip(
                id: 7,
                fullName: "W Sauce",
                hoursWorked: 80m,
                hourlyRate: 45m,
                TaxThresholdOption.N,
                grossPay: 3600,
                tax: 1293.5329m, // 0.47 * 3600.99 - 398.9324
                superannuation: 378m)),
    };

    [Test, TestCaseSource(nameof(GetData_WithThreshold))]
    public void PayCalculator_CalculatesPaySlip_WithTaxThreshold(Employee employee, PaySlip expectedPaySlip)
    {
        var calculator = PayCalculator.CreateNew(employee, expectedPaySlip.HoursWorked);
        calculator.CreatePaySlip().Should().BeEquivalentTo(expectedPaySlip);
    }

    [Test, TestCaseSource(nameof(GetData_NoThreshold))]
#pragma warning disable S4144 // Methods should not have identical implementations
    public void PayCalculator_CalculatesPaySlip_NoTaxThreshold(Employee employee, PaySlip expectedPaySlip)
#pragma warning restore S4144 // Methods should not have identical implementations
    {
        var calculator = PayCalculator.CreateNew(employee, expectedPaySlip.HoursWorked);
        calculator.CreatePaySlip().Should().BeEquivalentTo(expectedPaySlip);
    }
}
