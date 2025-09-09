using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Maui.Controls;
using SalishSeaTides.Models;
using Syncfusion.Maui.Calendar;

namespace SalishSeaTides;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnButtonClicked(object sender, EventArgs e)
	{
		var tideViewModel = (TideViewModel)BindingContext;
		await Navigation.PushModalAsync(new LocationSelector(tideViewModel));
	}

	private void SwipeGestureRecognizer_OnSwipedLeft(object? sender, SwipedEventArgs e)
	{
		//get next tides
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = tideViewModel.SelectedDateTime.AddDays(2);
	}

	private void SwipeGestureRecognizer_OnSwipedRight(object? sender, SwipedEventArgs e)
	{
		//get previous tides
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = tideViewModel.SelectedDateTime.AddDays(-2);
	}

	private void SfCalendar_OnSelectionChanged(object? sender, CalendarSelectionChangedEventArgs e)
	{
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = ((SfCalendar)sender).SelectedDate.Value.AddDays(1);
	}

	private void SfCalendar_OnActionButtonClicked(object? sender, CalendarSubmittedEventArgs e)
	{
		//Not firing.
		/*              if (e.CalendarAction.Today)
		                {
		                        // Logic to navigate to today's date
		                        // For example, you could set the SelectedDate property:
		                        Calendar.SelectedDate = DateTime.Now;
		                }
		        }*/
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = DateTime.Now;
	}
}