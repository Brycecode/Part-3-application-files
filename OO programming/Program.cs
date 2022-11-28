using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OO_programming;

file static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}

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
    /// Gets the first name of this employee.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name of this employee.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the hourly rate of this employee.
    /// </summary>
    internal decimal HourlyRate { get; }

    /// <summary>
    /// Gets the tax threshold for this employee.
    /// </summary>
    internal TaxThreshold TaxThreshold { get; }

    /// <summary>
    /// Initializes this employee record.
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <param name="firstName">The employee first name</param>
    /// <param name="lastName">The employee last name</param>
    /// <param name="hourlyRate">The employee hourly rate</param>
    /// <param name="taxThreshold">The employee tax threshold</param>
    public Employee(int id, string firstName, string lastName, decimal hourlyRate, TaxThreshold taxThreshold)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        HourlyRate = hourlyRate;
        TaxThreshold = taxThreshold;
    }

    public override string ToString() => $"{Id} - {FirstName} {LastName}";
}

/// <summary>
/// Enum representing whether tax threshold should be taken into account.
/// </summary>
internal enum TaxThreshold
{
    /// <summary>
    /// Tax threshold should NOT be taken into account.
    /// </summary>
    N,
    /// <summary>
    /// Tax threshold should be taken into account.
    /// </summary>
    Y
}

/// <summary>
/// Class a capture details accociated with an employee's pay slip record
/// </summary>
file sealed record PaySlip
{
}

/// <summary>
/// Base class to hold all Pay calculation functions
/// Default class behaviour is tax calculated with tax threshold applied
/// </summary>
internal abstract class PayCalculator
{
    internal abstract decimal CalculatePay();

    internal abstract decimal CalculateTax();

    internal abstract decimal CalculateSuperannuation();

    internal static PayCalculator CreateNew(Employee employee) => employee.TaxThreshold switch
    {
        TaxThreshold.Y => new PayCalculatorWithThreshold(),
        TaxThreshold.N => new PayCalculatorNoThreshold(),
        _ => throw new InvalidOperationException(),
    };
}

/// <summary>
/// Extends PayCalculator class handling No tax threshold
/// </summary>
file sealed class PayCalculatorNoThreshold : PayCalculator
{
    internal override decimal CalculatePay() => 0;

    internal override decimal CalculateSuperannuation() => 0;

    internal override decimal CalculateTax() => 0;
}

/// <summary>
/// Extends PayCalculator class handling With tax threshold
/// </summary>
file sealed class PayCalculatorWithThreshold : PayCalculator
{
    internal override decimal CalculatePay() => 0;

    internal override decimal CalculateSuperannuation() => 0;

    internal override decimal CalculateTax() => 0;
}

/// <summary>
/// Extension methods for reading from and writing to CSV files.
/// </summary>
internal static class CsvExtensions
{
    private static readonly CsvConfiguration csvConfiguration = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = false
    };

    /// <summary>
    /// Reads from a CSV file in the provided path.
    /// </summary>
    /// <typeparam name="T">The type to convert each CSV record to</typeparam>
    /// <param name="filePath">The CSV file path</param>
    /// <returns>Records read from the CSV file as instances of <typeparamref name="T"/></returns>
    public static IList<T> ReadCsv<T>(this string filePath)
    {
        using var reader = File.OpenText($"Resources/{filePath}.csv");
        using var csv = new CsvReader(reader, csvConfiguration);
        return csv.GetRecords<T>().ToList();
    }
}
