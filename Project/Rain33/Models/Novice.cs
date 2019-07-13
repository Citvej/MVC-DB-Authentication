using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rain33.Models
{
    public class Novice
    {
        [Key]
        public int NoviceId { get; set; }
        public DateTime datum { get; set; }
        public string avtor { get; set; }
        public string besedilo { get; set; }

        public int UporabnikiId { get; set; }
        public Uporabniki Uporabniki { get; set; }
    }
}
