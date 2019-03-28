using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Media.Animation;

namespace SimulationInterface
{
    class RAM
    {
        public int capacite;   // la capacite de la ram exprimer en ko
        public List<int> list_zone_libre;// la liste des zones libres de la MC(!!!!les trois premiers position determine le num de la partition et les autres sa taille!!!!!)
        public List<partition> list_rep;
        protected static int next_fit;

        // la liste de toute les partitions de la ram
        public void afficher(Canvas ram, process_list pro)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                int i = 0;
                int pos = 0;
                byte color = 0;
                while (true)
                {
                    list_rep[i].View(ram, pos, color, pro, capacite);
                    color += 100 % 255;
                    pos += list_rep[i].Get_taille() * 435 / capacite;
                    i++;
                    if (i == list_rep.Count) break;
                }
            });
        }
        public void afficher_ptr_zonelibre()
        {
            list_zone_libre.ForEach(delegate (int num)
            {
                Console.WriteLine(num.ToString());
            });

        }
        public void afficher_libre()
        {
            int i = -1;
            while (true)
            {
                i++;
                if (i == list_zone_libre.Count) break;
                Console.WriteLine((list_zone_libre[i] % 1000).ToString() + ")   adr==" + list_rep[(list_zone_libre[i] % 1000)].Get_adr().ToString() + "taille=" + list_rep[(list_zone_libre[i] % 1000)].Get_taille().ToString() + "vide=" + list_rep[(list_zone_libre[i] % 1000)].G_vide().ToString());

            }

        }
        public int calcul_esplibre()
        {
            int res = 0, i = 0;
            while (true)
            {
                if (i == list_rep.Count) break;
                if (list_rep[i].Get_id() < 0) res += list_rep[i].Get_taille();
                i++;
            }
            Console.WriteLine("esp_lib==" + res);
            return res;
        }
    }
}
