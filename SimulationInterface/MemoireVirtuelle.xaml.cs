using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Windows.Media.Animation;
namespace SimulationInterface
{
    public partial class MemoireVirtuelle : Page
    {
        private int nbPages = 5;
        private int etape = 0;
        private long vitesse = 1000;
        private int iteration;
        private SystemExploitation systemExploitation = new SystemExploitation();
        private RamVirtuelle ram = new RamVirtuelle(5);
        private DiskDur diskDur = new DiskDur(5);
        private List<EntreeTablePage> tablePages = new List<EntreeTablePage>();
        private List<String> requete = new List<String>();
        private Stack<List<object>> sequence = new Stack<List<object>>();
        private String sauvSuite = "";
        int[] nbr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private FlowDocumentScrollViewer documentReader = new FlowDocumentScrollViewer();
        System.Timers.Timer timer = new System.Timers.Timer();
        private Table oTable = new Table();
        private FlowDocument oDoc = new FlowDocument()
        { TextAlignment = TextAlignment.Center };
        public MemoireVirtuelle()
        {
           //documentReader.li
            InitializeComponent();
            systemExploitation.creationTablePages(tablePages, 2 * nbPages, ram, diskDur);
            afficherRam(ram);
            affichTablePage();
            affichMemVirtuelle();


        }



        private void affichMemVirtuelle()
        {
            double y = 0;
            double x = 0;
            Rectangle shape = new Rectangle();
            int k = 0;
            int plc = 20;
            double plcy = 50;
            foreach (EntreeTablePage entree in tablePages)
            {
                TextBlock np = new TextBlock();
                shape = new Rectangle();
                shape.Height = ((1 * 500) / tablePages.Count);
                shape.Width = 150;

                x = (x + shape.Height) + 1;
                plc += (int)shape.Height + 1;
                if (entree.getDisponible() == true)
                {
                    shape.Fill = Brushes.LightGray;
                }
                else
                {
                    shape.Fill = Brushes.DarkGray;
                    
                }
                np.FontSize                = 20;
                np.Text                    = k.ToString();
                np.TextAlignment = TextAlignment.Center;
                np.Width = 50;
                shape.MouseLeftButtonDown += delegate
                {

                        if (suiteReferences.Text == "") suiteReferences.Text = np.Text;
                        else suiteReferences.Text += " " + np.Text;
                    if (!lancer.IsEnabled) { lancer.IsEnabled = true; suivant.IsEnabled = true; }


                };
;

                memVirtuelle.Children.Add(shape);
                Canvas.SetLeft(shape, y);
                Canvas.SetTop(shape, x);
                memVirtuelle.Children.Add(np);
                Canvas.SetLeft(np, plcy);
                Canvas.SetTop(np, plc);
                k++;
            }

        }



public delegate void ajoutNombre(UIElement element);


