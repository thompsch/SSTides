using SalishSeaTides.Models;
using Xunit;

namespace SalishSeaTides.Tests;

public class TideTests
{
    [Theory]
    [InlineData("4.5", 4.5)]
    [InlineData("0.0", 0.0)]
    [InlineData("-1.5", -1.5)]
    [InlineData("13.2", 13.2)]
    public void Tide_ParsesHeightFromString(string input, double expected)
    {
        var tide = new Tide(input, DateTime.Now, "High");
        Assert.Equal(expected, tide.Height);
    }

    [Theory]
    [InlineData("High")]
    [InlineData("Low")]
    public void Tide_StoresTideType(string tideType)
    {
        var tide = new Tide("3.0", DateTime.Now, tideType);
        Assert.Equal(tideType, tide.TideType);
    }

    [Fact]
    public void Tide_StoresTideDateTime()
    {
        var dt = new DateTime(2024, 6, 15, 14, 30, 0);
        var tide = new Tide("4.1", dt, "High");
        Assert.Equal(dt, tide.TideDateTime);
    }

    [Fact]
    public void Tide_HeightIsSettable()
    {
        var tide = new Tide("3.0", DateTime.Now, "High");
        tide.Height = 7.5;
        Assert.Equal(7.5, tide.Height);
    }

    [Fact]
    public void Tide_DisplayTimeForTableIsSettable()
    {
        var tide = new Tide("3.0", DateTime.Now, "High");
        tide.DisplayTimeForTable = "06:17 AM";
        Assert.Equal("06:17 AM", tide.DisplayTimeForTable);
    }

    [Fact]
    public void Tide_InvalidHeightThrows()
    {
        Assert.Throws<FormatException>(() => new Tide("not-a-number", DateTime.Now, "High"));
    }
}

public class TideGroupTests
{
    [Fact]
    public void TideGroup_StoresdateCategory()
    {
        var group = new TideGroup("Mon, Jan. 01, 2024", []);
        Assert.Equal("Mon, Jan. 01, 2024", group.TideDateTime);
    }

    [Fact]
    public void TideGroup_ContainsProvidedTides()
    {
        var t1 = new Tide("4.5", new DateTime(2024, 1, 1, 6, 0, 0), "High");
        var t2 = new Tide("1.2", new DateTime(2024, 1, 1, 12, 0, 0), "Low");
        var group = new TideGroup("Mon, Jan. 01, 2024", [t1, t2]);

        Assert.Equal(2, group.Count);
        Assert.Contains(t1, group);
        Assert.Contains(t2, group);
    }

    [Fact]
    public void TideGroup_EmptyGroupHasZeroCount()
    {
        var group = new TideGroup("Mon, Jan. 01, 2024", []);
        Assert.Empty(group);
    }
}
