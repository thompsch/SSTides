using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Maps;

namespace SalishSeaTides.Models;

public partial class MapViewModel : ObservableObject
{
    TideViewModel _tideViewModel;

    [ObservableProperty]
    public ObservableCollection<Station> markerCollection = Station.Stations;

    public MapViewModel()
    {
    }

    public MapViewModel(TideViewModel tideViewModel) : this()
    {
        this._tideViewModel = tideViewModel;
    }

    [RelayCommand]
    public void MarkerSelected(object sender)
    {
        if (_tideViewModel != null && sender is Station station)
        {
            _tideViewModel.SelectedStation = station;
            var mainPage = Application.Current?.Windows[0]?.Page;
            mainPage.Navigation.PopModalAsync();
        }
    }
    
    [RelayCommand]
    private void ItemTapped(MapMarker clickedItem)
    {
        if (clickedItem == null) return;
        
        var station = Station.GetStationByLatLong(clickedItem.Latitude, clickedItem.Longitude);
        _tideViewModel.SelectedStation = station;
        var mainPage = Application.Current?.Windows[0]?.Page;
        mainPage.Navigation.PopModalAsync();
        // Example action: navigate or process the clicked item data
       // System.Diagnostics.Debug.WriteLine($"*********************************User tapped on: {clickedItem.Latitude}, {clickedItem.Longitude}");
    }
}