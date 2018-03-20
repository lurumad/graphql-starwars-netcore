using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Core.Models
{
    public class Human : Character
    {
        public Planet HomePlanet { get; set; }
    }
}
