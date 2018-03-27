using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCVideoVerhuur.Models
{
    public class AfrekenenViewModel
    {
        public Klant Klant { get; set; }
        [DisplayFormat(DataFormatString ="{0:€ #,##0.00}")]
        public decimal Totaal { get; set; }
        public List<Verhuur> Verhuringen { get; set; }
    }
}