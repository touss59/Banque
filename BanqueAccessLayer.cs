using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Banque2
{
    public static class BanqueAccessLayer
    {
       public static (string,string) SaltAndHashPassword( string password, string salt="nothing")
        {
            var sha = SHA512.Create();
            if (salt == "nothing")
            {
                salt = CreateSalt512();
            }
            var saltedPassword = password + salt;
            return (Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword))),salt);
        }

        private static string CreateSalt512()
        {
            var message = RandomString(512, false);
            return BitConverter.ToString((new SHA512Managed()).ComputeHash(Encoding.ASCII.GetBytes(message))).Replace("-", "");
        }

        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static void AddPerson( Person person)
        {
            using (Db db = new Db())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        public static Person CheckConnection(string nom, string prenom, string mdp)
        {
            using (Db db = new Db())
            {

                List<Person> persons = db.Persons.Where(p => p.Nom == nom && p.Prenom == prenom).ToList();
                if (persons == null) { return null; }
                foreach(var person in persons)
                {
                    (string,string) mdp1 = SaltAndHashPassword(mdp, person.Salt);
                    if(person.PassWord == mdp1.Item1)
                    {
                        return person;
                    }
                    
                }
                return null;

            }

        }

        public static void CreateCompte(Person person)
        {
            using(Db db = new Db())
            {
                Person person1 = db.Persons.Where(p => p.Id == person.Id).FirstOrDefault();
                person1.Compte = new Compte();
                db.SaveChanges();
            }
        }

        public static void Virement(Person person, int id, decimal somme)
        {
            using(Db db = new Db())
            {
                Compte compte = db.Comptes.Where(c => c.PersonId == person.Id).FirstOrDefault();
                Compte compte1 = db.Comptes.Where(c => c.PersonId == id).FirstOrDefault();

                compte.Solde -= somme;
                compte1.Solde += somme;

                Operation operation = new Operation(-somme,"virement");

                Operation operation2 = new Operation(somme, "virement");

                compte.Operations.Add(operation);

                compte1.Operations.Add(operation2);

                db.SaveChanges();
            }
        }
    }
}
