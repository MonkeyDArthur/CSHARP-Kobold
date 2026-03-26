using Serie_II;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using System.Xml.Linq;

//Q1 : Dictionnaire string string car association entre clé unique numero et valeur non unique contact

namespace Serie_IV
{
    public class PhoneBook
    {
        private Dictionary<string, string> annuaire = new Dictionary<string, string>();
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^0[1-9][0-9]{8}$");
        }
        public bool ContainsPhoneContact(string phoneNumber)
        {
            return annuaire.ContainsKey(phoneNumber);
        }
        public void PhoneContact(string phoneNumber)
        {
            foreach (var paire in annuaire)
            {
                if (paire.Key == phoneNumber)
                {
                    Console.WriteLine($"Le numéro {paire.Key} est détenu par {paire.Value}."); break;
                }
            }
        }
        public bool AddPhoneNumber(string phoneNumber, string name)
        {
            if ( ContainsPhoneContact(phoneNumber) == false)
            {
                annuaire[phoneNumber] = name;
                return true;
            }
            return false;
        }
        public bool DeletePhoneNumber(string phoneNumber)
        {
            if (ContainsPhoneContact(phoneNumber) == true)
            {
                annuaire.Remove(phoneNumber);
                return true;
            }
            return false;
        }
        public void DisplayPhoneBook()
        {
            Console.WriteLine("Annuaire Téléphonique");
            if ( annuaire.Count == 0 ) { Console.WriteLine("Pas de numéros téléphoniques."); }
            else
            {
                foreach (var paire in annuaire)
                {
                    Console.WriteLine($"{paire.Key} : {paire.Value}");
                }
            }
        }
    }
}