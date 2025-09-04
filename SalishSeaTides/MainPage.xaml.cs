using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.Serialization;
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
		await Navigation.PushModalAsync(new LocationSelector());
	}
}