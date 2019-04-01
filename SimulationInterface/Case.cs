using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationInterface
{
    class Case
    {
        private int derniereModification;
        private int numeroCase;

        public Case(int numeroCase)
        {
            this.numeroCase = numeroCase;
        }
        public int getNumeroCase() { return numeroCase; }
        public void setNumeroCase(int numeroCase) { this.numeroCase = numeroCase; }
        public int getDerniereModification() { return derniereModification; }
        public void setDerniereModification(int derniereModif) { derniereModification = derniereModif; }


    }
}
