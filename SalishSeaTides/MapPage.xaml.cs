using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalishSeaTides.Models;
using Syncfusion.Maui.Maps;

namespace SalishSeaTides;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
    }

    private MapViewModel _locationViewModel;

    public MapPage(MapViewModel viewModel)
    {
        _locationViewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}