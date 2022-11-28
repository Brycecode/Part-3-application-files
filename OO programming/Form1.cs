using System;
using System.Globalization;
using System.Windows.Forms;

namespace OO_programming;

internal partial class Form1 : Form
{
    private EmployeePayReport report;

    public Form1()
    {
        InitializeComponent();

        // Add code below to complete the implementation to populate the listBox
        // by reading the employee.csv file into a List of PaySlip objects, then binding this to the ListBox.
        // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>

        listBox1.DataSource = "employee".ReadCsv<Employee>();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        // Add code below to complete the implementation to populate the
        // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
        // and PayCalculatorWithThresholds classes object and methods.

        var selectedEmployee = (Employee)listBox1.SelectedItem;
        var hoursWorked = decimal.Parse(textBox1.Text.Trim(), CultureInfo.InvariantCulture);
        var calculator = PayCalculator.CreateNew(selectedEmployee, hoursWorked);

        report = calculator.CreateReport();

        textBox2.Text = report.ToString();

        button2.Visible = true;
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        // Add code below to complete the implementation for saving the
        // calculated payment data into a csv file.
        // File naming convention: Pay_<full name>_<datetimenow>.csv
        // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation

        $"Pay_{report.FullName}_{DateTime.UtcNow:yyyyMMddHHmmssfff}.csv".WriteCsv(report);
    }
}
