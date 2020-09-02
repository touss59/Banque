using System;
using System.Collections.Generic;
using System.Text;

namespace Banque2
{
   public class Operation
    {
        public int Id { get; set; }
        public decimal Montant { get; set; }

        public DateTime DateOperation { get; set; }

        public string Libelle { get; set; }

        public int CompteId { get; set; }


        public Operation(decimal montant, string libelle)
        {
            Montant = montant;
            DateOperation = DateTime.Now;
            Libelle = libelle;
        }

    }
}
