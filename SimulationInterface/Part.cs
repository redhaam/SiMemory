using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class Part : ICloneable
    {
        private Boolean vide;
        private int derniereModification;
        public Part Clone()
        {
            return (Part)this.MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        public Part()
        { vide = true; }

        public Boolean getVide() { return vide; }
        public void setVide(Boolean vide) { this.vide = vide; }
        public int getDerniereModification() { return derniereModification; }
        public void setDerniereModification(int derniereModif) { derniereModification = derniereModif; }

    }
}
