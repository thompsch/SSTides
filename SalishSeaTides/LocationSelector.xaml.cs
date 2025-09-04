using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            // Now you can use the (x, y) coordinates to trigger an action.
            // For example, display an alert or navigate.
        }
    }
}