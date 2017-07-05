using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGood.Departamento
{
    /// <summary>
    /// Summary description for Departamento
    /// </summary>
    public class Departamento
    {
        public int DepartamentoId { get; set; }
        public string NombreDepartamento { get; set; }

        public Departamento()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Departamento(int departamentoId, string nombreDepartamento)
        {
            DepartamentoId = departamentoId;
            NombreDepartamento = nombreDepartamento;
        }
    }
}