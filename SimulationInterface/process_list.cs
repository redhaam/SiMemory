using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

using System.Windows.Media.Animation;


namespace SimulationInterface
{
    class process_list 
        {
        int id;
        public List<processus> en_cours = new List<processus>();
        public List<processus> fifo = new List<processus>();
        public List<processus> fini = new List<processus>();
        public static int finis;
        private int n;
        //public EventHandler OnTimedEvent = new EventHandler();
        public int get_finis()
        {
            return finis;
        }
        public void finish(int id)
        {
            fini.Add(en_cours.Find(x => x.Get_id() == id));
            en_cours.Remove(en_cours.Find(x => x.Get_id() == id));
            finis++;
        }
        public void Set_alg(int n)
        {
            this.n = n;
        }
        public string Get_name_by_id(int id)
        {
            return en_cours.Find(x => x.Get_id() == id).name;
        }
        public Color Get_color_by_id(int id)
        {
            return en_cours.Find(x => x.Get_id() == id).clr;
        }
        public int Get_time_by_id(int id)
        {
             return en_cours.Find(x => x.Get_id() == id).Get_temps();
        }
        public int Get_fulltime_by_id(int id)
        {
            return en_cours.Find(x => x.Get_id() == id).Get_bas();
        }
        public void corriger_prt(int prt, int desc)
        {


            (en_cours.FindAll(x => x.Get_part() > prt)).ForEach(x => x.Set_part(x.Get_part() - desc));

        }
        public void afficher_encours(StackPanel Sta, int P)
        {
            
            int i = -1;
            List<processus> all = new List<processus>();
            switch (P)
            {
                default:
                    all.AddRange(en_cours);
                    all.AddRange(fifo);
                    all.AddRange(fini);

                    break;
                case 1:
                    all.AddRange(en_cours);
                    all.AddRange(fifo);
                    all.AddRange(fini);

                    break;
                case 2:
                    all.AddRange(en_cours);
                    break;
                case 3:
                    all.AddRange(fifo);
                    break;
                case 4:
                    all.AddRange(fini);
                    break;
            }
            all.Sort();  

            while (true)
            {
                i++;
                if (i == all.Count)  break;
                Canvas F = new Canvas();
                F.Height = 30;
                F.Width = 350;
                F.HorizontalAlignment = HorizontalAlignment.Left;
                TextBlock nom = new TextBlock();
                nom.Text = all[i].name;
                nom.FontWeight = FontWeights.UltraBold;
                nom.FontSize = 16;
                nom.Width = 40;
                nom.Height = 30;
                //F.Height = +30;
                nom.TextAlignment = TextAlignment.Center;
                F.Children.Add(nom);
                Canvas.SetTop(nom, 5);
                Canvas.SetLeft(nom, 0);
                TextBlock info = new TextBlock();
                info.Text = "taille : " + all[i].Get_taille() + "    temps : " + (all[i].Get_bas() - 1);
                info.FontSize = 10;
                info.Foreground = new SolidColorBrush(Color.FromRgb(113, 113, 113));
                info.Width = 205;
                info.Height = 15;
                info.TextAlignment = TextAlignment.Center;
                F.Children.Add(info);
                Canvas.SetTop(info, 10);
                Canvas.SetLeft(info, 25);
                Rectangle prog = new Rectangle();
                Rectangle rec = new Rectangle();
                rec.Height = 5;
                prog.Height = 1;
                prog.Width = 240;
                prog.Stroke = null;
                prog.Fill = new SolidColorBrush(Color.FromRgb(62, 62, 66));
                rec.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 184));
                F.Children.Add(prog);
                F.Children.Add(rec);
                Canvas.SetTop(prog, 30);
                Canvas.SetLeft(prog, 5);
                Canvas.SetTop(rec, 28);
                Canvas.SetLeft(rec, 5);
                Ellipse et = new Ellipse();
                et.Height = 10;
                et.Width = 10;
                F.Children.Add(et);
                Canvas.SetTop(et, 15);
                Canvas.SetLeft(et, 230);
                
                nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G, all[i].clr.B));
                if (all[i].Get_bas() == all[i].Get_temps())
                {
                    rec.Width = 0;
                    et.Fill = new SolidColorBrush(Colors.Red);
                    nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G, all[i].clr.B));

                }
                else
                {
                    if (all[i].Get_temps() == 0)
                    {
                        rec.Width = 0;
                        nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G, all[i].clr.B));
                    }
                    else
                    {
                        DoubleAnimation db = new DoubleAnimation((all[i].Get_bas() - all[i].Get_temps()) * 240 / all[i].Get_bas(), (all[i].Get_bas() - all[i].Get_temps() + 1) * 240 / all[i].Get_bas(), TimeSpan.FromMilliseconds(500));
                        rec.BeginAnimation(Rectangle.WidthProperty, db);

                        info.Text += "  temps restants : " + (all[i].Get_temps() - 1);
                        et.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 184));
                        if (all[i].Get_temps() == 1)
                        {
                            et.Fill = new SolidColorBrush(Colors.Yellow);
                            nom.Foreground = new SolidColorBrush(Color.FromRgb(113, 113, 113));
                        }
                    }

                }

                Sta.Children.Add(F);
            }
        }
        public void ajout_process(string name, int taille, int temp)
        {
            processus pro = new processus(name, fifo.Count + en_cours.Count +1, taille, temp);
            fifo.Add(pro);

        }
        public void check_fifo(RAM_var mem, ref string d, int P)
        {
            int ind;
            if (fifo.Count != 0)
            {
                int i = 0;
                string nom;
                while (true)
                {
                    switch (P)
                    {

                        case 0:
                            ind = mem.firts_fit(fifo[i].Get_taille());
                            nom = "First Fit";
                            break;
                        case 1:
                            ind = mem.best_fit(fifo[i].Get_taille());
                            nom = "Best Fit";
                            break;

                        case 2:
                            ind = mem.worst_fit(fifo[i].Get_taille());
                            nom = "Worst Fit";
                            break;
                        case 3:
                            ind = mem.Next_fit(fifo[i].Get_taille());
                            nom = "Next Fit";
                            break;
                        default:
                            ind = mem.firts_fit(fifo[i].Get_taille());
                            nom = "First Fit";
                            break;


                    }
                    if (ind >= 0)
                    {
                        d += ">>>> le processus " + fifo[i].name + "  est ajouter a l'emplacement <" + ind + "> avec l'algorithme de :  <<" + nom + ">>\n\n";
                        corriger_prt(ind, mem.allocation_process(ind, fifo[i].Get_id(), fifo[i].Get_taille())); execute_process(fifo[i].Get_id(), ind);
                    }
                    else break;
                    i++;
                    if (i >= fifo.Count) break;
                }

            }

        }
        public void afficher_fifo(Canvas att)
        {
            int i = 0, pos = 1;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                while (i < fifo.Count)
                {
                    TextBlock nom = new TextBlock();
                    nom.Text = fifo[i].name;
                    nom.Width = 20;
                    nom.Height = 20;
                    nom.FontWeight = FontWeights.UltraBold;
                    nom.Foreground = new SolidColorBrush(Colors.White);
                    att.Children.Add(nom);
                    Canvas.SetZIndex(nom, 1);
                    Canvas.SetTop(nom, pos + 6);
                    Canvas.SetRight(nom, 12);
                    Ellipse front = new Ellipse();
                    front.Width = 30;
                    front.Height = 30;
                    front.Fill = new SolidColorBrush(Color.FromRgb(fifo[i].clr.R, fifo[i].clr.G, fifo[i].clr.B));
                    att.Children.Add(front);
                    Canvas.SetTop(front, pos);
                    Canvas.SetRight(front, 10);
                    pos += 31;
                    i++;
                }
            });
        }
        /*public process_list()
        {

            int taille;
            int temps_ex;
            int id = 1;
            fifo = new List<processus>();
            en_cours = new List<processus>();

            while (true)
            {
                Console.WriteLine("entrez les information de la processus");
                Console.WriteLine("taille==");
                taille = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("temps==");
                temps_ex = Convert.ToInt32(Console.ReadLine());
                if (taille == 0) break;
                processus pro = new processus(id++, taille, temps_ex);
                fifo.Add(pro);
            }
        }*/


        /*public void une_file()      //ordonnancement par une seule file d'attente

        {
            int taille;
            int temps_ex;
            int id = 1;

            fifo = new List<processus>();
            en_cours = new List<processus>();
            List<int> liste = new List<int>()
            RAM ram = new RAM(liste);
            int n; int t;
            Console.WriteLine("quel algorithme de recherche voullez vous utiliser?");
            Console.WriteLine("1)first_fit");
            Console.WriteLine("2)Next_fit");
            Console.WriteLine("3)Best_fit");
            Console.WriteLine("4)Worst_fit");
            n = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("entrez les information de la processus");
                Console.WriteLine("taille==");
                taille = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("temps==");
                temps_ex = Convert.ToInt32(Console.ReadLine());
                if (taille == 0) break;
                processus proc = new processus(id++, taille, temps_ex);

                t = 0;

                switch (n)
                {
                    case 1:
                        t = ram.Rech_first(proc);
                        break;
                    case 2:
                        t = ram.Rech_best(proc);
                        break;
                    case 3:
                        t = ram.Rech_worst(proc);
                        break;

                }


                if (t != -1)
                {
                    if (ram.list_rep[t].G_vide() == true)
                    {
                        Console.WriteLine("une place a ete trouvé");
                        proc.Set_etat(1);
                        proc.Set_part(t);
                        en_cours.Add(proc);
                        ram.occ_part(ram.list_rep[t]);
                    }
                }
                else { Console.WriteLine("ajoute a la fifo"); fifo.Add(proc); }
            }
            Console.WriteLine("*******La RAM :");
            ram.affich_ram();
            Console.WriteLine("*******les proce en cour d'execution****");
            afficher_encours();
            Console.WriteLine("fifo");
            afficher_fifo();
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 980;
            aTimer.Enabled = true;
            while (en_cours.Count == 0) ;

            void OnTimedEvent(object source, ElapsedEventArgs e)
            {
                int part = 0;
                for (int i = 0; i < en_cours.Count; i++)
                {
                    en_cours[i].Set_time(en_cours[i].Get_temps() - 1);
                    if (en_cours[i].Get_temps() == 0)
                    {
                        part = en_cours[i].Get_part();
                        Console.WriteLine("le processus:  " + en_cours[i].id + "  de taille:  " + en_cours[i].Get_taille() + "  est terminé");
                        en_cours.Remove(en_cours[i]);
                        ram.vider_part(ram.list_rep[part]);
                        Console.WriteLine("Ram");
                        Console.WriteLine("juste apres supression du processus en cours d'execution :");
                        ram.affich_ram();
                        afficher_encours();
                        afficher_fifo();

                        if (fifo.Count == 0) { Console.WriteLine("FIN de la fifo"); }
                        else
                        {
                            t = 0;
                            switch (n)
                            {
                                case 1:
                                    t = ram.Rech_first(fifo[0]);
                                    break;
                                case 2:
                                    t = ram.Rech_best(fifo[0]);
                                    break;
                                case 3:
                                    t = ram.Rech_worst(fifo[0]);
                                    break;
                            }
                            if (t == -1) { Console.WriteLine("le processus ne trouve pasde place "); }
                            while (t != -1)
                            {
                                Console.WriteLine(ram.list_rep[t].Get_taille());
                                fifo[0].Set_part(t);
                                en_cours.Add(fifo[0]);
                                fifo.Remove(fifo[0]);
                                ram.occ_part(ram.list_rep[t]);
                                if (fifo.Count == 0) { Console.WriteLine("FIN de la fifo"); break; }

                                switch (n)
                                {
                                    case 1:
                                        t = ram.Rech_first(fifo[0]);
                                        break;
                                    case 2:
                                        t = ram.Rech_best(fifo[0]);
                                        break;
                                    case 3:
                                        t = ram.Rech_worst(fifo[0]);
                                        break;

                                }
                            }
                        }
                        ram.affich_ram();
                        afficher_encours();
                        afficher_fifo();
                    }
                }
            }
        }
        */







        public void Add_Fifo(int taille , int delay)
        {
            processus p = new processus("P"+ fifo.Count  , id++ , taille, delay);
            fifo.Add(p);
        }









        static int minute = 0;

        public void une_file_rnd(RAM_fix ram ,List<string> Derl)      //ordonnancement par une seule file d'attente
        {
            int t = 0;
            //n = 1;
            int part = 0;
            
            if (fifo.Count != 0)
            {
                t = 0;
                switch (n)
                {
                    case 1:
                        t = ram.Rech_first(fifo[0]);
                        break;
                    case 2:
                        t = ram.Rech_best(fifo[0]);
                        break;
                    case 3:
                        t = ram.Rech_worst(fifo[0]);
                        break;
                }
                if (t != -1)
                {
                    Derl.Add(">>>> le processus <" + fifo[0].name + "> est ajouter a lemplacement <" + t + ">\n\n");
                    fifo[0].Set_part(t);
                    en_cours.Add(fifo[0]);
                    ram.occ_part(ram.list_rep[t], fifo[0], fifo[0].Get_taille());
                    //ram.list_rep[t].Set_id(fifo[0].Get_id());
                    fifo.Remove(fifo[0]);
                }
            }

            t = -1;
            for (int i = 0; i < en_cours.Count; i++)
            {
                if (en_cours[i] != null)
                {
                    en_cours[i].Set_time(en_cours[i].Get_temps() - 1);
                    if (en_cours[i].Get_temps() == 0)
                    {
                        Derl[minute+1] += ">>>> le processus <" + en_cours[i].name + "> est libere de lemplacement<" + en_cours[i].Get_part() + ">\n\n";
                        part = en_cours[i].Get_part();
                        //finish(en_cours[i].Get_id());
                        ram.vider_part(ram.list_rep[part], en_cours[i].Get_taille());
                        finis++;
                        fini.Add(en_cours[i]);
                        en_cours.Remove(en_cours[i]);
                        i--;
                        if (fifo.Count != 0) { t = ram.Rech_first(fifo[0]); }
                    }
                }
            }
            minute += 1;
        }
        

        public void execute_process(int id, int rep)
        {
           

            processus pro = new processus("", id, 0, 0);
            pro = fifo.Find(x => x.Get_id() == id);
            fifo.Remove(pro);
            pro.Set_part(rep);
            en_cours.Add(pro);

        }
        public void ajout_process(int taille, int temp)
        {
            processus pro = new processus("process", fifo.Count + en_cours.Count +1 , taille, temp);
            fifo.Add(pro);

        }
        public void afficher_encours()
        {
            int i = 0;
            Console.WriteLine("les processus :");
            while (true)
            {
                if (en_cours.Count == 0) { Console.WriteLine("liste vide"); break; }
                if (i >= en_cours.Count) break;
                Console.WriteLine("id==" + en_cours[i].Get_id().ToString() + "****taille==" + en_cours[i].Get_taille().ToString());
                i++;
            }
        }
        public void afficher_fifo()
        {
            int i = 0;
            Console.WriteLine("la fifo :");
            while (true)
            {
                if (fifo.Count == 0) { Console.WriteLine("liste vide"); break; }
                if (i >= fifo.Count) break;
                Console.WriteLine("id==" + fifo[i].Get_id().ToString() + "****taille==" + fifo[i].Get_taille().ToString());
                i++;

            }
        }
        


        public void add_process(int taille, int temps, RAM_fix ram)
        {
            List<int> liste = new List<int>();
            processus proc;
            
            int j = 0;
            int i = 0;
            proc = new processus("pr"+id,id++, taille, temps);
            
            Boolean rech = true;
            i = 0;

            while (i < ram.list_rep.Count)
            {
                liste.Add(ram.list_rep[i].Get_taille() * 1000 + i);
                i++;
                
            }
            liste.Sort();
            i = 0;
            while ((rech) && (i < liste.Count))
            {
                if (liste[i] / 1000 >= proc.Get_taille())
                {
                    rech = false;
                }
                else { i++; }
            }
            if (rech == false)//si on trouve la partition qui convient 
            {
                j = liste[i] % 1000;
                proc.Set_part(j);
                if ((ram.list_rep[j].G_vide()) == false) //si elle est occupee
                {
                    ram.list_rep[j].f_proc.Add(proc);
                    fifo.Add(proc);

                }
                else//le processus  est ajoute a la liste des proc en cours d'execution 
                {
                    proc.Set_etat(1);
                    
                    en_cours.Add(proc);

                    ram.occ_part(ram.list_rep[j],proc,proc.Get_taille());

                }
            }
        }


        public void plusieurs_fifo(RAM_fix ram)   //ordonnancement des processus par la methode de +files
        {
            int i = 0;
            i = 0;
                int part = 0;

                    for (i = 0; i < en_cours.Count; i++)
                    {
                       if (en_cours[i]!=null)
                        {
                            en_cours[i].Set_time(en_cours[i].Get_temps() - 1);
                            if (en_cours[i].Get_temps() == 0)
                            {
                        finis++;
                        fini.Add(en_cours[i]);
                                part = en_cours[i].Get_part();
                                //en_cours.Remove(en_cours[i]);
                                //ram.vider_part(ram.list_rep[part]);
                                if (ram.list_rep[part].f_proc.Count > 0)
                                {
                                    en_cours[i] = ram.list_rep[part].f_proc[0];
                            fifo.Remove(ram.list_rep[part].f_proc[0]);
                                    ram.list_rep[part].Set_id(en_cours[i].Get_id());
                                    //ram.occ_part(ram.list_rep[part]);
                                    ram.list_rep[part].f_proc.Remove(ram.list_rep[part].f_proc[0]); 
                                }
                                else
                                {
                                    //
                                    ram.vider_part(ram.list_rep[part],en_cours[i].Get_taille());
                                    en_cours.Remove(en_cours[i]);i--;
                                }
                            }
                        }
                    }
                    
           
        }
        public int calcul_fifo()
        {
            if ((fifo.Count + en_cours.Count + finis) != 0)
            {
                return fifo.Count * 100 / (fifo.Count + en_cours.Count + finis);
            }
            else return 0;
        }
        public int calcul_en_cours()
        {
            if ((fifo.Count + en_cours.Count + finis) != 0)
            {
                return en_cours.Count * 100 / (fifo.Count + en_cours.Count + finis);
            }
            else return 0;
        }
        public int calcul_finis()
        {
            if ((fifo.Count + en_cours.Count + finis) != 0)
            {
                return finis * 100 / (fifo.Count + en_cours.Count + finis);
            }
            else return 0;
        }













       
        public void lilster_pro(StackPanel k,  int choi)
        {
            int bas, cour, x = 20;
            switch (choi)
            {
                case 0:
                    int pos = 0;
                    foreach (processus p in en_cours)
                    {
                        /*TextBlock tb = new TextBlock();
                        tb.Background = new SolidColorBrush(Color.FromRgb(221, 221, 212));
                        tb.Height = 25;
                        tb.Text = "Nom : " + p.name + "   Taille : " + p.Get_taille() + "   Temps Restant : " + p.Get_temps();
                        ProgressBar pb = new ProgressBar();
                        pb.IsIndeterminate = false;
                        pb.Orientation = Orientation.Horizontal;
                        pb.Width = 400;
                        pb.Height = 1;
                        Duration dur = new Duration(TimeSpan.FromSeconds(1000));
                        DoubleAnimation a = new DoubleAnimation(330.0, dur);
                        pb.BeginAnimation(ProgressBar.ValueProperty, a);
                        cour = p.Get_temps(); bas = p.Get_bas();
                        pb.Maximum = bas - 1;
                        pb.Value = bas - cour - 1;
                        k.Children.Add(tb);
                        k.Children.Add(pb);*/

                        Canvas att = new Canvas();
                        att.Width =100;
                        att.Height = 30;

                        TextBlock nom = new TextBlock();
                        nom.Text = p.name;
                        nom.Width = 20;
                        nom.Height = 20;
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Foreground = new SolidColorBrush(Colors.Black);
                        att.Children.Add(nom);
                        Canvas.SetZIndex(nom, 1);
                        Canvas.SetTop(nom, pos + 6);
                        Canvas.SetRight(nom, 12);
                        Ellipse front = new Ellipse();
                        front.Width = 30;
                        front.Height = 30;
                        front.Fill = new SolidColorBrush(Color.FromRgb(p.clr.R, p.clr.G, p.clr.B));
                        att.Children.Add(front);
                        Canvas.SetTop(front, pos);
                        Canvas.SetRight(front, 10);
                        pos += 31;

                        k.Children.Add(att);
                        
                    }
                    break;
                case 1:
                    foreach (processus p in fifo)
                    {
                        TextBlock tb = new TextBlock();
                        tb.Height = 25;
                        tb.Background = new SolidColorBrush(Color.FromRgb(221, 221, 212));
                        tb.Text = "Nom : " + p.name + "   Taille : " + p.Get_taille() + "   Temps Restant : " + p.Get_temps();
                        k.Children.Add(tb);
                    }
                    break;
                case 2:
                     pos = 0;
                    foreach (processus p in en_cours)
                    {
                        /*TextBlock tb = new TextBlock();
                        tb.Background = new SolidColorBrush(Color.FromRgb(221, 221, 212));
                        tb.Height = 25;
                        tb.Text = "Nom : " + p.name + "   Taille : " + p.Get_taille() + "   Temps Restant : " + p.Get_temps();
                        ProgressBar pb = new ProgressBar();
                        pb.IsIndeterminate = false;
                        pb.Orientation = Orientation.Horizontal;
                        pb.Width = 400;
                        pb.Height = 1;
                        Duration dur = new Duration(TimeSpan.FromSeconds(1000));
                        DoubleAnimation a = new DoubleAnimation(330.0, dur);
                        pb.BeginAnimation(ProgressBar.ValueProperty, a);

                        cour = p.Get_temps(); bas = p.Get_bas();


                        pb.Maximum = bas - 1;
                        pb.Value = bas - cour - 1;
                        Canvas.SetLeft(pb, 10);
                        Canvas.SetTop(pb, x + 50);
                        k.Children.Add(tb);
                        k.Children.Add(pb);*/
                        Canvas att = new Canvas();
                        att.Width = 300;
                        att.Height = 30;
                        att.HorizontalAlignment = HorizontalAlignment.Center;
                        TextBlock nom = new TextBlock();
                        nom.Text = p.name;
                        nom.Width = 20;
                        nom.Height = 20;
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Foreground = new SolidColorBrush(Colors.Black);
                        att.Children.Add(nom);
                        Canvas.SetZIndex(nom, 1);
                        Canvas.SetTop(nom, pos + 6);
                        Canvas.SetRight(nom, 12);
                        Ellipse front = new Ellipse();
                        front.Width = 30;
                        front.Height = 30;
                        front.Fill = new SolidColorBrush(Color.FromRgb(p.clr.R, p.clr.G, p.clr.B));
                        att.Children.Add(front);
                        Canvas.SetTop(front, pos);
                        Canvas.SetRight(front, 10);
                        pos += 31;

                        k.Children.Add(att);
                    }
                    foreach (processus p in fifo)
                    {
                        /*TextBlock tb = new TextBlock();
                        tb.Background = new SolidColorBrush(Color.FromRgb(221, 221, 212));
                        tb.Height = 25;
                        tb.Text = "Nom : " + p.name + "   Taille : " + p.Get_taille() + "   Temps Restant : " + p.Get_temps();
                        k.Children.Add(tb);*/

                        Canvas att = new Canvas();
                        att.Width = 300;
                        att.Height = 30;

                        TextBlock nom = new TextBlock();
                        nom.Text = p.name;
                        nom.Width = 20;
                        nom.Height = 20;
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Foreground = new SolidColorBrush(Colors.Black);
                        att.Children.Add(nom);
                        Canvas.SetZIndex(nom, 1);
                        Canvas.SetTop(nom, pos + 6);
                        Canvas.SetRight(nom, 12);
                        Ellipse front = new Ellipse();
                        front.Width = 30;
                        front.Height = 30;
                        front.Fill = new SolidColorBrush(Color.FromRgb(p.clr.R, p.clr.G, p.clr.B));
                        att.Children.Add(front);
                        Canvas.SetTop(front, pos);
                        Canvas.SetRight(front, 10);
                        pos += 31;

                        k.Children.Add(att);
                       

                    }

                    break;
                default: break;
            }

        }

        public void CreateACircle(List<processus> k, Canvas c)
        {
            //c.Children.Clear();
            float x = 300;
            if (k.Count != 0)
            {
                int ps;
                if (k.Count > 7) ps = 7;
                else ps = k.Count;
                Rectangle r = new Rectangle();
                r.RadiusX = 20;
                r.RadiusY = 20;
                r.Fill = new SolidColorBrush(Colors.Gray);
                r.Width = ps * 35 + 10;
                r.Height = 38;
                c.Children.Add(r);
                Canvas.SetLeft(r, 300);
                Canvas.SetTop(r, 149);
            }
            int i = 0;
            foreach (processus p in k)
            {
                Ellipse Circ = new Ellipse();
                Circ.Height = 30;
                Circ.Width = 30;

                Circ.Stroke = Brushes.Black;
                Circ.StrokeThickness = 1;
                Circ.Fill = new SolidColorBrush(Color.FromRgb(p.clr.R, p.clr.G, p.clr.B));
                c.Children.Add(Circ);
                Canvas.SetLeft(Circ, x+5);
                Canvas.SetTop(Circ, 152);
                TextBlock nom = new TextBlock();
                nom.Text = p.name;
                nom.Foreground = new SolidColorBrush(Colors.Black);
                nom.FontWeight = FontWeights.UltraBold;
                nom.Width = 20;
                nom.Height = 20;
                c.Children.Add(nom);
                Canvas.SetTop(nom, 156);
                Canvas.SetLeft(nom, x + 9);
                Canvas.SetZIndex(nom, 1);
                x = x + 35;
                if (x == 460) break;
                i++;
                if (i == 7) break;


            }

        }
        public void CreateACircle_2(RAM_fix mc, Canvas c)
        {
            c.Children.Clear();
            float x = 0;
            foreach (partition p in mc.list_rep)
            {
                int i=0;
                float y = 320;
                if(p.f_proc.Count!=0)
                {
                    int ps;
                    if (p.f_proc.Count > 7) ps = 7;
                    else ps = p.f_proc.Count;
                    Rectangle r = new Rectangle();
                    r.RadiusX = 20;
                    r.RadiusY = 20;
                    r.Fill = new SolidColorBrush(Colors.Gray);
                    r.Width =ps*35+10;
                    r.Height = 38;
                    c.Children.Add(r);
                    Canvas.SetLeft(r, y-10);
                    Canvas.SetTop(r, x + 6);
                }
                
                foreach (processus pr in p.f_proc)
                {
                    Ellipse Circ = new Ellipse();
                    Circ.Height = 30;
                    Circ.Width = 30;
                    Circ.Stroke = Brushes.Black;
                    Circ.StrokeThickness = 1;
                    Circ.Fill = new SolidColorBrush(Color.FromRgb(pr.clr.R, pr.clr.G, pr.clr.B));
                    c.Children.Add(Circ);
                    Canvas.SetLeft(Circ, y-2);
                    Canvas.SetTop(Circ, x + 10);
                    TextBlock nom = new TextBlock();
                    nom.Text = pr.name;
                    nom.Foreground = new SolidColorBrush(Colors.Black);
                    nom.FontWeight = FontWeights.UltraBold;
                    nom.Width = 20;
                    nom.Height = 20;
                    c.Children.Add(nom);
                    Canvas.SetTop(nom, x + 14);
                    Canvas.SetLeft(nom, y + 4);
                    Canvas.SetZIndex(nom, 1);
                    y = y + 35;
                    i++;
                    if (i == 7) break;
                }

                x = x + (Convert.ToSingle(p.Get_taille()) * 435) / Convert.ToSingle(mc.Get_Capacity());


            }

        }
    }
}
