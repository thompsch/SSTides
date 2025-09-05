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
		var tideViewModel = (TideViewModel)BindingContext;
		await Navigation.PushModalAsync(new LocationSelector(tideViewModel));
	}
}