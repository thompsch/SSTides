using System.Collections.Generic;

namespace SalishSeaTides.Models;

public class Station
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public int StationId { get; set; }

    private Station(double latitude, double longitude, string name, int stationId)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;    
        this.Name = name;
        this.StationId = stationId;
    }
    public static List<Station> Stations { get; set; } = new List<Station>()
    {
        new Station(48.04,-122.38,"Sandy Point", 9447856),
        new Station(48.0267,-122.535,"Holmes Harbor", 9447855),        
        new Station(48.11290, -122.75950,"Port Townsend", 9444900),
        new Station(48.035,-122.377,"Coupeville, Penn Cove", 9447929),
        new Station(48.52,-122.62,"Anacortes", 9448794),
        new Station(48.545,-123.01,"Friday Harbor", 9449880),
        new Station(48.69,-123.04,"Waldron Island", 9449746),
        new Station(48.6467,-122.87,"Rosario, Orcas Island", 9449771),
        new Station(48.447,-122.9,"Lopez Island", 9449982),
        new Station(48.58,-123.17,"Hanbury Point (Roche Harbor)", 9449828),
        new Station(48.49,-123.08,"Kanaka Bay, San Juan Island", 9449856),
        new Station(48.125,-123.44,"Port Angeles", 9444090)
    };
}