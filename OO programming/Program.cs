using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
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
/// Class a capture details accociated with an employee's pay slip record
/// </summary>
file sealed record PaySlip
{
}

/// <summary>
/// Base class to hold all Pay calculation functions
/// Default class behaviour is tax calculated with tax threshold applied
/// </summary>
file abstract class PayCalculator
{
}

/// <summary>
/// Extends PayCalculator class handling No tax threshold
/// </summary>
file sealed class PayCalculatorNoThreshold : PayCalculator
{
}

/// <summary>
/// Extends PayCalculator class handling With tax threshold
/// </summary>
file sealed class PayCalculatorWithThreshold : PayCalculator
{
}

/// <summary>
/// Extension methods for reading from and writing to CSV files.
/// </summary>
file static class CsvExtensions
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
        using var reader = File.OpenText(filePath);
        using var csv = new CsvReader(reader, csvConfiguration);
        return csv.GetRecords<T>().ToList();
    }
}
