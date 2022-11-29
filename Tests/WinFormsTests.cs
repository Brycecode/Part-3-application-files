using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Extensions.Forms;
using NUnit.Framework;
using OO_programming;
using System.Globalization;
using System.Linq;

namespace Tests;

/// <summary>
/// UI/Integration tests to ensure all Windows Forms components work
/// as expected and calculations yield the expected results.
/// </summary>
public sealed class WinFormsTests
{
    private Form1 form = new();
    private ListBoxTester listBoxTester;
    private TextBoxTester textBox1Tester;
    private TextBoxTester textBox2Tester;
    private ButtonTester button1Tester;
    private ButtonTester button2Tester;

    [SetUp]
    public void SetUp()
    {
        form = new();

        listBoxTester = new("listBox1", form);
        textBox1Tester = new("textBox1", form);
        textBox2Tester = new("textBox2", form);
        button1Tester = new("button1", form);
        button2Tester = new("button2", form);

        form.Show(); // Not needed, but useful.
    }

    [TearDown]
    public void TearDown()
    {
        form.Hide();
        form.Dispose();
    }

    [Test]
    public void Ctor_PopulatesListBox_WithEmployeeData()
    {
        listBoxTester.Properties.DataSource.Should().BeEquivalentTo("employee".ReadCsv<Employee>().ToArray());
    }

    [Test]
    public void CalculateButton_CalculatesPaySlip_WithHoursWorkedAndSelectedItem()
    {
        const decimal hoursWorked = 60;
        var textBox1 = textBox1Tester.Properties;
        textBox1.Text = hoursWorked.ToString(CultureInfo.InvariantCulture);

        button1Tester.Properties.PerformClick(); // Trigger `Button1_Click`

        var employee = (Employee)listBoxTester.Properties.SelectedItem;

        var paySlip = PayCalculator.CreateNew(employee, hoursWorked).CreatePaySlip();

        textBox2Tester.Properties.Text.Should().BeEquivalentTo(paySlip.ToString());
    }

    [Test]
    public void CalculateButton_MakesExportButtonVisible()
    {
        var button2 = button2Tester.Properties;
        using var _ = new AssertionScope();

        button2.Visible.Should().BeFalse();

        button1Tester.Properties.PerformClick(); // Trigger `Button1_Click`

        button2.Visible.Should().BeTrue();
    }

    [Test]
    public void ExportButton_ExportsToCsv()
    {
        const decimal hoursWorked = 44;
        var textBox1 = textBox1Tester.Properties;
        textBox1.Text = hoursWorked.ToString(CultureInfo.InvariantCulture);

        button1Tester.Properties.PerformClick(); // Trigger `Button1_Click`

        var employee = (Employee)listBoxTester.Properties.SelectedItem;

        button2Tester.Properties.PerformClick(); // Export CSV

        var expectedPaySlip = PayCalculator.CreateNew(employee, hoursWorked).CreatePaySlip();

        form
            .LastCsvFileExported
            .ReadCsv<PaySlip>(string.Empty)
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(expectedPaySlip);
    }
}
