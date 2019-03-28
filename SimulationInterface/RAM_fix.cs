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

namespace SimulationInterface
{
    class RAM_fix : RAM
    {
        
        public int frag;

        public RAM_fix()
        {
            capacite = 500;                          
            int nb =8;                                
            int k = 0;
            int m = 0;
            partition p;
            Random rand = new Random();
            for (int i = 0; i < nb - 1; i++)
            {
                m = rand.Next(10, capacite / nb);
                p = new partition(k, m);
                list_rep.Add(p);
                list_zone_libre.Add(i + m * 1000);
                
                k = k + m + 1;
            }
            p = new partition(k, capacite - k);
            list_rep.Add(p);
            list_zone_libre.Add(nb - 1 + (capacite - k) * 1000);

        }




        public RAM_fix(List<int> list,int cap)
        {            
            capacite = cap;                          
            int nb = list.Count;                               
            int k = 0;
            int m = 0;
            partition p;
            list_rep = new List<partition>();
            list_zone_libre = new List<int>();
            for (int i = 0; i < nb; i++)
            {
                m = list[i];
                p = new partition(k, m);
                list_rep.Add(p);
                list_zone_libre.Add(i + m * 1000);
                k = k + m ;
            }
        }

        



        public void vider_part(partition p,int tai)
        {
            int num = list_rep.IndexOf(p);
            //p.f_proc.Remove(p.f_proc[0]);
            list_rep[num].Set_vide(true);
            list_rep[num].Set_id(-1);
            list_zone_libre.Add(num + p.Get_taille() * 1000);
            this.frag = frag - (list_rep[num].Get_taille() - tai);
            int i = 0;
            for (i = 0; i < list_zone_libre.Count; i++)
            {
                list_zone_libre[i] = (list_zone_libre[i] / 1000) + ((list_zone_libre[i] % 1000) * 10000);
            }
            list_zone_libre.Sort();
            for (i = 0; i < list_zone_libre.Count; i++)
            {
                list_zone_libre[i] = (list_zone_libre[i] / 10000) + ((list_zone_libre[i] % 10000) * 1000);
            }

           
        }
        public void occ_part(partition pr,processus proc,int tai)
        {
            int num = list_rep.IndexOf(pr);
            list_rep[num].Set_vide(false);
            list_rep[num].Set_id(proc.Get_id());
           // pr.f_proc.Add(proc);
            list_zone_libre.Remove(num + pr.Get_taille() * 1000);
            this.frag = frag + (list_rep[num].Get_taille() - tai);

        }
        
        public int Rech_first(processus proc)//retourne l'indice ou doit etre inseré proc
        {
            Boolean rech = true; int indice = 0;
            while ((rech) && (indice < list_zone_libre.Count))
            {
                if ((list_zone_libre[indice] / 1000 - proc.Get_taille()) > 0)
                {
                    rech = false;

                    indice = list_zone_libre[indice] % 1000;
                }
                else { indice++; }
            }
            if (rech) { return -1; }
            else { return indice; }
        }
        public int Rech_best(processus proc)//retourne l'indice de la partition ou doit etre insere proc
        {
            int indice = 0;
            Boolean rech = true;
            List<int> liste = new List<int>();
            foreach (int j in list_zone_libre)
            {
                liste.Add(j);

            }
            liste.Sort();
            while ((rech) && (indice < liste.Count))
            {
                if ((liste[indice] / 1000 - proc.Get_taille()) > 0)
                {
                    rech = false;
                    indice = liste[indice] % 1000;
                }
                else { indice++; }
            }
            if (rech) { return -1; }
            else { return indice; }



        }
        public int Rech_worst(processus proc)
        {

            //int indice = 0;
            List<int> liste = new List<int>();
            foreach (int j in list_zone_libre)
            {
                liste.Add(j);

            }
            liste.Sort();
            liste.Reverse();
            if (liste[0] / 1000 >= proc.Get_taille()) { return liste[0] % 1000; }
            else { return -1; }

        }

        public int Rech_next(processus proc, int prec)//retourne l'indice ou doit etre inseré proc
        {
            Boolean rech = true; int indice = prec;
            while ((rech) && (indice < list_zone_libre.Count))
            {
                if ((list_zone_libre[indice] / 1000 - proc.Get_taille()) > 0)
                {
                    rech = false;
                    indice = list_zone_libre[indice] % 1000;
                }
                else { indice++; }
            }
            indice = 0;
            while ((rech) && (indice < prec))
            {
                if ((list_zone_libre[indice] / 1000 - proc.Get_taille()) > 0)
                {
                    rech = false;

                    indice = list_zone_libre[indice] % 1000;
                }
                else { indice++; }
            }
            if (rech) { return -1; }
            else { return indice; }
        }

        /*public int best_fit(processus pro)
         * 
		{

			int[] comp;
			int min = pro.Get_taille()-list_zone_libre[0].Get_taille();
			int index;
			while (true)
			{
				for (int i = 1; i < list_zone_libre.Count; i++)
				{
					if ((pro.Get_taille() - list_zone_libre[i].Get_taille() > 0) && (pro.Get_taille() - list_zone_libre[i].Get_taille() < min))
					{
						min = pro.Get_taille() - list_zone_libre[i].Get_taille();
						index = i;
					}
				}
			}
			return index;
         }
		public void Worst_fit(processus pro)
		{
          list_zone_libre.Sort()
		}
		*/
        public int Get_Capacity()
        {
            return capacite;
        }
        public int calcul_esplibre()
        {
            int res = 0, i = 0;
            while (true)
            {
                if (i == list_rep.Count) break;
                if (list_rep[i].G_vide() == true) res += list_rep[i].Get_taille();
                i++;
            }
            return res*100/ capacite;
        }
    }
}
