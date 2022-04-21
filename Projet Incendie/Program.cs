using Projet_Incendie;
using System;
using System.Globalization;
using System.IO;

namespace Projet_incendie
{
    public class Program
    {

        static void Saisie(ref int n , ref int m)    //Méthode de saisie classique 
        {
            Console.WriteLine("saisir longueur de votre forêt");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("saisir largeur de votre forêt");
            m = Convert.ToInt32(Console.ReadLine());

        }

        static Cellule[,] RemplissageAleatoire()
        { 
            int n = 0;
            int m = 0;

            Saisie(ref n, ref m);

            Cellule[,] surface = new Cellule[n, m];
            Random rand = new Random();
            
            n = surface.GetLength(0);
            m = surface.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int valeur = rand.Next(1, 7);

                    surface[i, j] = new Cellule(valeur);   // association d'une valeur aléatoire à chaque case de la matrice
                }
            }
            return surface;
        }
        

        static void Affichage(Cellule[,] surface)
        {
            int n = surface.GetLength(0);
            int m = surface.GetLength(1);
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write( surface[i,j].Type + " | ");   // On veut afficher la matrice avec le symbole d'où le .Type 
                }
                Console.WriteLine();
            }

        }

        static void InitialiseFeu(Cellule[,] surface)
        {
            int positionL = 0;
            int positionC = 0;
            Random rand = new Random();
            do
            {
                positionL = rand.Next(0, surface.GetLength(0));     // Les 2 lignes permettent de choisir une case au hasard dans notre matrice 
                positionC = rand.Next(0, surface.GetLength(1));

            } while (surface[positionL, positionC].Type == '/' || surface[positionL, positionC].Type == '+');


            surface[positionL, positionC].Etat = true;
        }

        static int NbVoisinsFeu(Cellule[,] surface, int indexligne, int indexcolonne)  
        {
             // l'erreur System.IndexOutOfRangeException se trouve vraisemblablement dans cette méthode. Un de nos tableau semble avoir une dimension incorrecte mais nous ne trouvons pas l'erreur
             // Cette erreur impacte donc parfois la suite du code (plus précisément à la ligne 152 où la méthode NbVoisinsFeu est utilisée), c'est pour cela que le lancement du programme s'arrête après le premier affichage.

             //version non circulaire

            int nbv = 0;
            int ligne = surface.GetLength(0);
            int colonne = surface.GetLength(1);

            for(int k = 0; k<= ligne -1 ; k++)
            {
                for(int h = 0; h <= colonne - 1 ; h++)
                {
                    if (surface[k, h].Etat == false)
                    {
                        continue;
                    }
                    
                    else if (surface[k, h].Etat == true)
                    {
                        indexligne = k;
                        indexcolonne = h;
                    }
                }
            }

            for (int i = indexligne - 1; i <= indexligne + 1 && i < ligne  ; i++)
            {
                  for (int j = indexcolonne - 1; j <= indexcolonne + 1 && j < colonne; j ++)
                  {

                     if (surface[i,j].Etat == true)
                     {
                        nbv++;
                     }

                     if (surface[i, j].Etat == false)
                     {
                        continue;
                     }

                  }
            }
            return nbv ;

        }

        public static void Propagation(Cellule[,] surface, int nombreTours)
        {
            int nombre = 0;

            for (nombre = 0; nombre < nombreTours; nombre++)
            {

                Console.WriteLine("Nous sommes au tour numéro : " + nombre);
                Affichage(surface);

                for (int i = 0; i < surface.GetLength(0); i++)
                {
                    for (int j = 0; j < surface.GetLength(1); j++)
                    {
                        if (surface[i, j].Degre == 2 && surface[i, j].Etat == true)
                        {
                            //Transforme le type en cendre 
                            surface[i, j] = new Cellule('-', true, 1);
                        }

                        if (surface[i, j].Degre == 1)
                        {
                            // Passe de cendre à cendres éteintes. 
                            surface[i, j] = new Cellule('.', false, 0);
                        }

                        if ((surface[i, j].Type == 'x' || surface[i, j].Type == '*' || surface[i, j].Type == '#') && NbVoisinsFeu(surface, i, j) >= 1)
                        {
                            // Prend feu et le degré diminue de 1 
                            int n = surface[i, j].Degre;
                            char type = surface[i, j].Type;

                            surface[i, j] = new Cellule(type, true , n--);
                        }

                        if (surface[i, j].Etat == true && surface[i, j].Degre > 2)
                        {
                            // Case déjà en feu à qui on enlève un degré
                            int n = surface[i, j].Degre;
                            char type = surface[i, j].Type;
                            surface[i, j] = new Cellule(type, true, n--);
                        }

                        if (surface[i, j].Type == '/' || surface[i, j].Type == '+')
                        {
                            //rien ne se passe 
                            continue;
                        }

                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Nous sommes au tour numéro : " + nombre);
            Affichage(surface);
        }
        public static void Simulation()
        {
            /*Console.WriteLine("Choisir le nombres de tour");
            int nbTours = Convert.ToInt32(Console.ReadLine());*/
            int nbTours = 0;
            
            Cellule[,] surface = LireFichier(ref nbTours, "FILE.txt");

            InitialiseFeu(surface);
            Propagation(surface, nbTours);
        }

        static void SauvegardeForet()
        {
            Cellule[,] surface = null;

            Affichage(surface);
        }


        static Cellule[,] LireFichier( ref int nbtour, string fichier)
        {

            StreamReader fichiers = new StreamReader(fichier);

            string ligne = fichiers.ReadLine();
            string[] info = ligne.Split(',');
            nbtour = int.Parse(info[0]);

            int i = int.Parse(info[1]);

            int j = int.Parse(info[2]);

            char[] div = { '|' };


            //Définition du tableau qui contiendra nos données une fois séparées de la ligne
            string[] data = new string[j];

            Cellule[,] surface = new Cellule[i, j];
            int k = 0;
            while (fichiers.Peek() > 0)
            {
                ligne = fichiers.ReadLine();

                //On la sépare en reconnaissant le caractère séparateur ( | )
                data = ligne.Split(div);

                //Création de toutes les cellules
                
                    for (int l = 0; l < j; l++)
                    {
                        surface[k, l] = new Cellule(data[l]);
    
                    }
                k++;
            }

            fichiers.Close();

            return surface;
        }


        private static void EcrireFichier(Cellule[,] surface, string newFile)
        {
            StreamWriter ecriture = new StreamWriter(newFile, false);

            //On itère dans notre matrice
            for (int i = 0; i < surface.GetLength(0); i++)
            {
                string ligne = "| ";
                for (int j = 0; j < surface.GetLength(1); j++)
                {
                    ligne += surface[i, j] + " | ";  //On crée un string par ligne de notre matrice
                }
                ecriture.WriteLine(ligne);
            }

            ecriture.Close();
        }


        public static void Main(string[] args)
        {
            //Cellule[,] foret = RemplissageAleatoire();

            Simulation(); 
        }
    }
}
