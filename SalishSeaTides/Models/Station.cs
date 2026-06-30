using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Maps;

namespace SalishSeaTides.Models;

public class Station : MapMarker
{
    public string Label { get; set; }
    public string StationId { get; set; }

    public Station(double latitude, double longitude, string label, string stationId)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;    
        this.Label = label;
        this.StationId = stationId;
    }

    public static Station GetStationByLatLong(double latitude, double longitude)
    {
        return Stations.FirstOrDefault(s => s.Latitude == latitude && s.Longitude == longitude);
    }
    public static ObservableCollection<Station> Stations { get; set; } = new ObservableCollection<Station>()
    {
        new Station(48.04,-122.38,"Sandy Point", "9447856"),
        new Station(48.0267,-122.535,"Holmes Harbor", "9447855"),        
        new Station(48.11290, -122.75950,"Port Townsend", "9444900"),
        new Station(48.22,-122.69,"Penn Cove", "9447929"),
        new Station(48.52,-122.62,"Anacortes", "9448794"),
        new Station(48.545,-123.01,"Friday Harbor", "9449880"),
        new Station(48.69,-123.04,"Waldron Island", "9449746"),
        new Station(48.6467,-122.87,"Rosario (Orcas Island)", "9449771"),
        new Station(48.447,-122.9,"Lopez Island", "9449982"),
        new Station(48.58,-123.17,"Hanbury Point (Roche Harbor)", "9449828"),
        new Station(48.49,-123.08,"Kanaka Bay (San Juan Island)", "9449856"),
        new Station(48.125,-123.44,"Port Angeles", "9444090")
    };
}