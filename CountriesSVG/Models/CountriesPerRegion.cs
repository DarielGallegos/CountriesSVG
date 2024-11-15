using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesSVG.Models
{
    public class CountriesPerRegion
    {
        public bool? Independent { get; set; } = null;
        public string? Status { get; set; } = null;
        public string[]? Capital { get; set; } = null;
        public TranslationCountry? Translations { get; set; } = null;
        public float[]? Latlng { get; set; } = null;
        public string? Flag { get; set; } = null;
        public int? Population { get; set; } = null;
        public double? Area { get; set; } = null;
        public FlagLinks? Flags { get; set; } = null;

    }
}
