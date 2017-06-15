using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Dosificacion
{
    /// <summary>
    /// Summary description for Dosificacion
    /// </summary>
    public class Dosificacion
    {
        public int DosificacionId { get; set; }
        public int Desde { get; set; }
        public int Hasta { get; set; }
        public string NumeroAutorizacion { get; set; }
        public string LlaveDosificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public Int32 FacturaActual { get; set; }
        public Int32 Nit { get; set; }
        public string Glosa { get; set; }
        public int CboEstado { get; set; }


        public string FechaInicioForDisplay
        {
            get { return FechaInicio.ToShortDateString(); }
        }

        public string FechaFinalForDisplay
        {
            get { return FechaFinal.ToShortDateString(); }
        }



        public Dosificacion()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public Dosificacion(int dosificacionId, int desde, int hasta, string numeroAutorizacion,
            string llaveDosificacion, DateTime fechaInicio, DateTime fechaFinal, int facturaActual,
            int nit, string glosa, int cboEstado)
        {
            DosificacionId = dosificacionId;
            Desde = desde;
            Hasta = hasta;
            NumeroAutorizacion = numeroAutorizacion;
            LlaveDosificacion = llaveDosificacion;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            FacturaActual = facturaActual;
            Nit = nit;
            Glosa = glosa;
            CboEstado = cboEstado;
        }




    }
}