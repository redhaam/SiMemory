using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class RamVirtuelle : ICloneable
    {
        public RamVirtuelle Clone()
        {
            RamVirtuelle tmp = (RamVirtuelle)this.MemberwiseClone();
            tmp.listPages = new List<Part> (this.listPages.Select(x => x.Clone()));
            return tmp;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        private int nombrePages;
        private int taillePage;
        private int nombrePagesLibres;
        public List<Part> listPages=new List<Part>();
        
        public RamVirtuelle(int cap)
        { nombrePages = cap;
            taillePage = 4;
            nombrePagesLibres = cap;

            for (int i = 0; i < cap; i++) listPages.Add(new Part());
        }

        public int getNombrePages() { return nombrePages; }
        public void setNombrePages(int nbpages) { nombrePages = nbpages; }
        public int getNombrepagesLibres() { return nombrePagesLibres; }
        public void setNombrePagesLibres(int nbpagesLibres) { nombrePagesLibres = nbpagesLibres; }

    }
}
