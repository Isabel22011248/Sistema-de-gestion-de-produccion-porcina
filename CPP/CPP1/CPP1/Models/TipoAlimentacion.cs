using System;
using System.Collections.Generic;

namespace CPP1.Models
{
    public partial class TipoAlimentacion
    {
        public TipoAlimentacion()
        {
            BitacoraCorrals = new HashSet<BitacoraCorral>();
        }

        public int IdtipoAlimento { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<BitacoraCorral> BitacoraCorrals { get; set; }
    }
}
