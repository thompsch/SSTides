using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SalishSeaTides.Models;

public class TideViewModel : INotifyPropertyChanged
{
    public DateTime SelectedDateTime { get; set; } = DateTime.Now;
    public static Station SelectedStation { get; set; } = Station.Stations.First(s => s.StationId == 9447856);

    private List<Tide> _tides = new List<Tide>();
    private TideModel allTides = new TideModel();
    private int daysBefore = 0;
    private int daysAfter = 2;
    
    public ObservableCollection<Tide> SelectedTides { get; set; }  = new ObservableCollection<Tide>();
    //todo: create xml file with lat/long for each station

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public string StationName { get; set; }

    public TideViewModel()
    {
        if (NeedsRefresh()) {
            Console.WriteLine("New tides needed");
            allTides = XmlToTideHandler.GetAllTides(SelectedStation.StationId).Result;
        }
        
        foreach (var item in allTides.Items)
        {
            DateTime tideDateTime = DateTime.Parse($"{item.Date}T{item.Time}");
            var newTide = new Tide(item.PredInFt, tideDateTime , item.HighLow);
            newTide.ShortDate = $"{item.Day},{item.Date.Substring(5)} {item.Time}";
            
            _tides.Add(newTide);
            if (tideDateTime.DayOfYear <= SelectedDateTime.DayOfYear + daysAfter && tideDateTime.DayOfYear >= SelectedDateTime.DayOfYear - daysBefore)
            {
                Console.WriteLine(tideDateTime.DayOfYear);
                SelectedTides.Add(newTide);
            }
        }
        this.StationName = allTides.StationName;
    }

    private bool NeedsRefresh()
    {
        if (allTides == null || _tides == null) return true;
        
        if (allTides.StationName != StationName || //wrong station
            allTides.BeginDate == null || allTides.BeginDate.Substring(0, 4) != SelectedDateTime.Year.ToString() || 
            _tides.Count <= 0 || allTides.Items.Count <= 0) return true;
        
        return false;
    }
}

public class Tide(string height, DateTime tideDateTime, string tideType)
{
    public double Height { get; set; } = Convert.ToDouble(height);
    public DateTime TideDateTime { get; set; } = tideDateTime;
    public  string TideType { get; set; } = tideType; //H or L
    public string ShortDate {get; set;}

}