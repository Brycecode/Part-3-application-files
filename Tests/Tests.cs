using FluentAssertions;
using NUnit.Framework;

namespace Tests;

public sealed class Tests
{
    [Test]
    public void Test()
    {
        string.Empty.Should().BeEmpty();
    }
}