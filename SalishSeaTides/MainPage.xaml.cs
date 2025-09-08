using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Maui.Controls;
using SalishSeaTides.Models;

namespace SalishSeaTides;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
	private async void OnButtonClicked(object sender, EventArgs e)
	{
		var _tideViewModel = (TideViewModel)BindingContext;
		await Navigation.PushModalAsync(new LocationSelector(_tideViewModel));
	}

	private void SwipeGestureRecognizer_OnSwipedLeft(object? sender, SwipedEventArgs e)
	{
		//get next tides
		var _tideViewModel = (TideViewModel)BindingContext;
		_tideViewModel.SelectedDateTime = _tideViewModel.SelectedDateTime.AddDays(2);
	}
	private void SwipeGestureRecognizer_OnSwipedRight(object? sender, SwipedEventArgs e)
	{
		//get previous tides
		var _tideViewModel = (TideViewModel)BindingContext;
		_tideViewModel.SelectedDateTime = _tideViewModel.SelectedDateTime.AddDays(-2);
	}
}