using System;
using System.Linq;
using static Banque2.BanqueAccessLayer;

namespace Banque2
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer;
            Person user;
            do
            {
                Console.Write("Bonjour si vous voulez creer un compte tapez 1 sinon tapez 2");
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2");

            int createConnection = Convert.ToInt32(answer);
            if (createConnection == 1)
            {
                DateTime dateTime;
                Console.Write("Quelle est votre nom ?");
                string nom = Console.ReadLine();
                Console.Write("Quelle est votre prenom ?");
                string prenom = Console.ReadLine();
                Console.Write("Quelle est votre date de naissance ?");
                dateTime = DateTime.Parse(Console.ReadLine()); 
                Console.Write("Quelle est votre pays ?");
                string pays = Console.ReadLine();
                Console.Write("Votre ville?");
                string ville = Console.ReadLine();
                Console.Write("Saisir mot de passe");
                string mdp = Console.ReadLine();
                (string,string) SaltedPassword = BanqueAccessLayer.SaltAndHashPassword(mdp);

                user = new Person() { Nom = nom, Prenom=prenom, Naissance=dateTime, Pays=pays, Ville=ville, PassWord = SaltedPassword.Item1, Salt=SaltedPassword.Item2 };
                AddPerson(user);
            }
            else
            {
                Console.Write("Quelle est votre nom ?");
                string nom = Console.ReadLine();
                Console.Write("Quelle est votre prenom ?");
                string prenom = Console.ReadLine();
                Console.Write("password: ");
                string password = null;
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }
                user = CheckConnection(nom, prenom, password);

                if (user==null) { Console.WriteLine("\nVotre mot de passe est erroné"); }
                else { Console.WriteLine("\nBon mot de passe"); }
            
            }
            string answer2;
            do
            {
                Console.WriteLine("Creer un compte tapez: 1\n Réaliser des retraits virements...tapez: 2\n Quitter l'application tapez 3");
                answer2 = Console.ReadLine();
            } while (answer2 != "1" && answer2 != "2" && answer2!="3");
            if (answer2 == "1")
            {
                CreateCompte(user);
            }
            else if (answer2 == "2")
            {
                Console.WriteLine("Si vous voulez réaliser un virement Tapez 1");
                string answer3 = Console.ReadLine();
                if (answer3 == "1")
                {
                    Console.WriteLine("Saisir l'identifiant de la personne qui va recevoir ce virement :");
                    int personVirement = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Saisir la somme de ce virement  :");
                    decimal somme = Convert.ToInt64(Console.ReadLine());
                    Virement(user, personVirement, somme);

                }
            }

        }
    }
}
