using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCVideoVerhuur.Models
{
    public partial class Klant
    {
        public override string ToString()
        {
            return this.Voornaam +" "+this.Naam;
        }
    }
}