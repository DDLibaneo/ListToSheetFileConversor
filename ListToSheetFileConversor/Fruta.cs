using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListToSheetFileConversor
{
    public class Fruta
    {
        public string Nome { get; set; }
        
        public string Cor { get; set; }

        [Display(Name = "Peso em gramas")]
        public double Peso { get; set; }
    }
}
