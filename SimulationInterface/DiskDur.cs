using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class DiskDur : ICloneable
    {
        public DiskDur Clone()
        {
            return (DiskDur)this.MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        private int nombreCases;
        public List<int> tableAuxiliere = new List<int>();
        public List<Case> cases        =new List<Case>();

    public DiskDur(int tailleTable)
        { nombreCases = 0;
            for (int i = 0; i < tailleTable; i++) tableAuxiliere.Add(-1);
        }
    public int getNombreCases() { return nombreCases; }
    public void setNombreCases(int nbcases)
        { nombreCases = nbcases; }


    }
}
