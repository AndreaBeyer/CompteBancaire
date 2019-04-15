using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaire
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime d;
            Banque b = null;
            double solde = 0, decouvertMax = 0, tauxDInteret = 0, montant = 0;
            bool valide = false, recommence = true, choixValide = true;
            int numeroDeCompte = 0;
            string nomDuCompte = null, choix = null, tauxDInteretString = null, tempCompte = null, tempMontant = null, tempCompte1 = null;

            Console.WriteLine("Outil de gestion de banque");

            Console.WriteLine("Quel nom porte la banque?");
            string nomBanque = Console.ReadLine();

            Console.WriteLine("Dans quelle ville est elle situee");
            string villeBanque = Console.ReadLine();

            do
            {
                Console.WriteLine("Quel taux d'interet souhaitez vous lui appliquer?");
                tauxDInteretString = Console.ReadLine();

            } while (!double.TryParse(tauxDInteretString, out tauxDInteret)||tauxDInteret < 0);


            b = new Banque(nomBanque, villeBanque, tauxDInteret);

            Console.WriteLine("Bienvenue dans la banque {0} situee dans la ville de {1}", nomBanque, villeBanque);
            Console.WriteLine();

            do
            {
                do
                {
                    Console.WriteLine("Quel operation souhaitez vous effectuer\n1- ajout de client\n2- recherche de client\n3- Crediter un compte\n4- Debiter un compte\n5- Effectuer un transfert entre 2 comptes\n6- Comparer deux comptes\n7- Afficher la liste des comptes\n8- appliquer taux d'interet\n9- Cloturer un compte\n0- Quitter");
                    choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":

                            Console.WriteLine(Environment.NewLine + "Outil de creation de compte");

                            do
                            {
                                Console.WriteLine("Quel numero souhaitez vous attribuer?");
                                try
                                {
                                    numeroDeCompte = int.Parse(Console.ReadLine());
                                    valide = true;
                                }
                                catch
                                {
                                    valide = false;
                                }

                            } while (!valide);

                            Console.WriteLine("Quel nom souhaitez vous attribuer?");
                            nomDuCompte = Console.ReadLine();

                            do
                            {
                                Console.WriteLine("Quelle somme souhaitez vous attribuer?");
                                try
                                {
                                    solde = double.Parse(Console.ReadLine());
                                    valide = true;
                                }
                                catch
                                {
                                    valide = false;
                                }

                            } while (!valide);

                            do
                            {
                                Console.WriteLine("Quelle autorisation de decouvert souhaitez vous lui attribuer?");

                                try
                                {
                                    decouvertMax = double.Parse(Console.ReadLine());
                                    valide = true;
                                }
                                catch
                                {
                                    valide = false;
                                }

                            } while (!valide);

                            try
                            {
                                Compte newCompte = new Compte(numeroDeCompte, nomDuCompte, solde, decouvertMax);
                                b.AjouteCompte(numeroDeCompte.ToString(), newCompte);
                                Console.WriteLine("le compte a bien ete creer");
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("l'ajout a echoue : le numero de compte est deja utiliser");
                            }
                            catch
                            {
                                Console.WriteLine("l'ajout a echoue : erreur inconnue");
                            }

                            Console.WriteLine("recapitulatif :");
                            Console.WriteLine(b.GetString());

                            break;

                        case "2":

                            Console.WriteLine(Environment.NewLine + "outil d'affichage de Client");
                            Console.WriteLine(b.GetString());
                            string tempS = null;
                            do
                            {
                                Console.WriteLine("Entrez le numero du client");
                                tempS = Console.ReadLine();

                            } while (!int.TryParse(tempS,out numeroDeCompte));

                            Console.WriteLine(b.GetCompte(numeroDeCompte));

                            if (!b.mesComptes.ContainsKey(numeroDeCompte.ToString()))
                            {
                                Console.WriteLine("le client n'existe pas");
                            }

                            break;

                        case "3":

                            Console.WriteLine("Outil de credit de compte");

                            Console.WriteLine(b.GetString());

                            Console.WriteLine("Quel compte souhaitez vous crediter?");
                            tempCompte = Console.ReadLine();

                            if ((!b.mesComptes.ContainsKey(tempCompte)))
                            {
                                Console.WriteLine("le compte n'existe pas");
                                break;
                            }

                            do
                            {
                                Console.WriteLine("Quelle somme voulez vous crediter?");
                                tempMontant = Console.ReadLine();

                            } while (!double.TryParse(tempMontant, out montant));

                            b.mesComptes[tempCompte].Crediter(montant);
                            Console.WriteLine("le transfert a reussi");

                            break;

                        case "4":

                            Console.WriteLine(b.GetString());

                            Console.WriteLine("Outil de debit de compte");

                            Console.WriteLine("Quel compte souhaitez vous debiter?");
                            tempCompte = Console.ReadLine();

                            if ((!b.mesComptes.ContainsKey(tempCompte)))
                            {
                                Console.WriteLine("le compte n'existe pas");
                            }

                            do
                            {
                                Console.WriteLine("Quelle somme voulez vous debiter?");
                                tempMontant = Console.ReadLine();

                            } while (!double.TryParse(tempMontant, out montant));

                            if (b.mesComptes[tempCompte].Debiter(montant))
                            {
                                Console.WriteLine("Le transfert a reussi");
                            }
                            else
                            {
                                Console.WriteLine("le transfert a echouer");
                            }
                            break;

                        case "5":

                            Console.WriteLine(b.GetString());

                            Console.WriteLine("Outil de transfert entre comptes");

                            Console.WriteLine("Quel compte souhaitez vous debiter");
                            tempCompte = Console.ReadLine();

                            if (!b.mesComptes.ContainsKey(tempCompte))
                            {
                                Console.WriteLine("le compte n'existe pas");
                                break;
                            }



                            do
                            {
                                Console.WriteLine("Quelle somme voulez vous transferer?");
                                tempMontant = Console.ReadLine();

                            } while (!double.TryParse(tempMontant, out montant));

                            do
                            {
                                Console.WriteLine("Quel compte souhaitez vous crediter");
                                tempCompte1 = Console.ReadLine();

                                if ((!b.mesComptes.ContainsKey(tempCompte)))
                                {
                                    Console.WriteLine("le compte n'existe pas");
                                }

                            } while (!b.mesComptes.ContainsKey(tempCompte1));

                            if (b.mesComptes[tempCompte].Transferer(montant, b.mesComptes[tempCompte1]))
                            {
                                Console.WriteLine("Le transfert a reussi");
                            }
                            else
                            {
                                Console.WriteLine("le transfert a echouer");
                            }
                            break;

                        case "6":

                            Console.WriteLine("Outil de comparaison entre comptes");
                            Console.WriteLine(b.GetString());

                      
                            Console.WriteLine("Veuillez entrer un premier compte");
                            tempCompte = Console.ReadLine();

                            if ((!b.mesComptes.ContainsKey(tempCompte)))
                            {
                                Console.WriteLine("le compte n'existe pas");
                                break;
                            }


                            Console.WriteLine("Veuillez entrer un second compte");
                            tempCompte1 = Console.ReadLine();

                            if ((!b.mesComptes.ContainsKey(tempCompte)))
                            {
                                Console.WriteLine("le compte n'existe pas");
                                break;
                            }

                            if (b.mesComptes[tempCompte].Superieur(b.mesComptes[tempCompte1]))
                            {
                                Console.WriteLine("le compte numero {0} est superieur", tempCompte);
                            }
                            else
                            {
                                Console.WriteLine("le compte numero {0} est superieur", tempCompte1);
                            }
                            break;

                        case "7":
                            
                            Console.WriteLine(b.GetString());
                            if(b.mesComptes.Count() == 0)
                            {
                                Console.WriteLine("la liste des comptes est vide");
                                Console.WriteLine();
                            }

                            break;

                        case "8":

                            Console.WriteLine("Outil d'ajout d'interet");
                            Console.WriteLine(b.GetString());

                            Console.WriteLine("Outil d'ajout d'interet");
                            Console.WriteLine("A quel compte souhaitez vous appliquer l'interet");
                            tempCompte = Console.ReadLine();

                            do
                            {
                                Console.WriteLine("A compter de quel date?");
                                tempMontant = Console.ReadLine();

                            } while (!DateTime.TryParse(tempMontant, out d));

                            try
                            {
                                b.AjoutInteret(d, b.mesComptes[tempCompte]);
                                Console.WriteLine("l'ajout a reussi");
                            }
                            catch
                            {
                                Console.WriteLine("le compte n'existe pas");
                            }
                            
                            break;

                        case "9":

                            Console.WriteLine("Outil de cloture de compte");
                            Console.WriteLine(b.GetString());

                            Console.WriteLine("Quel compte souhaitez vous cloturer?");
                            tempCompte = Console.ReadLine();

                            try
                            {
                                b.ClotureCompte(b.mesComptes[tempCompte]);
                                Console.WriteLine("la cloture a reussie");
                            }
                            catch
                            {
                                Console.WriteLine("le compte n'existe pas");
                            }

                            break;

                        case "0":

                            recommence = false;

                            break;

                        default:

                            choixValide = false;

                            break;

                    }

                } while (!choixValide);


            } while (recommence);
        }
    }
}
