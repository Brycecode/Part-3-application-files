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

    /// <summary>
    /// Writes the provided object model to a CSV file in the specified path.
    /// </summary>
    /// <typeparam name="T">The object model type</typeparam>
    /// <param name="filePath">Path to save the CSV record to</param>
    /// <param name="model">Model to write to CSV file</param>
    public static void WriteCsv<T>(this string filePath, T model) where T : class
    {
        using var writer = new StreamWriter($"{filePath}.csv");
        using var csv = new CsvWriter(writer, csvConfiguration);
        csv.WriteRecord(model);
    }
}
