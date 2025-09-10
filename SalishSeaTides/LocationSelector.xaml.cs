using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SalishSeaTides.Models;


namespace SalishSeaTides;

public partial class LocationSelector : ContentPage
{
    private bool _isLoaded = false;
    
    private LocationViewModel _locationViewModel;
    
    public LocationSelector(LocationViewModel viewModel)
    {
        _locationViewModel = viewModel;
        InitializeComponent();
        this.IShouldNotNeedThis.SelectedIndex = viewModel.SelectedIndex;
        this.Loaded += async (sender, args) => _isLoaded = true;
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
        _locationViewModel.SetSelectedStation(FindNearestStation(x, y));
        await Navigation.PopModalAsync();
    }
    
    private Station FindNearestStation(double x, double y)
    {
        // Variable to keep track of the closest point  
        var closestPointId = String.Empty;
        var minDistance = double.MaxValue;

        // Iterate through each point in the dictionary  
        foreach (var station in LocationViewModel.stationLocations)
        {
            var pointId = station.StationId;
            var point = station.StationMapLocation;
            
            double distance = CalculateDistance(x, y, point);

            // Update the closest point if necessary  
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPointId = pointId;
            }
        }

        // Output the closest point  
        var closestPoint = LocationViewModel.stationLocations.First(a => a.StationId == closestPointId);
        Console.WriteLine("Closest point to X is: {0}", closestPoint.StationName);
        return Station.Stations.First(s=>s.StationId == closestPoint.StationId);
    }

    // Function to calculate Euclidean distance between two points  
    static double CalculateDistance(double x, double y, Tuple<double, double> p2)
    {
        double dx = p2.Item1 - x;
        double dy = p2.Item2 - y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private async void Picker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        // todo: this fires on load; we only want when it changes!
        if (_isLoaded)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                _locationViewModel.SetSelectedStation(Station.Stations.First(s =>
                    s.StationId == LocationViewModel.stationLocations[selectedIndex].StationId));
            }

            await Navigation.PopModalAsync();
        }
    }
}
