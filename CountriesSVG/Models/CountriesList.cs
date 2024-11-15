
namespace CountriesSVG.Models
{
    public class CountriesList
    {
        public string? NombrePais { get; set; } = null;
        public string? Capital { get; set; } = null;
        public string? UrlSVG { get; set; } = null;
        public string? Bandera { get; set; } = null;
        public float? Latitud { get; set; } = null;
        public float? Longitud { get; set; } = null;
        public int? Poblacion { get; set; } = null;
        public double? Area { get; set; } = null;
        public Currencies? Moneda { get; set; } = null;
        public List<String>? Lenguajes { get; set; } = null;
    }
}
