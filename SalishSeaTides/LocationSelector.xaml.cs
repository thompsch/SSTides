using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalishSeaTides.Models;


namespace SalishSeaTides;

public partial class LocationSelector : ContentPage
{
    public LocationSelector()
    {
        InitializeComponent();

    }

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        Point? position = e.GetPosition((Image)sender);
        if (position.HasValue)
        {
            double x = position.Value.X;
            double y = position.Value.Y;
            //var stationId = await FindNearestStation(x, y);
            //TideViewModel.SelectedStation =
            //    Station.Stations.First(s => s.StationId == stationId);
            // Now you can use the (x, y) coordinates to trigger an action.
            // For example, display an alert or navigate.
        }

        var note = @"
        THESE ARE WRONG - NEED TO RECALCULATE

        Sandy Point: 268.71732954545456, 326.88516512784093
        Port Townsend: 184.3543146306818, 299.98264382102275
        Holmes Harbor: 184.3543146306818, 299.98264382102275
        Coupeville, Penn Cove: 184.3543146306818, 299.98264382102275
        Bush Point: 184.3543146306818, 299.98264382102275
        Anacortes: 184.3543146306818, 299.98264382102275
        Friday Harbor: 184.3543146306818, 299.98264382102275
        Rosario, Orcas Island: 184.3543146306818, 299.98264382102275
        Waldron Island: 184.3543146306818, 269.299.98264382102275
        Lopez Island: 184.3543146306818, 299.98264382102275
        Hanbury Point: 94.17853338068181, 138.87912819602272 
       
        ";



    }

    private async Task<int> FindNearestStation(double x, double y)
    {
        var stationLocations = new Dictionary<int, (double X, double Y)>                
        {
            { 9447856, (268.71732954545456, 326.88516512784093) }, // Sandy Point
            { 9444900, (184.3543146306818, 299.98264382102275) }, // Port Townsend
            { 9447854, (184.3543146306818, 299.98264382102275) }, // Bush Point
            { 9447855, (184.3543146306818, 299.98264382102275) }, // Holmes Harbor
            { 9447929, (184.3543146306818, 299.98264382102275) }, // Coupeville, Penn Cove
            { 9448794, (184.3543146306818, 299.98264382102275) }, // Anacortes
            { 9449880, (184.3543146306818, 299.98264382102275) }, // Friday Harbor
            { 9449771, (184.3543146306818, 299.98264382102275) }, // Rosario, Orcas Island
            { 9449982, (184.3543146306818, 299.98264382102275) }, // Lopez Island
            { 9449828, (94.17853338068181, 138.87912819602272) }   // Hanbury Point
        };

        return -1; // Return the StationId of the nearest station.
    }

}

