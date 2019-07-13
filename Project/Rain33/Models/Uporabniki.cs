using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rain33.Models
{
    public class Uporabniki
    {
        [Key]
        public int UporabnikiId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool Roles { get; set; }
        public string salt { get; set; }

        public List<Novice> Novice { get; set; }
    }
}
