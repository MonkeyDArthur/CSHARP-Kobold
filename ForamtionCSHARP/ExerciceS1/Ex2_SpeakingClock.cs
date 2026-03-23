using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class SpeakingClock
    {
        public static string GoodDay(int heure)
        {
            string rep;
            if (heure >= 0 & heure < 6)
            {
                rep = "Merveilleuse nuit!";
            }
            else if (heure >= 6 & heure < 12)
            {
                rep = "Bonne matinée !";
            }
            else if (heure >= 12 & heure < 13)
            {
                rep = "Bon appétit !";
            }
            else if (heure >= 13 & heure <= 18)
            {
                rep = "Profitez de votre après-midi !";
            }
            else if (heure > 18 & heure < 24)
            {
                rep = "Passez une bonne soirée !";
            }
            else
            {
                rep = "HEURE INVALIDE";
            }
            return rep;
        }
    }
}
