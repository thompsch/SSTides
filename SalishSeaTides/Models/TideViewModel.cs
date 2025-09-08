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
            SelectedStation = Station.Stations.First(s => s.StationId == 9447856);
        }
    }

    private void CheckForRefresh()
    {
        if (_tideModel == null || _tides == null || _tideModel.StationName != SelectedStation.Name || //wrong station
            _tideModel.BeginDate == null || _tideModel.BeginDate.Substring(0, 4) != SelectedDateTime.Year.ToString() ||
            _tides.Count <= 0 || _tideModel.Items.Count <= 0)
        {

            Console.WriteLine("New tides needed");
            _tides = new List<Tide>();
            SelectedTides.Clear();
            _tideModel = XmlToTideHandler.GetAllTides(SelectedStation.StationId).Result;

            if (_tideModel == null)
            {
                return;
                //TODO: popup to warn user that tides are missing
            }

            foreach (var item in _tideModel.Items)
            {
                string time = item.Time;
                var format = "yyyy/MM/dd'T'hh:mm tt";

                DateTime tideDateTime = DateTime.ParseExact($"{item.Date}T{time}", format, null);

                var newTide = new Tide(item.PredInFt, tideDateTime, item.HighLow);
                newTide.ShortDate = $"{item.Day},{item.Date.Substring(5)} {item.Time}";


                _tides.Add(newTide);
                if (tideDateTime.DayOfYear <= SelectedDateTime.DayOfYear + _daysAfter &&
                    tideDateTime.DayOfYear >= SelectedDateTime.DayOfYear - _daysBefore)
                {
                    Console.WriteLine(tideDateTime.DayOfYear);
                    SelectedTides.Add(newTide);
                }
            }
        }
    }
}

public partial class Tide
{
}