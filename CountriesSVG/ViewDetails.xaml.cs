namespace CountriesSVG;

using CountriesSVG.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

public partial class ViewDetails : ContentPage
{
	private CountriesList country;
	public ViewDetails(CountriesList country)
	{
		InitializeComponent();
		BindingContext = country;

		var position = new Location(country.Latitud ?? 0, country.Longitud ?? 0);
		mapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(500)));

		var pin = new Pin
		{
			Label = country.NombrePais,
			Address = country.Capital,
			Location = position
		};
		mapa.Pins.Add(pin);
	}
}