using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class RAM_var : RAM
    {
        
        public RAM_var(int taille)
        {
            capacite = taille;
            list_rep = new List<partition>();
            list_zone_libre = new List<int>();
            partition prt = new partition(0, taille);
            list_rep.Add(prt);
            list_zone_libre.Add(taille * 1000);

        }
        ///retourner l indice ou il faut inserer la processus selon le first fit
        public int firts_fit(int procees_taille)
        {
            int i = -1;
            partComparer dc = new partComparer();
            list_zone_libre.Sort(dc);
            if (procees_taille > capacite) return -2; //!!!!!si la taille de la processus est sup a celle de la ram elle reourne -2
            while (true)
            {
                i++;
                if (i == list_zone_libre.Count) { i = -1; break; }//!!!!! si il n ya pas elle reourne le -1
                if ((list_rep[list_zone_libre[i] % 1000].Get_taille() >= procees_taille)) { i = list_zone_libre[i] % 1000; break; }

            }
            next_fit = i;
            return i;
        }

        public int worst_fit(int procees_taille)
        {
            if (procees_taille > capacite) return -2; //!!!!!si la taille de la processus est sup a celle de la ram elle reourne -2
            list_zone_libre.Sort();//trier la list des partition libre par taille 
            if (list_zone_libre[list_zone_libre.Count - 1] / 1000 >= procees_taille)
            {
                next_fit = list_zone_libre[list_zone_libre.Count - 1] % 1000;
                return list_zone_libre[list_zone_libre.Count - 1] % 1000;
            }
            else return -1;// il n'ya de parttion qui peyt contenir la processus
        }

        public int best_fit(int procees_taille)
        {
            int i = -1;
            if (procees_taille > capacite) return -2; //!!!!!si la taille de la processus est sup a celle de la ram elle reourne -2
            list_zone_libre.Sort();
            while (true)
            {
                //!!!!! si il n ya pas elle reourne le -1
                i++;
                if (i == list_zone_libre.Count) { i = -1; break; }
                if ((list_rep[list_zone_libre[i] % 1000].Get_taille() >= procees_taille)) { i = list_zone_libre[i] % 1000; break; }

            }
            next_fit = i;
            return i;
        }

        public int Next_fit(int procees_taille)
        {
            if ((next_fit >= list_rep.Count) || (next_fit < 0)) next_fit = 0;
            try { return list_rep.IndexOf(list_rep.Find(w => ((w.Get_id() < 0) && (w.Get_taille() >= procees_taille) && (list_rep[next_fit].Get_adr() <= w.Get_adr())))); }
            catch (ArgumentNullException)
            {
                return -1;
            }

        }

        public int allocation_process(int prt, int process_id, int process_taille)
        {



            int nr = list_rep[prt].Get_taille() - process_taille;
            list_zone_libre.Remove(list_zone_libre.Find(x => x == prt + list_rep[prt].Get_taille() * 1000));
            if (nr == 0)
            {
                list_rep[prt].Set_id(process_id);
                if (process_id == -1) { list_rep[prt].Set_vide(true); }
                else { list_rep[prt].Set_vide(false); }
                return 0;
            }
            else
            {
                list_rep[prt].Set_taille(process_taille);
                list_rep[prt].Set_id(process_id);
                if (process_id == -1) { list_rep[prt].Set_vide(true); }
                else { list_rep[prt].Set_vide(false); }
                partition part = new partition(list_rep[prt].Get_adr() + process_taille, nr);
                list_rep.Insert(prt + 1, part);
                list_zone_libre.Add(part.Get_taille() * 1000 + prt + 1);
                corriger_remove(prt + 1, -1);
                return -1;
            }


        }

        public int Liberation_part(int prt)
        {
            int prec, suiv = 0, res = 0;
            
            list_rep[prt].Set_vide(true);
            list_rep[prt].Set_id(-1);

            if (prt > 0) prec = list_rep[prt - 1].Get_id();
            else prec = 0;
            if (prt < list_rep.Count) { if (prt + 1 < list_rep.Count) suiv = list_rep[prt + 1].Get_id(); }
            else suiv = 0;
            if ((prec == -1) && (suiv == -1))
            {

                list_rep[prt - 1].Set_taille(list_rep[prt - 1].Get_taille() + list_rep[prt].Get_taille() + list_rep[prt + 1].Get_taille());
                list_zone_libre[list_zone_libre.IndexOf(list_zone_libre.Find(x => x % 1000 == prt - 1))] = list_rep[prt - 1].Get_taille() * 1000 + prt - 1;
                list_rep.RemoveAt(prt);
                list_rep.RemoveAt(prt);
                afficher_ptr_zonelibre();
                list_zone_libre.Remove(list_zone_libre.Find(x => x % 1000 == prt + 1));
                afficher_ptr_zonelibre();
                corriger_remove(prt - 1, 2);
                afficher_ptr_zonelibre();
                res = 2;
            }
            else
            {
                if (prec == -1)
                {
                    list_rep[prt - 1].Set_taille(list_rep[prt - 1].Get_taille() + list_rep[prt].Get_taille());
                    list_zone_libre[list_zone_libre.IndexOf(list_zone_libre.Find(x => x % 1000 == prt - 1))] = list_rep[prt - 1].Get_taille() * 1000 + prt - 1;
                    corriger_remove(prt - 1, 1);
                    list_rep.RemoveAt(prt);
                    res = 1;
                }
                if (suiv == -1)
                {
                    list_rep[prt].Set_taille(list_rep[prt].Get_taille() + list_rep[prt + 1].Get_taille());
                    list_zone_libre[list_zone_libre.IndexOf(list_zone_libre.Find(x => x % 1000 == prt + 1))] = list_rep[prt].Get_taille() * 1000 + prt;
                    corriger_remove(prt, 1);
                    list_rep.RemoveAt(prt + 1);
                    res = 1;
                }
            }
            if ((prec != -1) && (suiv != -1))
            {
                list_zone_libre.Add(list_rep[prt].Get_taille() * 1000 + prt);
                return 0;
            }
            else return res;
        }

        public void corriger_remove(int prt, int dec)
        {
            int i = 0;
            while (i < list_zone_libre.Count)
            {
                if (list_zone_libre[i] % 1000 > prt) list_zone_libre[i] -= dec;
                i++;
            }

        }


    }
}
