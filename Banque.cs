using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompteBancaire
{
    public class Banque
    {
        private string nom, ville;
        public Dictionary<string, Compte> mesComptes { get; }
        public double tauxDInteret { get; private set; }

        public Banque(string _nom, string _ville, double _tauxDInteret)
        {
            mesComptes = new Dictionary<string, Compte>();
            nom = _nom;
            ville = _ville;
            tauxDInteret = _tauxDInteret;
        }

        public void AjouteCompte(string signature, Compte newCompte)
        {
            mesComptes.Add(signature, newCompte);
        }

        public void AjouteCompte(int _num, string _nom, int _solde, int _decouvertAutorise)
        {
            Compte newCompte = new Compte(_num, _nom, _solde, _decouvertAutorise);
            mesComptes.Add(_num.ToString(), newCompte);
        }
        public string GetString()
        {
            string s = null;
            foreach(Compte c in mesComptes.Values)          
            {
                s += c.GetString();
                s += Environment.NewLine;
            }
            return s;
        }
        public string GetVille()
        {
            return ville;
        }
        public string GetNom()
        {
            return nom;
        }
        public int CompteSoldeMax()
        {
            int index = 0;
            string sIndex = null;
            double temp=0;
            foreach(Compte c in mesComptes.Values)
            {
                if(c.GetSolde() > temp)
                {
                    temp = c.GetSolde();
                    index = c.GetNumero();
                    sIndex = index.ToString();
                }
            }            
            return mesComptes[sIndex].GetNumero();
        }
        public string GetCompte(int numero)
        {
            string s = null;
            double temp;
            foreach(Compte c in mesComptes.Values)
            {
                temp = c.GetNumero();
                if(temp == numero)
                {
                    s = c.GetString();
                }
            }
            return s;
        }

        public void Transferer(int _numeroCompteDepart, int _numeroDeCompteArrive, double _montant)
        {
            mesComptes[_numeroCompteDepart.ToString()].Debiter(_montant);
            mesComptes[_numeroDeCompteArrive.ToString()].Crediter(_montant);
        }
        public Compte GetCompte(string _numero)
        {
            return mesComptes[_numero];
        }

        public bool SetTauxDInteret(object _tauxDInteret)
        {
            try
            {
                tauxDInteret = (double)_tauxDInteret;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void AjoutInteret(DateTime _dateDebut,Compte c)
        {
            TimeSpan t = DateTime.Today - _dateDebut;
            int nbDeJour = (int)t.TotalDays;
            double tauxDInteretJour = tauxDInteret / 365;
            c.Crediter((nbDeJour * tauxDInteretJour));

        }
        public void ClotureCompte(Compte compte)
        {
            mesComptes.Remove(compte.GetNumero().ToString());
        }
    }
}