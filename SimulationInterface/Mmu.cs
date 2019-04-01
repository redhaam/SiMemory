using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class Mmu
    {
        public static EntreeTablePage traductionAdresse(List<EntreeTablePage> tablePages, int adresseLogique)
        {
            if(adresseLogique>=tablePages.Count)
            { return null; }
            else
            {
                return tablePages[adresseLogique];
            }
        }

    }
}
