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
            string message;
            if (heure >= 0 & heure < 6)         { message = "Merveilleuse nuit !"; }
            else if (heure >= 6 & heure < 12)   { message = "Bonne matinée !"; }
            else if (heure >= 12 & heure < 13)  { message = "Bon appétit !"; }
            else if (heure >= 13 & heure <= 18) { message = "Profitez de votre après-midi !"; }
            else if (heure > 18 & heure < 24)   { message = "Passez une bonne soirée !"; }
            else                                { message = "HEURE INVALIDE"; }
            return $"Il est {heure}H, {message}";
        }
    }
}
