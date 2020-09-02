using System;
using System.Collections.Generic;
using System.Text;

namespace Banque2
{
    public class Compte
    {
        public int Id { get; set; }
        public decimal Solde { get; set; }
        public DateTime DateOuverture { get; set; }
        public int PersonId { get; set; }
        public List<Operation> Operations { get; set; }

        public Compte()
        {
            DateOuverture = DateTime.Now;
            Solde = 0;
            Operations = new List<Operation>() { };
        }
    }
}
