using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banque2
{
    public class Person
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime Naissance { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string PassWord { get; set; }
        public string Salt { get; set; }
        public Compte Compte { get; set; }

    }
}
