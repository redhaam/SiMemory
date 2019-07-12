using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class EntreeTablePage : ICloneable
    {

        private int pageCorrespandante;
        private Boolean disponible;
        private Boolean dirty;

        public EntreeTablePage()
        {
            dirty = false;
            disponible = false;
        }

        public int getPageCorrespandante() { return pageCorrespandante; }
        public void setPageCorrespandante(int numpage) { pageCorrespandante = numpage; }
        public Boolean getDisponible() { return disponible; }
        public void setDisponible(Boolean dispo) { disponible = dispo; }
        public Boolean getDirty() { return dirty; }
        public void setDirty(Boolean dirty) { this.dirty = dirty; }
        public EntreeTablePage Clone()
        {
            return (EntreeTablePage)this.MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

    }
    
}
