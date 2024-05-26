using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPP1.Models.ViewModels
{
    public class CorralVM
    {
        public BitacoraCorral oBitacora { get; set; }

        public List<SelectListItem> oListaCerdo { get; set; }
        public List<SelectListItem> oListaAlimento { get; set; }

        public List<SelectListItem> oListaCorral { get; set; }

        public List<SelectListItem> oListaTamaño { get; set; }


        public CorralVM()

        {
            oBitacora=new BitacoraCorral();
            oListaCerdo=new List<SelectListItem>(); 
            oListaAlimento=new List<SelectListItem>();  
            oListaCorral=new List<SelectListItem>();    
            oListaTamaño=new List<SelectListItem>();    
    }
    }

    

}
