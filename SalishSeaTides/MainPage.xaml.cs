using SalishSeaTides.Models;
using Syncfusion.Maui.Calendar;

namespace SalishSeaTides;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		TidesCalendar.SelectedDate = DateTime.Today;
		TidesCalendar.DisplayDate = DateTime.Today;
	}

	private async void OnButtonClicked(object sender, EventArgs e)
	{
		var tideViewModel = (TideViewModel)BindingContext;
		var newViewModel = new LocationViewModel(tideViewModel);
		var modalPage = new LocationSelector(newViewModel);
		await Navigation.PushModalAsync(modalPage);
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
		if (sender is not SfCalendar calendar || !calendar.SelectedDate.HasValue) return;
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = calendar.SelectedDate.Value;
	}

	private void OnTodayClicked(object? sender, EventArgs e)
	{
		TidesCalendar.SelectedDate = DateTime.Today;
		TidesCalendar.DisplayDate = DateTime.Today;
		var tideViewModel = (TideViewModel)BindingContext;
		tideViewModel.SelectedDateTime = DateTime.Today;
	}
}