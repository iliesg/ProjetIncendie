using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Incendie
{
    public class Cellule
    {
        private char type;
        private bool etat;
        private int degre;
        private string[] data;



        public Cellule(char type, bool etat, int degre)
        {
            this.type = type;
            this.etat = etat;
            this.degre = degre;


            if (this.etat == true)
            {
                this.degre = degre - 1;
            }

            if(this.etat == true && this.degre == 1)
            {
                this.degre = 0;
            }
           
            if (this.degre == 0)
            {
                this.type = '.';  //cendres éteintes
            }
        }

        public char Type
        {
            get { return type; }
            set { }
        }
        public bool Etat
        {
            get { return etat; }
            set { etat = value; } 
        }
        public int Degre
        {
            get { return degre; }
            set { degre = value; }
        }
        public string [] Data
        {
            get { return data; }
            set { data = value; }
        }
        public Cellule (int n) // on associe à chaque valeur (qu'on obtient dans RemplissageAleatoire()) son type, son degre et son etat. 
        {
            if ( n == 1 ) //herbe
            {
                this.type = 'x';
                this.degre = 8;
                this.etat = false;
            }
            
            else if (n == 2) //arbre
            {
                this.type = '*';
                this.degre = 8;
                this.etat = false;
            }

            else if (n == 3) //terrain
            {
                this.type = '+';
                this.degre = 0;
                this.etat = false;
            }

            else if (n == 4) //feuille
            {
                this.type = ' ';
                this.degre = 4;
                this.etat = false;
            }

            else if (n == 5) //eau
            {
                this.type = '/';
                this.degre = 0;
                this.etat = false;
            }

            else if (n == 6) //rocher 
            {
                this.type = '#';
                this.degre = 50;
                this.etat = false;
            }

        }

        public Cellule(string data)
        {
            for (int i = 0; i <= data.Length; i++)
            {
                if (data == "x") //herbe
                {
                    this.type = 'x';
                    this.degre = 8;
                    this.etat = false;
                }

                else if (data == "*") //arbre
                {
                    this.type = '*';
                    this.degre = 8;
                    this.etat = false;
                }

                else if (data == "+") //terrain
                {
                    this.type = '+';
                    this.degre = 0;
                    this.etat = false;
                }

                else if (data == " ") //feuille
                {
                    this.type = ' ';
                    this.degre = 4;
                    this.etat = false;
                }

                else if (data == "/") //eau
                {
                    this.type = '/';
                    this.degre = 0;
                    this.etat = false;
                }

                else if (data == "#") //rocher 
                {
                    this.type = '#';
                    this.degre = 50;
                    this.etat = false;
                }
            }
        }

    }
}
