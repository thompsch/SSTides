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
}