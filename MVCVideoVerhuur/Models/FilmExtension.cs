using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCVideoVerhuur.Models
{
    [MetadataType(typeof(FilmProperties))]
    public partial class Film
    {
        public void VerhuurFilm()
        {
            if (this.InVoorraad > 0)
            {
                this.InVoorraad--;
                this.UitVoorraad++;
            }
            else
            {
                throw new Exception("Kan geen Film verhuren waarvan er geen in voorraad zijn");
            }
        }
    }
}