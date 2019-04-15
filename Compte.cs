using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompteBancaire
{
    public class Compte
    {
        private double decouvertAutorise;
        private string nom;
        private int numero;
        private double solde;

        public Compte()
        {
            numero = 0;
            nom = "";
            solde = 0;
            decouvertAutorise = 0;

        }

        public Compte(int _numero, string _nom, double _solde, double _decouvertAutorise)
        {
            numero = _numero;
            nom = _nom;
            solde = _solde;
            decouvertAutorise = _decouvertAutorise;

        }

        public void Crediter(double _montant)
        {
            solde += _montant;

        }

        public bool Debiter(double _montant)
        {
            if (solde >= (_montant + decouvertAutorise))
            {
                solde -= _montant;
            }
            return solde >= (_montant + decouvertAutorise);

        }

        public bool Superieur(Compte _autreCompte)
        {
            return solde > _autreCompte.solde;
        }

        public string GetString()
        {
            string s = ("num : " + numero.ToString() + " // nom : " + nom + " // solde : " + solde + " // decouvert Max : " + decouvertAutorise);
            return s;
        }

        public bool Transferer(double _montant, Compte _compteDestinataire)
        {
            bool ok = (solde >= (_montant - decouvertAutorise));
            if (ok)
            {
                solde -= _montant;
                _compteDestinataire.solde += _montant;
            }
            return (ok);
        }

        public string GetNom()
        {
            return nom;
        }
        public double GetSolde()
        {
            return solde;
        }
        public int GetNumero()
        {
            return numero;
        }
        public double GetDecouvertAutorise()
        {
            return decouvertAutorise;
        }


    }
}