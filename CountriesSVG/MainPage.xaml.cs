using System.Net.Http.Json;
using SkiaSharp;
using SkiaSharp.Extended.Svg;

namespace CountriesSVG
{
    public partial class MainPage : ContentPage
    {
        private List<Models.CountriesList> lista;
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
            //using HttpContent content = response.Content;
            //string data = await content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<Models.CountriesPerRegion>>();
                lista = new List<Models.CountriesList>();
                foreach (Models.CountriesPerRegion item in list) {
                    itemAdd = new Models.CountriesList() {
                        NombrePais = item.Translations.Spa.Official,
                        Capital = item.Capital[0],
                        UrlSVG = await getSvg(item.Flags.Svg),
                        Bandera = item.Flag,
                        Latitud = item.Latlng[0],
                        Longitud = item.Latlng[1],
                        Poblacion = item.Population,
                        Fifa = item.Fifa
                    };
                    lista.Add(itemAdd);
                    Console.WriteLine($"Bandera: {item.Flags.Svg}, Nombre Oficial: {item.Translations.Spa.Official}");
                }
            }
            countriesListView.ItemsSource = lista;
        }

        private async Task<ImageSource> getSvg(string url) {
            using var client = new HttpClient();
            var svgData = await client.GetByteArrayAsync(url);

            var svg = new SkiaSharp.Extended.Svg.SKSvg();
            //svg.Load(svgData);

            var bitmap = new SKBitmap(100, 100);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.Transparent);
            canvas.DrawPicture(svg.Picture);

            var image = SKImage.FromBitmap(bitmap);
            var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return ImageSource.FromStream(() => data.AsStream());            
        }
        private async void cboRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var region = cboRegions.SelectedItem.ToString();
            await clientHttp(client, region);
        }
    }

}
