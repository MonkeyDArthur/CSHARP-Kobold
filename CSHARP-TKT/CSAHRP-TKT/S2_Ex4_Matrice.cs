using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Matrice
    {
        public static void AffMatrice(int[][] matrice)
        {
            if (matrice == null) { Console.WriteLine($"Il faut des tailles de vecteurs ou de matrices égales."); }
            else
            {
                for (int i = 0; i < matrice.Length; i++)
                {
                    for (int j = 0; j < matrice[i].Length; j++) 
                    { 
                        Console.Write(string.Join(", ", matrice[i][j]) + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static int[][] BuildingMatrix(int[] leftVector, int[] rightVector)
        {
            Console.WriteLine("Création de matrice avec 2 vecteurs");
            Console.WriteLine("Vecteur 1 : [" + string.Join(", ", leftVector) + "]");
            Console.WriteLine("Vecteur 2 : [" + string.Join(", ", rightVector) + "]");
            if (leftVector.Length != rightVector.Length) { return null; }
            
            int taille = leftVector.Length;
            int[][] res = new int[taille][];
            for (int i = 0; i < taille; i++)
            {
                res[i] = new int[taille];
                for (int j = 0; j < taille; j++)
                {
                    res[i][j] = leftVector[i] * rightVector[j];
                }
            }
            return res;
        }
        
        public static int[][] AddSous(int[][] leftMatrix, int[][] rightMatrix, char ope)
        {
            Console.WriteLine($"Addition ou soustraction de 2 matrices : {ope}");
            Console.WriteLine("Matrice 1 :"); AffMatrice(leftMatrix);
            Console.WriteLine("Matrice 2 :"); AffMatrice(rightMatrix);
            if (leftMatrix.Length != rightMatrix.Length) { return null; }

            int taille1 = leftMatrix.Length;
            int[][] res = new int[taille1][];
            for (int i = 0; i < taille1; i++)
            {
                if (leftMatrix[i].Length != rightMatrix[i].Length) { return null; }

                int taille2 = leftMatrix[i].Length;
                res[i] = new int[taille2];
                for (int j = 0; j < taille2; j++)
                {
                    if (ope == '+') { res[i][j] = leftMatrix[i][j] + rightMatrix[i][j]; }
                    else { res[i][j] = leftMatrix[i][j] - rightMatrix[i][j]; }
                }
            }
            return res;
        }

        public static int[][] Multiplication(int[][] leftMatrix, int[][] rightMatrix)
        {
            Console.WriteLine($"Multiplication de 2 matrices :");
            Console.WriteLine("Matrice 1 :"); AffMatrice(leftMatrix);
            Console.WriteLine("Matrice 2 :"); AffMatrice(rightMatrix);
            if (leftMatrix[0].Length != rightMatrix.Length) { return null; }

            int ligne = leftMatrix.Length;
            int colonneL = leftMatrix[0].Length;
            int colonneR = rightMatrix[0].Length;

            int[][] res = new int[ligne][];
            for (int i = 0; i < ligne; i++) { res[i] = new int[colonneR]; }
            for (int i = 0; i < ligne; i++)
            {
                for (int j = 0; j < colonneR; j++)
                {
                    res[i][j] = 0;
                    for (int k = 0; k < colonneL; k++)
                    {
                        res[i][j] += leftMatrix[i][k] * rightMatrix[k][j];
                    }
                }
            }
            return res;
        }
    }
}