        private void affichTablePage()
        {

            oDoc.Blocks.Add(oTable);
            int numberOfColumns = 4;
            for (int x = 0; x < numberOfColumns; x++)

            {
                oTable.Columns.Add(new TableColumn());
                oTable.Columns[x].Width = new GridLength(80);
            }

            oTable.RowGroups.Add(new TableRowGroup());
            oTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = oTable.RowGroups[0].Rows[0];
            currentRow.Background = Brushes.Navy;
            currentRow.Foreground = Brushes.White;
            String[] titres = new String[] { "Adresse \nvirtuelle", "Adresse \nphysique" , "Bit de \nvalidité", "bit de \nmodification" };
            for(int k=0;k<numberOfColumns;k++)
            currentRow.Cells.Add( new TableCell(new Paragraph(new Run(titres[k]))));

            int i = 0;
            foreach (EntreeTablePage e in tablePages)
            {
                currentRow.Background = Brushes.White;
                currentRow.Foreground = Brushes.Black;
                oTable.RowGroups[0].Rows.Add(new TableRow());
                currentRow = oTable.RowGroups[0].Rows[i + 1];
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(i)))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(e.getPageCorrespandante())))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(Convert.ToInt32(e.getDisponible()))))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(Convert.ToInt32(e.getDirty()))))));
                //foreach (var c in currentRow.Cells)
                //{
                //    c.TextAlignment = TextAlignment.Center;
                //}

                i++;
            }
            currentRow.Background = Brushes.White;
            currentRow.Foreground = Brushes.Black;
            documentReader.Document = oDoc;

            tPages.Content = documentReader;
            tPages.VerticalScrollBarVisibility = 0;
        }


        //private void derouler(object sender, RoutedEventArgs e)
        //{

        //    stop.IsEnabled = true;
        //    suiteReferences.IsReadOnly = true;
        //    if (iteration == 0)
        //    {
        //        sauvSuite = suiteReferences.Text;
        //        requete = sauvSuite.Split(' ').ToList<String>();
        //    }
        //    if (iteration < suiteReferences.Text.Split(' ').Length)
        //    {
        //        sequence.Push(new List<object> { systemExploitation.Clone(), ram.Clone(), diskDur.Clone(), new List<EntreeTablePage>(tablePages.Select(x => x.Clone())), requete, sauvSuite });
        //        if (requete.Count != 0)
        //        {
        //            int r;
        //            try
        //            {
        //                r = Convert.ToInt32(requete[iteration]);
        //            }
        //            catch (FormatException)
        //            {
        //                return;
        //            }
        //            EntreeTablePage entreeTablePage = Mmu.traductionAdresse(tablePages, r);
        //            oTable.RowGroups[0].Rows[r].Background.BeginAnimation(BackgroundProperty, new ColorAnimation());
        //            switch (choixAlgorithme.SelectedIndex)
        //            {
        //                case 3:
        //                    affichMatriceAging();
        //                    break;

        //            }
        //            if (entreeTablePage == null)
        //            {
        //                // acces page non alloué
        //            }
        //            else
        //            {
        //                if (entreeTablePage.getDisponible() == false) //defaut de page
        //                {
        //                    switch (choixAlgorithme.SelectedIndex)
        //                    {
        //                        case 0:
        //                            if (ram.getNombrepagesLibres() == 0) //remplacement
        //                            {
        //                                int indRam = systemExploitation.fileLru.Peek();
        //                                EntreeTablePage tmp = new EntreeTablePage();
        //                                tmp = tablePages.Find(g => g.getPageCorrespandante() == indRam && g.getDisponible() == true);
        //                                ColorAnimation animation = new ColorAnimation(Brushes.Azure, new Duration(new TimeSpan(100000)));
        //                                oTable.RowGroups[0].Rows[tablePages.FindIndex(mo => mo == tmp)].BeginAnimation(BackgroundProperty, animation);
        //                                oTable.RowGroups[0].Rows[tablePages.FindIndex(mo => mo == tmp)].Cells[2].BeginAnimation(ForegroundProperty, animation);
        //                                oTable.RowGroups[0].Rows[tablePages.FindIndex(mo => mo == tmp)].Cells[2] = new TableCell(new Paragraph(new Run("0")));
        //                                oTable.RowGroups[0].Rows[tablePages.FindIndex(mo => mo == tmp)].Cells[3] = new TableCell(new Paragraph(new Run("0")));
        //                                oTable.RowGroups[0].Rows[r].Cells[2] = new TableCell(new Paragraph(new Run("1")));

        //                                oTable.RowGroups[0].Rows[tablePages.FindIndex(mo => mo == tmp)].Cells[1] = new TableCell(new Paragraph(new Run(hdd.tableAuxiliere[indRam])));
        //                                //hdd.tableAuxiliere[indRam] = tablePages[entree].getPageCorrespandante();
        //                                oTable.RowGroups[0].Rows[r].Cells[1] = new TableCell(new Paragraph(new Run(Convert.ToString(indRam))));
        //                            }
        //                            else
        //                            {
        //                                int ipage = 0;
        //                                ipage = ram.listPages.FindIndex(p => p.getVide() == true);
        //                                int entree = tablePages.FindIndex(x => x.getPageCorrespandante() == adressePhysique);
        //                                tablePages[entree].setDisponible(true);
        //                                tablePages[entree].setPageCorrespandante(ipage);
        //                                hdd.tableAuxiliere[ipage] = adressePhysique;
        //                                ram.listPages[ipage].setVide(false);
        //                                ram.setNombrePagesLibres(ram.getNombrepagesLibres() - 1);

        //                            }
        //                            fileLru.Enqueue(entreeTablePage.getPageCorrespandante());
        //                            break;
        //                    }






        //                }
        //                else
        //                {

        //                }
        //                precedent.IsEnabled = true;
        //                if (iteration == suiteReferences.Text.Split(' ').Length) lancer.IsEnabled = false;
        //            }
        //        }
        //    }
        //}



        private void majTablePage()
        {
            TableRow currentRow = oTable.RowGroups[0].Rows[0];
            int i = 0;
            foreach (EntreeTablePage e in tablePages)
            {
                currentRow.Background = Brushes.White;
                currentRow.Foreground = Brushes.Black;
                oTable.RowGroups[0].Rows.Add(new TableRow());
                currentRow = oTable.RowGroups[0].Rows[i + 1];
                currentRow.Cells[0] = new TableCell(new Paragraph(new Run(Convert.ToString(i))));
                currentRow.Cells[1] = new TableCell(new Paragraph(new Run(Convert.ToString(e.getPageCorrespandante()))));
                currentRow.Cells[2] = new TableCell(new Paragraph(new Run(Convert.ToString(Convert.ToInt32(e.getDisponible())))));
                currentRow.Cells[3] = new TableCell(new Paragraph(new Run(Convert.ToString(Convert.ToInt32(e.getDirty())))));
                i++;
            }
            documentReader.Document = oDoc;
            tPages.Content = documentReader;
        }

        private void afficherRam(RamVirtuelle ram)
        {
            double y = 0;
            double x = 0;
            Rectangle shape = new Rectangle();
            int k = 0;
            int plc = 0; 
            double plcy = 50;
            foreach (Part p in ram.listPages)
            {
                TextBlock np = new TextBlock()
                { TextAlignment = TextAlignment.Center, Width = 50, FontFamily = new FontFamily("Roboto"), FontSize = 20,VerticalAlignment=VerticalAlignment.Center,Height= ((1 * 250) / ram.getNombrePages()) };
                shape = new Rectangle();
                shape.Height = ((1 * 250) / ram.getNombrePages());
                shape.Width = 150;

                x = (x + shape.Height) + 1;
                plc += (int)shape.Height + 1;
                if (p.getVide() == true)
                {
                    shape.Fill = Brushes.DarkCyan;
                }
                else
                {
                    shape.Fill = Brushes.LightCyan;
                    int mil = tablePages.FindIndex(e => e.getPageCorrespandante() == k && e.getDisponible() == true);
                    np.Text = Convert.ToString(mil);
                }
                canvas1.Children.Add(shape);
                Canvas.SetLeft(shape, y);
                Canvas.SetTop(shape, x);
                canvas1.Children.Add(np);
                Canvas.SetLeft(np, plcy);
                Canvas.SetTop(np, plc);
                k++;
            }
        }

        private void affichMatriceAging()
        {
             FlowDocumentScrollViewer docr = new FlowDocumentScrollViewer();
            Table table = new Table();
             FlowDocument ag = new FlowDocument();

             ag.Blocks.Add(table);
            table.Columns.Add(new TableColumn());
            table.Columns[0].Width = new GridLength(30);

            table.Columns.Add(new TableColumn());
            table.Columns[1].Width = new GridLength(200);
            
            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table.RowGroups[0].Rows[0];




            int i = 0;
            foreach (int e in systemExploitation.aging)
            {
                currentRow.Foreground = Brushes.Black;
                currentRow.Background = Brushes.White;
                table.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table.RowGroups[0].Rows[i + 1];
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(i)))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Join("", Enumerable.Repeat<String>("0", 16 - (Convert.ToString(e, 2).Length)).ToList<String>()) + Convert.ToString(e, 2)))));
                i++;
            }
            currentRow.Background = Brushes.White;
            currentRow.Foreground = Brushes.Black;
            docr.Document = ag;
            docr.VerticalScrollBarVisibility = 0;
            aux.Children.Clear();
            aux.Children.Add(docr);
            Canvas.SetLeft(docr, 0);
            Canvas.SetTop(docr, 0);

        }


        private void suppFileFifo()
        {
            int i = 0;
            aux.Children.RemoveAt(aux.Children.Count - 1);
            aux.Children.RemoveAt(aux.Children.Count - 1);
            foreach (UIElement x in aux.Children)
            {
                DoubleAnimation db = new DoubleAnimation
                {
                    To = Canvas.GetLeft(x) - 30 - 10,
                    Duration = new Duration(new TimeSpan(1000000))
                };
                x.BeginAnimation(Canvas.LeftProperty, db);
                i++;
            }

        }

        private void affichLfu()
        {

            FlowDocumentScrollViewer auxr = new FlowDocumentScrollViewer();
            Table tbl = new Table();
            FlowDocument lfuDoc = new FlowDocument();
            lfuDoc.Blocks.Add(tbl);
            for (int x = 0; x < 2; x++)

            {
                tbl.Columns.Add(new TableColumn());
                tbl.Columns[x].Width = new GridLength(50);
            }
            tbl.RowGroups.Add(new TableRowGroup());
            tbl.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = tbl.RowGroups[0].Rows[0]; ;

            double xx = 0;
            double y = 0;
            List<int[]> tompo = new List<int[]>(systemExploitation.lfu);
                tompo.Sort((x,yx)=> x[0].CompareTo(yx[0]));
            for (int ind = 0; ind < nbPages; ind++)
            {
                currentRow = tbl.RowGroups[0].Rows[ind];
                currentRow.Foreground = Brushes.Black;
                currentRow.Background = Brushes.White;
                tbl.RowGroups[0].Rows.Add(new TableRow());
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(ind.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("0"))));

            }
            int i = 0;
            //    foreach (int[] f in tompo)
            //{

            //    currentRow = tbl.RowGroups[0].Rows[i];
            //    currentRow.Foreground = Brushes.Black;
            //    if (i == f[0]) currentRow.Cells.Add( new TableCell(new Paragraph(new Run(f[1].ToString()))))  ;
            //  //  currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(tablePages.FindIndex(t => t.getPageCorrespandante() == f[0] && t.getDisponible() == true))))));
            //    //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(f[0]==i?f[1]:0)))));
            //    i++;
            //}
            currentRow.Foreground = Brushes.Black;
            currentRow.Background = Brushes.White;
            auxr.Document = lfuDoc;
            auxr.Width = 60;
            auxr.VerticalScrollBarVisibility = 0;
            auxr.Document = lfuDoc;
            aux.Children.Clear();
            aux.Children.Add(auxr);
            Canvas.SetLeft(auxr, xx);
            Canvas.SetTop(auxr, y);

        }


        private void suppFileLru()
        {
            int i = 0;

            aux.Children.RemoveAt(aux.Children.Count - 1);
            aux.Children.RemoveAt(aux.Children.Count - 1);
            foreach (UIElement x in aux.Children)
            {
                DoubleAnimation db = new DoubleAnimation
                {
                    To = Canvas.GetLeft(x) - 30 - 10,
                    Duration = new Duration(new TimeSpan(1000000))
                };
                x.BeginAnimation(Canvas.LeftProperty, db);
                i++;
            }

        }


        private void ajoutFileFifo()
        {
            if (systemExploitation.fifo.Count != 0)
            {
                int l = systemExploitation.fifo.LastOrDefault();
                Ellipse ellipse = new Ellipse()
                { Height = 30, Width = 30, Fill = Brushes.Cyan };
                TextBlock num = new TextBlock
                { Text = tablePages.FindIndex(t => t.getPageCorrespandante() == l && t.getDisponible() == true).ToString(), FontSize = 14 };
                double xx = 0;
                double y = 0;
                int i = 0;
                foreach (UIElement x in aux.Children)
                {
                    DoubleAnimation db = new DoubleAnimation
                    {
                        To = Canvas.GetLeft(x) + ellipse.Width + 10,
                        Duration = new Duration(new TimeSpan(1000000))
                    };
                    x.BeginAnimation(Canvas.LeftProperty, db);
                    i++;
                }
                aux.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, xx);
                Canvas.SetTop(ellipse, y);
                aux.Children.Add(num);
                Canvas.SetLeft(num, xx + ellipse.Height / 2 - num.FontSize / 2 + 1);
                Canvas.SetTop(num, y + ellipse.Width / 2 - num.FontSize / 2);
            }
        }

        private void ajoutFileLru()
        {
            if (systemExploitation.fileLru.Count != 0)
            {
                int l = systemExploitation.fileLru.LastOrDefault();
                Ellipse ellipse = new Ellipse()
                { Height = 30, Width = 30, Fill = Brushes.Cyan };

                TextBlock num = new TextBlock
                { Text = tablePages.FindIndex(t => t.getPageCorrespandante() == l && t.getDisponible() == true).ToString(), FontSize = 14 };
                double xx = 0;
                double y = 0;
                int i = 0;
                foreach (UIElement x in aux.Children)
                {
                    DoubleAnimation db = new DoubleAnimation
                    {
                        To = Canvas.GetLeft(x) + ellipse.Width + 10,
                        Duration = new Duration(new TimeSpan(1000000))
                    };
                    x.BeginAnimation(Canvas.LeftProperty, db);
                    i++;
                }

                aux.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, xx);
                Canvas.SetTop(ellipse, y);
                aux.Children.Add(num);
                Canvas.SetLeft(num, xx + ellipse.Height / 2 - num.FontSize / 2 + 1);
                Canvas.SetTop(num, y + ellipse.Width / 2 - num.FontSize / 2);
            }


        }


        private void dessin(int r)
        {
            int pVictime=0;
            switch (etape)
            {
                case 0:
                    if (tablePages[r].getDisponible() == false)
                    {
                        champTPages.Text = "page non disponible dans la memoire centra (bit de disponibilité a 0)donc defaut de page";
                        etape = 1;
                    }
                    break;
                case 1:
                    if (ram.getNombrepagesLibres() == 0)
                    {

                    }
                    else etape = 2;
                    break;
                case 2:
                    champRam.Text = "il existe des cases libres dans la ram,choix d'une case libre";
                    break;



            }
            if (tablePages[r].getDisponible() == false && ram.getNombrepagesLibres() == 0)
            {
                switch (choixAlgorithme.SelectedIndex)
                {
                    case 0:
                        pVictime = systemExploitation.fileLru.Peek();
                        break;
                    case 1:
                        pVictime = systemExploitation.fifo.Peek();
                        break;
                    case 2:
                        pVictime = systemExploitation.lfu.Find(l => l[1] == systemExploitation.lfu.Min(f => f[1]))[0];
                        break;
                    case 3:
                        affichMatriceAging();
                        pVictime = systemExploitation.aging.FindIndex(s => s == systemExploitation.aging.Min());
                        break;

                }
                champRam.Text = "Swap out de la page" + pVictime.ToString();

            }

            if (tablePages[pVictime].getDirty() == true) champRam.Text += "\n reecriture de la page dans la memoire secondure";
        }

        private void begin(object sender, RoutedEventArgs e)
        {
            champAdressePhysique.Text = "";
            champAux.Text = "";
            champRam.Text = "";
            champTPages.Text = "";
            deroulement.Text = "";

            stop.IsEnabled = true;
            suivant.IsEnabled = true;
            suiteReferences.IsReadOnly = true;
            if (iteration == 0)
            {
                sauvSuite = suiteReferences.Text;
                requete = sauvSuite.Split(' ').ToList<String>();
            }
            if (iteration < suiteReferences.Text.Split(' ').Length)
            {
                sequence.Push(new List<object> { systemExploitation.Clone(), ram.Clone(), diskDur.Clone(), new List<EntreeTablePage>(tablePages.Select(x => x.Clone())), requete, sauvSuite });
                if (requete.Count != 0)
                {
                    String r = requete[iteration];
                    demande.Text = "Demande de l'adresse virtuelle: " + r;
                    try
                    {
                        int tmp = Convert.ToInt32(r);
                        switch (choixAlgorithme.SelectedIndex)
                        {
                            case 3:
                                affichMatriceAging();
                                break;

                        }

                        int sDefaut = systemExploitation.getDefautDePage();
                        int sNbpage = ram.getNombrepagesLibres();
                        SolidColorBrush myBrush = new SolidColorBrush();
                        myBrush.Color = Colors.White;
                        ColorAnimation ba = new ColorAnimation()
                        { Duration = TimeSpan.FromMilliseconds(1000)  };
                        if (tablePages[tmp].getDisponible()==true)
                        {

                            ba.To = Colors.Green;
                        }
                        else
                        {
                            ba.To = Colors.Red;
                        }
                        myBrush.BeginAnimation(SolidColorBrush.ColorProperty, ba);
                        oTable.RowGroups[0].Rows[tmp].Background = myBrush;
                        systemExploitation.gestionRequete(choixAlgorithme.SelectedIndex, tablePages, tmp, ram, diskDur);
                        Line line = new Line()
                        { X1= 150, Y1= (tmp + 1) * 51 + 25, Stroke=new SolidColorBrush(Colors.Black),StrokeThickness=2};
                        DoubleAnimation animation = new DoubleAnimation(260, TimeSpan.FromSeconds(0.5))
                        { From = 150,AutoReverse=true };

                        memVirtuelle.Children.Add(line);
                        line.BeginAnimation(Line.X2Property, animation);
                        animation.From = line.Y1;
                        animation.To   = 200+(51)*tablePages[tmp].getPageCorrespandante();
                        line.BeginAnimation(Line.Y2Property, animation);



                        switch (choixAlgorithme.SelectedIndex)
                        {
                            case 0:
                                if (sDefaut < systemExploitation.getDefautDePage() && sNbpage == 0)
                                {
                                    aux.Children.RemoveAt(0);
                                    aux.Children.RemoveAt(0);
                                }
                                else
                                {
                                    if(sDefaut == systemExploitation.getDefautDePage())
                                    {
                                        for (int i = 1; i < aux.Children.Count; i = i + 2)
                                        {
                                            if (((TextBlock)aux.Children[i]).Text == r)
                                            {
                                                aux.Children.RemoveRange(i - 1, 2);
                                                for (int k = i - 1; k < aux.Children.Count; k++)
                                                {
                                                    DoubleAnimation db = new DoubleAnimation
                                                    {
                                                        To = Canvas.GetLeft(aux.Children[k]) + 10,
                                                        Duration = new Duration(new TimeSpan(1000000))
                                                    };
                                                    aux.Children[k].BeginAnimation(Canvas.LeftProperty, db);
                                                }

                                                break;
                                            }
                                        }

                                    }
                                   
                                }
                                ajoutFileLru();
                                break;

                            case 1:
                                if (sDefaut < systemExploitation.getDefautDePage())
                                {
                                    if (sNbpage == 0)
                                    {
                                        aux.Children.RemoveAt(0);
                                        aux.Children.RemoveAt(0);
                                    }

                                    ajoutFileFifo();
                                }

                                break;

                            case 2:
                                affichLfu();
                                break;
                            case 3:
                                affichMatriceAging();
                                break;
                        }
                        champTPages.Text = "Mise a jour de la table de pages ";
                        afficherRam(ram);
                        champAdressePhysique.Text = "L'adresse physique de la page " + r + " est: " + tablePages[Convert.ToInt32(r)].getPageCorrespandante().ToString();
                        deroulement.Text = "Nombre de defauts de page: " + systemExploitation.getDefautDePage().ToString();
                        majTablePage();
                        iteration++;
                    }
                    catch (FormatException)
                    {
                        msgErreur.Text = "erreur dans la suite entrée.";

                    }

                    precedent.IsEnabled = true;
                    if (iteration == suiteReferences.Text.Split(' ').Length)
                    {
                        lancer.IsEnabled = false;
                        suivant.IsEnabled = false;
                    }
                }
            }

        }



        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                begin(sender, e);
            }
        }

        private void choixAlgorithme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ram = new RamVirtuelle(5);
            systemExploitation = new SystemExploitation();
            diskDur = new DiskDur(5);
            tablePages = new List<EntreeTablePage>();
            majTablePage();
            lancer.IsEnabled = true;
            sauvSuite = suiteReferences.Text;
            suivant.IsEnabled = true;
            iteration = 0;
            systemExploitation.creationTablePages(tablePages, 2*nbPages, ram, diskDur);
            aux.Children.Clear();
            canvas1.Children.Clear();
            afficherRam(ram);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            iteration = 0;
            suiteReferences.Text = "";
            suiteReferences.IsReadOnly = false;
            sequence.Clear();
            stop.IsEnabled = false;
            lancer.IsEnabled = true;
            suivant.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (sequence.Count != 0)
            {
                SystemExploitation tmpOs = new SystemExploitation();
                tmpOs = systemExploitation.Clone();
                List<object> tmp = sequence.Pop();
                systemExploitation = (SystemExploitation)tmp[0];
                ram = (RamVirtuelle)tmp[1];
                diskDur = (DiskDur)tmp[2];
                tablePages = (List<EntreeTablePage>)tmp[3];
                requete = (List<String>)tmp[4];
                sauvSuite = (String)tmp[5];
                afficherRam(ram);
                champAdressePhysique.Text = "";
                champAux.Text = "";
                champRam.Text = "";
                champTPages.Text = "";
                demande.Text = "Demande de l'adresse virtuelle: "  ;
                deroulement.Text = "Nombre de defauts de page: " + systemExploitation.getDefautDePage().ToString();
                switch (choixAlgorithme.SelectedIndex)
                {
                    case 0:
                        suppFileLru();
                        if (tmpOs.fileLru.Count == systemExploitation.fileLru.Count && tmpOs.fileLru.Peek() != systemExploitation.fileLru.Peek())
                       {
                            double y = 0;
                            Ellipse ellipse = new Ellipse()
                            { Height = 30, Width = 30, Fill = Brushes.Cyan };

                            TextBlock num = new TextBlock
                            { Text = tablePages.FindIndex(h => h.getPageCorrespandante() == systemExploitation.fileLru.Peek() && h.getDisponible() == true).ToString(), FontSize = 14 };
                            double x =  (aux.Children.Count / 2) * (ellipse.Width + 10);
                            aux.Children.Insert(0, ellipse);
                            Canvas.SetLeft(ellipse, x);
                            Canvas.SetTop(ellipse, y);
                            aux.Children.Insert(1, num);
                            Canvas.SetLeft(num, x + ellipse.Height / 2 - num.FontSize / 2 + 1);
                            Canvas.SetTop(num, y + ellipse.Width / 2 - num.FontSize / 2);
                        }
                        break;
                    case 1:
                        foreach(EntreeTablePage page in tablePages)
                        {

                        }
                        //suppFileFifo();
                        //if (tmpOs.fifo.Count == systemExploitation.fifo.Count && tmpOs.fifo.Peek() != systemExploitation.fifo.Peek())
                        //{
                        //    double y = 0;
                        //    Ellipse ellipse = new Ellipse()
                        //    { Height = 30, Width = 30, Fill = Brushes.Cyan };

                        //    TextBlock num = new TextBlock
                        //    { Text = tablePages.FindIndex(h => h.getPageCorrespandante() == systemExploitation.fifo.Peek() && h.getDisponible() == true).ToString(), FontSize = 14 };
                        //    double x =  (aux.Children.Count / 2) * (ellipse.Width + 10);
                        //    aux.Children.Insert(0, ellipse);
                        //    Canvas.SetLeft(ellipse, x);
                        //    Canvas.SetTop(ellipse, y);
                        //    aux.Children.Insert(1, num);
                        //    Canvas.SetLeft(num, x + ellipse.Height / 2 - num.FontSize / 2 + 1);
                        //    Canvas.SetTop(num, y + ellipse.Width / 2 - num.FontSize / 2);
                        //}
                        break;
                    case 2:
                        affichLfu();
                        break;
                    case 3:
                        affichMatriceAging();
                        break;
                }
                iteration--;
                lancer.IsEnabled = true;
                suivant.IsEnabled = true;
                majTablePage();
                if (iteration == 0) precedent.IsEnabled = false;
            }
        }



    }
}
