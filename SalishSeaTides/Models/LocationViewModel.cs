using CommunityToolkit.Mvvm.ComponentModel;

namespace SalishSeaTides.Models;

public partial class LocationViewModel : ObservableObject
{
    TideViewModel _tideViewModel;
    
    [ObservableProperty] public static List<DisplayStation> stationLocations = new();
    [ObservableProperty] public int selectedIndex;
    [ObservableProperty] public DisplayStation selectedItem;

    public LocationViewModel()
    {
    }

    public LocationViewModel(TideViewModel tideViewModel)
    {
        this._tideViewModel = tideViewModel;
        
        StationLocations = new();
        
        StationLocations.Add(new DisplayStation("9447856", "Sandy Point",
            Tuple.Create(282.6558779761905, 337.8887648809524)));
        StationLocations.Add(new DisplayStation("9447855", "Holmes Harbor",
            Tuple.Create(243.41927083333334, 340.1767113095238)));
        StationLocations.Add(new DisplayStation("9444900", "Port Townsend",
            Tuple.Create(192.3671875, 308.9267113095238)));
        StationLocations.Add(new DisplayStation("9447929", "Penn Cove (Coupeville)",
            Tuple.Create(199.22284226190476, 267.7715773809524)));
        StationLocations.Add(new DisplayStation("9448794", "Anacortes",
            Tuple.Create(226.26785714285714, 169.5014880952381)));
        StationLocations.Add(new DisplayStation("9449982", "Lopez Island", 
            Tuple.Create(186.27752976190476,201.8954613095238)));
        StationLocations.Add(new DisplayStation("9449771", "Rosario (Orcas Island)",
            Tuple.Create(166.08779761904762,126.44903273809524))); 
        StationLocations.Add(new DisplayStation("9449880", "Friday Harbor",
            Tuple.Create(132.9404761904762,161.4936755952381))); 
        StationLocations.Add(new DisplayStation("9449746", "Waldron Island",
            Tuple.Create(124.17671130952381,106.63876488095238))); 
        StationLocations.Add(new DisplayStation("9449856", "Kanaka Bay (San Juan Island)",
            Tuple.Create(119.22953869047619,180.93154761904762))); 
        StationLocations.Add(new DisplayStation("9449828", "Hanbury (Roche Harbor)",
            Tuple.Create(97.89732142857143,139.02306547619048))); 
        StationLocations.Add(new DisplayStation("9444090", "Port Angeles",
            Tuple.Create(35.796502976190474,306.26636904761904))); 
        
        SetSelectedStationIndex();
        
    }

    public void SetSelectedStationIndex()
    {
        var selectedStation = _tideViewModel.SelectedStation;

        SelectedItem = stationLocations.First(s => s.StationId == selectedStation.StationId);
        
        for (int i = 0; i < stationLocations.Count; i++)
        {
            if (stationLocations[i].StationId == selectedStation.StationId)
            {
                SelectedIndex = i;
                return;
            }
        }
        SelectedIndex = -1;
    }
    
    public void SetSelectedStation(Station station)
    {
        _tideViewModel.SelectedStation = station;
    }
}

public class DisplayStation
{
    public string StationId { get; set; }
    public string StationName { get; set; }
    public Tuple<double, double> StationMapLocation { get; set; }

    public DisplayStation(string stationId, string stationName, Tuple<double, double> stationMapLocation)
    {
        this.StationId = stationId;
        this.StationName = stationName;
        this.StationMapLocation = stationMapLocation;
    }
}