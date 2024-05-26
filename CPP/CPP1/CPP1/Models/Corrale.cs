using System;
using System.Collections.Generic;

namespace CPP1.Models
{
    public partial class Corrale
    {
        public Corrale()
        {
            BitacoraCorrals = new HashSet<BitacoraCorral>();
        }

        public int Idcorral { get; set; }
        public string Nombre { get; set; } = null!;
        public int Capacidad { get; set; }

        public virtual ICollection<BitacoraCorral> BitacoraCorrals { get; set; }
    }
}
