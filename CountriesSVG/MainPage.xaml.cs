using System.Net.Http.Json;
using Android.Content;
using Android.Webkit;
using CountriesSVG.Models;
using SkiaSharp;

namespace CountriesSVG
{
    public partial class MainPage : ContentPage
    {
        private List<Models.CountriesList> lista;
        private List<Models.CountriesList> lista_filter;
        private Models.CountriesList itemAdd;
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("https://restcountries.com"),
        };
        public MainPage()
        {
            InitializeComponent();
            lista = new List<Models.CountriesList>();
            BindingContext = this;
        }

        private async Task clientHttp(HttpClient c, string? region) {
            using HttpResponseMessage response = await c.GetAsync($"/v3.1/region/{region}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<Models.CountriesPerRegion>>();
                lista = new List<Models.CountriesList>();
                foreach (Models.CountriesPerRegion? item in list) {
                    if (item != null) {
                        var key = item.Currencies.Keys.ToArray()[0];
                        List<string>? lenguages = new List<string>();
                        for (int i = 0; i < item?.Languages.Count; i++) {
                            lenguages.Add(item.Languages.Values.ToArray()[i]);
                        }

                        itemAdd = new Models.CountriesList()
                        {
                            NombrePais = item?.Translations?.Spa?.Official,
                            Capital = item?.Capital?[0],
                            UrlSVG = item?.Flags?.Svg,
                            Bandera = item?.Flag,
                            Latitud = item?.Latlng?[0],
                            Longitud = item?.Latlng?[1],
                            Poblacion = item?.Population,
                            Area = item?.Area,
                            Moneda = new Models.Currencies() { 
                                Name = item?.Currencies[key]?.Name,
                                Symbol = item?.Currencies[key]?.Symbol
                            },
                            Lenguajes = lenguages
                        };
                    }
                    if (item != null) { 
                        lista.Add(itemAdd);
                    }
                }
            }
            lista_filter = lista;
            countriesListView.ItemsSource = lista_filter;
        }
    
        private async void cboRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var region = cboRegions.SelectedItem.ToString();
            await clientHttp(client, region);
        }

        private void countriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CountriesList item = e.SelectedItem as CountriesList;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textFilter = e.NewTextValue?.ToLower() ?? string.Empty;
            lista_filter = lista.Where(x => x.NombrePais?.ToLower().Contains(textFilter) ?? false).ToList();
            countriesListView.ItemsSource = lista_filter;
        }
    }

}
