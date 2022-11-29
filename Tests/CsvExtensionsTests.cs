using FluentAssertions;
using NUnit.Framework;
using OO_programming;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tests;

/// <summary>
/// Unit tests for extension methods responsible for serialization and deserialization of CSV values.
/// </summary>
public sealed class CsvExtensionsTests
{
    private static readonly IReadOnlyCollection<Employee> employees = new Employee[] {
        new(1,"Marge","Student",25m,TaxThresholdOption.Y),
        new(2,"Mindy","Student",25m,TaxThresholdOption.Y),
        new(3,"Mike","Student",25m,TaxThresholdOption.Y),
        new(4,"Mary","Student",25m,TaxThresholdOption.Y),
        new(5,"Minh","Student",25m,TaxThresholdOption.Y),
        new(6,"Mohammad","Student",25m,TaxThresholdOption.Y),
        new(7,"Moanna","Student",25m,TaxThresholdOption.N)
    };

    private static readonly IReadOnlyCollection<TaxThreshold> taxRateNoThreshold = new TaxThreshold[] {
        new(0m,88m,0.1900m,0.1900m),
        new(88m,371m,0.2348m,3.9639m),
        new(371m,515m,0.2190m,-1.9003m),
        new(515m,932m,0.3477m,64.4297m),
        new(932m,1957m,0.3450m,61.9132m),
        new(957m,3111m,0.3900m,150.0093m),
        new(3111m,99999999m,0.4700m,398.9324m)
    };

    private static readonly IReadOnlyCollection<TaxThreshold> taxRateWithThreshold = new TaxThreshold[] {
        new(0m,359m,0m,0m),
        new(359m,438m,0.1900m,68.3462m),
        new(438m,548m,0.2900m,112.1942m),
        new(548m,721m,0.2100m,68.3465m),
        new(721m,865m,0.2190m,74.8369m),
        new(865m,1282m,0.3477m,186.2119m),
        new(1282m,2307m,0.3450m,182.7504m),
        new(2307m,3461m,0.3900m,286.5965m),
        new(3461m,99999999m,0.4700m,563.5196m)
    };

    [TearDown]
    public async Task TearDownAsync()
    {
        var disposals = new[] { nameof(WriteCsv_WritesToFile_Employee), nameof(WriteCsv_WritesToFile_TaxRate_NoThreshold), nameof(WriteCsv_WritesToFile_TaxRate_WithThreshold) }
        .Select(fileName => $"{fileName}.csv")
        .Select(async fileName => await Task.Run(() => File.Delete(fileName)));

        await Task.WhenAll(disposals);
    }

    [Test]
    public void ReadCsv_ReadsFromFile_Employee()
    {
        "employee".ReadCsv<Employee>().Should().BeEquivalentTo(employees);
    }

    [Test]
    public void ReadCsv_ReadsFromFile_TaxRate_NoThreshold()
    {
        "taxrate-nothreshold".ReadCsv<TaxThreshold>().Should().BeEquivalentTo(taxRateNoThreshold);
    }

    [Test]
    public void ReadCsv_ReadsFromFile_TaxRate_WithThreshold()
    {
        "taxrate-withthreshold".ReadCsv<TaxThreshold>().Should().BeEquivalentTo(taxRateWithThreshold);
    }

    [Test]
    public void WriteCsv_WritesToFile_Employee()
    {
        const string fileName = nameof(WriteCsv_WritesToFile_Employee);
        var expectedEmployee = employees.First();

        fileName.WriteCsv(expectedEmployee);
        fileName.ReadCsv<Employee>(string.Empty).Should().ContainSingle().Which.Should().BeEquivalentTo(expectedEmployee);
    }

    [Test]
    public void WriteCsv_WritesToFile_TaxRate_NoThreshold()
    {
        const string fileName = nameof(WriteCsv_WritesToFile_TaxRate_NoThreshold);
        var expectedTaxThreshold = taxRateNoThreshold.First();

        fileName.WriteCsv(expectedTaxThreshold);
        fileName.ReadCsv<TaxThreshold>(string.Empty).Should().ContainSingle().Which.Should().BeEquivalentTo(expectedTaxThreshold);
    }

    [Test]
    public void WriteCsv_WritesToFile_TaxRate_WithThreshold()
    {
        const string fileName = nameof(WriteCsv_WritesToFile_TaxRate_WithThreshold);
        var expectedTaxThreshold = taxRateWithThreshold.First();

        fileName.WriteCsv(expectedTaxThreshold);
        fileName.ReadCsv<TaxThreshold>(string.Empty).Should().ContainSingle().Which.Should().BeEquivalentTo(expectedTaxThreshold);
    }
}