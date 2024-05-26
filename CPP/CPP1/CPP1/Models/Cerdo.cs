using System;
using System.Collections.Generic;

namespace CPP1.Models
{
    public partial class Cerdo
    {
        public Cerdo()
        {
            BitacoraCorrals = new HashSet<BitacoraCorral>();
        }

        public int Idcerdo { get; set; }
        public string Tamaño { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }

        public virtual ICollection<BitacoraCorral> BitacoraCorrals { get; set; }
    }
}
