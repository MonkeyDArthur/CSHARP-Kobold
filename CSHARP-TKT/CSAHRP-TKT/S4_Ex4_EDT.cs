using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//Q1 :  Début N inversion d'élément
//      Milieu N/2 inversion d'élément
//      Fin 0 inversion d'élément
//Q2 :  N comparaisons pour non trié
//      N/4-1 comparaisons pour trié



namespace Serie_IV
{
    public class BusinessSchedule
    {
        private SortedDictionary<DateTime, TimeSpan> reunion;
        private DateTime debutDate;
        private DateTime finDate;
        public BusinessSchedule()
        {
            reunion = new SortedDictionary<DateTime, TimeSpan>();
            debutDate = new DateTime(2020, 1, 1);
            finDate = new DateTime(2030, 12, 31);
        }
        public bool IsEmpty() { return reunion.Count == 0; }
        public void SetRangeOfDates(DateTime debut, DateTime fin)
        {
            if (!IsEmpty()) throw new Exception("Planning non vide");
            if (debut >= fin) throw new Exception("Intervalle invalide");
            debutDate = debut;
            finDate = fin;
        }
        public void DisplayMeetings()
        {
            int numReunion = 1;
            Console.WriteLine($"Emploi du temps : {debutDate:dd/MM/yyyy HH:mm:ss} - {finDate:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"-----------------------------------------------------------");
            if (IsEmpty()) 
            { 
                Console.WriteLine("Pas de réunions programmées");
                Console.WriteLine($"-----------------------------------------------------------");
                return;
            }
            foreach (var item in reunion)
            {
                DateTime start = item.Key; DateTime end = start + item.Value;
                Console.WriteLine($"Réunion {numReunion}       : {start:dd/MM/yyyy HH:mm:ss} - {end:dd/MM/yyyy HH:mm:ss}");
                numReunion++;
            }
            Console.WriteLine($"-----------------------------------------------------------");
        }
        public KeyValuePair<DateTime?, DateTime?> closestElements(DateTime debutReunion)
        {
            DateTime? avant = null;
            DateTime? apres = null;

            foreach (var key in reunion.Keys)
            {
                if (key <= debutReunion) avant = key;
                else { apres = key; break; }
            }
            return new KeyValuePair<DateTime?, DateTime?>(avant, apres);
        }
        public bool AddBusinessMeeting(DateTime date, TimeSpan duree)
        {
            DateTime finReunion = date + duree;
            if (date < debutDate || finReunion > finDate) return false;
            var closest = closestElements(date);

            if (closest.Key != null)
            {
                DateTime prevStart = closest.Key.Value;
                DateTime prevEnd = prevStart + reunion[prevStart];
                if (prevEnd > date) return false;
            }

            if (closest.Value != null)
            {
                DateTime nextStart = closest.Value.Value;
                if (finReunion > nextStart) return false;
            }

            reunion.Add(date, duree);
            return true;
        }
        public bool DeleteBusinessMeeting(DateTime date, TimeSpan duree)
        {
            if (reunion.ContainsKey(date) && reunion[date] == duree)
            {
                reunion.Remove(date);
                return true;
            }
            return false;
        }
        public int ClearMeetingPeriod(DateTime debut, DateTime fin)
        {
            if (debut >= fin) throw new Exception("Intervalle invalide");
            if (debut < debutDate || fin > finDate) throw new Exception("Hors planning");

            List<DateTime> aEffacer = new List<DateTime>();
            foreach (var reu in reunion)
            {
                DateTime start = reu.Key;
                DateTime finish = start + reu.Value;
                if (start < fin && finish > debut) { aEffacer.Add(start); }
            }
            foreach (var suppr in aEffacer) reunion.Remove(suppr);
            return aEffacer.Count;
        }
    }
}