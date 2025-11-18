using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagGame.Classes
{

    public class Country
    {
        public string? name { get; set; }
        public string? flag { get; set; }
        public string? region { get; set; }
        public bool independent { get; set; }
        public string? flagPng => $"https://flagcdn.com/w320/{flag.Split("/").Last().Replace("svg", "png")}";


        public Country(string? name, string? flag, string? region, bool independent)
        {
            this.name = name;
            this.flag = flag;
            this.region = region;
            this.independent = independent;
        }
        public Country(Country country)
        {
            this.name = country.name;
            this.flag = country.flag;
            this.region = country.region;
            this.independent = country.independent;
        }
        public Country()
        {
        }
    }
}
