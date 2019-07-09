using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace SimulationInterface
{
    class processus : IEquatable<processus>, IComparable<processus>
    {
        private int id;
        public string name;
        private int bas;
        private int taille;
        private int temp_ex;      //le temps d execution de la processus en seconde 
        private int etat; // possede trois etat active(1) bloquee(-1) et en attente(0) 
        private int num_part;
        public Color clr;
       

        public processus(string nom, int ID, int taille, int temps)
        {
            this.name=nom;
            this.taille = taille;
            this.temp_ex = temps;
            this.bas = temps;
            this.id = ID;
            Random r = new Random();
            clr = new Color();
            clr.R = (byte)r.Next(45, 255);
            clr.G = (byte)r.Next(45, 256);
            clr.B = (byte)r.Next(40, 256);
        }

        // definez les getter de la classe 
        public int Get_taille()
        {
            return taille;
        }
        public int Get_id()
        {
            return id;
        }
        public int Get_temps()
        {
            return temp_ex;
        }
        public int Get_bas()
        {
            return bas;
        }
        public int Get_etat()
        {
            return etat;
        }
        public int Get_part()
        {
            return num_part;
        }
        
        // definez des setter 
        public void Set_taille(int taille)
        {
            this.taille = taille;
        }
        public void Set_time(int T)
        {
            temp_ex = T;
        }
        public void Set_bas(int T)
        {
            bas = T;
        }
        public void Set_etat(int etat)
        {
            this.etat = etat;
        }
        public void Set_part(int num_part)
        {
            this.num_part = num_part;
        }

        //redifinition des equals, compareTO ET equals
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                processus p = (processus)obj;
                return (id == p.id);
            }
        }
        public int CompareTo(processus comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.taille.CompareTo(comparePart.taille);
        }
        public bool Equals(processus other)
        {
            if (other == null) return false;
            return (this.id.Equals(other.id));
        }
    }
}
