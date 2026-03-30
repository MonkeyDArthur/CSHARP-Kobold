using System;

namespace CARTE
{
    public class Carte
    {
        private string _numero;
        private uint _plafond;
        
        public Carte(string numero, uint plafond)
        {
            _numero = numero;
            _plafond = plafond;
        }
        public string Numero { get => _numero; set => _numero = value; }
        public uint Palfond { get => _plafond; set => _plafond = value; }
        public void AfficherCarte() { Console.WriteLine($"Carte : {_numero}\tPlafond : {_plafond}"); }    
    }
}
