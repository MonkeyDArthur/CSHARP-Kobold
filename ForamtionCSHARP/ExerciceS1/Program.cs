using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciceS1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Exercice I - Opérations élémentaires");
            //Opérations de base
            Serie_I.ElementaryOperations.BasicOperation(3, 4, '+');
            Serie_I.ElementaryOperations.BasicOperation(6, 2, '/');
            Serie_I.ElementaryOperations.BasicOperation(3, 0, '/');
            Serie_I.ElementaryOperations.BasicOperation(6, 9, 'L');

            //Division entière
            Serie_I.ElementaryOperations.IntegerDivision(12, 4);
            Serie_I.ElementaryOperations.IntegerDivision(13, 4);
            Serie_I.ElementaryOperations.IntegerDivision(12, 0);

            //Puissance entière
            Serie_I.ElementaryOperations.Pow(3, 4);
            Serie_I.ElementaryOperations.Pow(2, 0);
            Serie_I.ElementaryOperations.Pow(6, -2);

            Console.WriteLine($"\n\nExercice II - Horloge parlante");
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(10));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(0));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(20));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(-2));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(25));

            Console.WriteLine($"\n\nExercice III - Construction d'une pyramide");
            Serie_I.Pyramid.PyramidConstruction(10, true);
            Serie_I.Pyramid.PyramidConstruction(10, false);
            Serie_I.Pyramid.PyramidConstruction(0, true);

            Console.ReadKey();
        }
    }
}
