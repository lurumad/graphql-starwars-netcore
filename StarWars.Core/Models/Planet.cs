using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Core.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Human> Humans { get; set; }
    }
}
