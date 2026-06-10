using SalishSeaTides.Models;
using Xunit;

namespace SalishSeaTides.Tests;

public class StationTests
{
    [Fact]
    public void Stations_HasTwelveEntries()
    {
        Assert.Equal(12, Station.Stations.Count);
    }

    [Fact]
    public void Stations_AllHaveNonEmptyStationId()
    {
        Assert.All(Station.Stations, s => Assert.False(string.IsNullOrWhiteSpace(s.StationId)));
    }

    [Fact]
    public void Stations_AllHaveNonEmptyName()
    {
        Assert.All(Station.Stations, s => Assert.False(string.IsNullOrWhiteSpace(s.Name)));
    }

    [Fact]
    public void Stations_AllHaveUniqueStationIds()
    {
        var ids = Station.Stations.Select(s => s.StationId).ToList();
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Fact]
    public void Stations_CoordinatesAreWithinSalishSeaRange()
    {
        Assert.All(Station.Stations, s =>
        {
            Assert.InRange(s.Latitude, 47.5, 49.0);
            Assert.InRange(s.Longitude, -124.0, -122.0);
        });
    }

    [Theory]
    [InlineData("9447856", "Sandy Point")]
    [InlineData("9444900", "Port Townsend")]
    [InlineData("9449880", "Friday Harbor")]
    [InlineData("9448794", "Anacortes")]
    [InlineData("9444090", "Port Angeles")]
    public void Stations_ContainsExpectedStation(string stationId, string name)
    {
        Assert.Contains(Station.Stations, s => s.StationId == stationId && s.Name == name);
    }

    [Fact]
    public void Stations_DefaultStationExists()
    {
        Assert.NotNull(Station.Stations.FirstOrDefault(s => s.StationId == "9447856"));
    }
}
