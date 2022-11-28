using System;
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
