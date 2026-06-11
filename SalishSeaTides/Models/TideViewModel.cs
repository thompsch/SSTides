using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;

namespace SalishSeaTides.Models;

public partial class TideViewModel : ObservableObject
{
    private DataTemplate circleTemplate;
    
    private DataTemplate template;

    public DataTemplate Template
    {
        get
        {
            return this.template;
        }
        set
        {
            this.template = value;
        }
    }
    
    
    [ObservableProperty]
    private DateTime selectedDateTime = DateTime.Now;

    [ObservableProperty]
    private DateTime nowDateTime = DateTime.Now;

    [ObservableProperty]
    private bool isNowVisible;

    [ObservableProperty] public Station selectedStation;

    private List<Tide> _tides = new ();
    private TideModel _tideModel = new();
    private readonly int _daysBefore = 0;
    private readonly int _daysAfter = 2;

    // this updates the graph but borks Android:
    public ObservableCollection<Tide> SelectedTides { get; set; } = new();

    private readonly ObservableCollection<Tide> _allTides = new();
    [ObservableProperty]
    private ObservableCollection<TideGroup> _groupedTides = new();

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        CheckForRefresh();
    }

    public TideViewModel()
    {
        if (SelectedStation == null)
        {
            SelectedStation = Station.Stations.First(s => s.StationId == "9447856");
        }
        MakeMonthTemplate();
        UpdateNowVisibility();
        StartNowTimer();
    }

    private void StartNowTimer()
    {
        var timer = Application.Current!.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMinutes(10);
        timer.Tick += (_, _) =>
        {
            NowDateTime = DateTime.Now;
            UpdateNowVisibility();
        };
        timer.Start();
    }

    private void UpdateNowVisibility()
    {
        var today = DateTime.Today;
        IsNowVisible = today >= SelectedDateTime.Date.AddDays(-_daysBefore)
                    && today <= SelectedDateTime.Date.AddDays(_daysAfter);
    }

    partial void OnSelectedDateTimeChanged(DateTime oldValue, DateTime newValue) => UpdateNowVisibility();

    private void CheckForRefresh()
    {
        {
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
            else if (SelectedTides.Count <= 0 || SelectedTides.First().TideDateTime.DayOfYear != SelectedDateTime.DayOfYear - _daysBefore)
            {
                //TODO: just get from _tides, or remove this check altogether
                GetNewDates();
            }
        }
    }

    private void GetNewDates()
    {
        _tides.Clear();
        SelectedTides.Clear();
        
        var min = _tideModel.Items.Min(t => Convert.ToDouble(t.PredInFt));
        foreach (var item in _tideModel.Items)
        {
            string time = item.Time;
            var format = "yyyy/MM/dd'T'hh:mm tt";

            DateTime tideDateTime = DateTime.ParseExact($"{item.Date}T{time}", format, null);

            string highlow = item.HighLow == "H"? "High" : "Low";
            var newTide = new Tide(item.PredInFt, tideDateTime, highlow);
            newTide.DisplayTimeForTable = item.Time;
            newTide.DisplayColorForTable = item.HighLow == "H" ? 
                Application.Current.Resources["HighTide"] as Color :
                Application.Current.Resources["LowTide"] as Color;
            
            _tides.Add(newTide);
            if (tideDateTime.DayOfYear <= SelectedDateTime.DayOfYear + _daysAfter &&
                tideDateTime.DayOfYear >= SelectedDateTime.DayOfYear - _daysBefore)
            {
                SelectedTides.Add(newTide);
            }
        }
        
        var groups = SelectedTides
            .OrderBy(x => x.TideDateTime) // Sort by date
            .GroupBy(x => x.TideDateTime.Date.ToString("ddd, MMM. dd, yyyy")) // Group by date as a string
            .Select(g => new TideGroup(g.Key, g))
            .ToList();
        
        GroupedTides = new ObservableCollection<TideGroup>(groups);
    }

    private void MakeMonthTemplate()
    {
        this.circleTemplate = new DataTemplate(() =>
        {
            Grid grid = new Grid();

            Border border = new Border();
            border.BackgroundColor = Color.FromRgba("#F5F5F5");
            border.StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(25)
            };

            border.SetBinding(Border.StrokeThicknessProperty, "Date");
            border.Stroke = (Color)Application.Current.Resources["LowTide"];
           //     Color.FromArgb("#000");//("#0A3A74");

            Label label = new Label();
            label.TextColor = Color.FromArgb("000");
            label.SetBinding(Label.TextProperty, "Date.Day");
            label.HorizontalOptions = LayoutOptions.Center;
            label.VerticalOptions = LayoutOptions.Center;
            label.Padding = new Thickness(2);
            border.Content = label;

            grid.Add(border);
            grid.Padding = new Thickness(1);
            grid.HeightRequest = 25;

            return grid;
        });

        this.template = this.circleTemplate;
    }       
}