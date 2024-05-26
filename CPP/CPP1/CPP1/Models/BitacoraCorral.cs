using System;
using System.Collections.Generic;

namespace CPP1.Models
{
    public partial class BitacoraCorral
    {
        public int Idbitacora { get; set; }
        public int Idcerdo { get; set; }
        public int Idcorral { get; set; }
        public int IdtipoAlimento { get; set; }
        public double Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual Cerdo oCerdo{ get; set; } = null!;
        public virtual Corrale oCorral { get; set; } = null!;
        public virtual TipoAlimentacion oAlimento{ get; set; } = null!;
    }
}
