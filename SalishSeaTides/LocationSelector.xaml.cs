using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SalishSeaTides.Models;


namespace SalishSeaTides;

public partial class LocationSelector : ContentPage
{
    public Dictionary<Station, Tuple<double, double>> StationLocationOnMap;

    public LocationSelector()
    {
        InitializeComponent();
        GenerateStationList();
    }

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        Point? position = e.GetPosition((Image)sender);
        if (!position.HasValue) return;
        
        var x = position.Value.X;
        var y = position.Value.Y;
        // Console.WriteLine($"{x},{y}");
        TideViewModel.SelectedStation = FindNearestStation(x, y);
        await Navigation.PopModalAsync();
    }

    private void GenerateStationList()
    {
        StationLocationOnMap =
            new Dictionary<Station, Tuple<double, double>>();
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9447856),
            Tuple.Create(282.6558779761905, 337.8887648809524)); //Sandy Point
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9447855),
            Tuple.Create(243.41927083333334, 340.1767113095238)); //Holmes Harbor
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9444900),
            Tuple.Create(192.3671875, 308.9267113095238)); //Port Townsend
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9447929),
            Tuple.Create(199.22284226190476, 267.7715773809524)); //Coupeville
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9448794),
            Tuple.Create(226.26785714285714, 169.5014880952381)); //Anacortes
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449982),
            Tuple.Create(186.27752976190476,201.8954613095238)); //Lopez
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449771),
            Tuple.Create(166.08779761904762,126.44903273809524)); //Orcas
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449880),
            Tuple.Create(132.9404761904762,161.4936755952381)); //Friday Harbor
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449746),
            Tuple.Create(124.17671130952381,106.63876488095238)); //Waldron
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449856),
            Tuple.Create(119.22953869047619,180.93154761904762)); //Kanaka Bay
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9449828),
            Tuple.Create(97.89732142857143,139.02306547619048)); //Hanbury / Roche Harbor
        StationLocationOnMap.Add(Station.Stations.First(s => s.StationId == 9444090),
            Tuple.Create(35.796502976190474,306.26636904761904)); //Port Angeles 
    }

    private Station FindNearestStation(double x, double y)
    {
        // Variable to keep track of the closest point  
        int closestPointId = -1;
        double minDistance = double.MaxValue;

        // Iterate through each point in the dictionary  
        foreach (var kvp in StationLocationOnMap)
        {
            int pointId = kvp.Key.StationId;
            var point = kvp.Value;
            
            double distance = CalculateDistance(x, y, point);

            // Update the closest point if necessary  
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPointId = pointId;
            }
        }

        // Output the closest point  
        var closestPoint = StationLocationOnMap.First(a => a.Key.StationId == closestPointId);
        Console.WriteLine("Closest point to X is: {0}", closestPoint.Key.Name);
        return closestPoint.Key;
    }

    // Function to calculate Euclidean distance between two points  
    static double CalculateDistance(double x, double y, Tuple<double, double> p2)
    {
        double dx = p2.Item1 - x;
        double dy = p2.Item2 - y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
