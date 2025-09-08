using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace SalishSeaTides.Models;

public partial class TideViewModel : ObservableObject
{
    [ObservableProperty] 
    private DateTime selectedDateTime = DateTime.Now;

    [ObservableProperty] private Station selectedStation;

    private List<Tide> _tides = new List<Tide>();
    private TideModel _tideModel = new TideModel();
    private readonly int _daysBefore = 0;
    private readonly int _daysAfter = 2;

    public ObservableCollection<Tide> SelectedTides { get; set; } = new ObservableCollection<Tide>();

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        CheckForRefresh();
    }

    public TideViewModel()
    {
        if (selectedStation == null)
        {
            SelectedStation = Station.Stations.First(s => s.StationId == "9447856");
        }
    }

    private void CheckForRefresh()
    {
        {
            // New Station == new need _tideModel
            // _tideModel can be null despite Rider information
            if (_tideModel == null || _tideModel.StationId != SelectedStation.StationId)
            {
                Console.WriteLine("New tides needed");
                _tides = new List<Tide>();
                _tideModel = XmlToTideHandler.GetAllTides(SelectedStation.StationId).Result;

                // _tideModel can be null despite Rider information
                if (_tideModel == null)
                {
                    return;
                    
                    //TODO: popup to warn user that tides are missing
                }

                GetNewDates();
            }
            // date changed, but not the station, so just extract from the existing _tideModel
            else if (SelectedTides.First().TideDateTime.DayOfYear != SelectedDateTime.DayOfYear - _daysBefore)
            {
                GetNewDates();
            }
        }
    }

    private void GetNewDates()
    {
        var min = _tideModel.Items.Min(t => Convert.ToDouble(t.PredInFt));
        SelectedTides.Clear();
        foreach (var item in _tideModel.Items)
        {
            string time = item.Time;
            var format = "yyyy/MM/dd'T'hh:mm tt";

            DateTime tideDateTime = DateTime.ParseExact($"{item.Date}T{time}", format, null);

            var newTide = new Tide(item.PredInFt, tideDateTime, item.HighLow);
            newTide.ShortDate = $"{item.Day}, {item.Date.Substring(5)} {item.Time}";


            _tides.Add(newTide);
            if (tideDateTime.DayOfYear <= SelectedDateTime.DayOfYear + _daysAfter &&
                tideDateTime.DayOfYear >= SelectedDateTime.DayOfYear - _daysBefore)
            {
                //Console.WriteLine(tideDateTime.DayOfYear);
                SelectedTides.Add(newTide);
            }
        }
    }
}

