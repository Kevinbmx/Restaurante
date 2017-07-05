using DepartamentoDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FoodGood.Departamento
{
    /// <summary>
    /// Summary description for DepartamentoBLL
    /// </summary>
    public class DepartamentoBLL
    {
        public DepartamentoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static List<Departamento> GetDepartamento()
        {
            List<Departamento> theList = new List<Departamento>();
            Departamento theUser = null;
            DepartamentoTableAdapter theAdapter = new DepartamentoTableAdapter();
            try
            {
                DepartamentoDS.DepartamentoDataTable table = theAdapter.GetDepartamento();

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DepartamentoDS.DepartamentoRow row in table.Rows)
                    {
                        theUser = FillDepartamentoRecord(row);
                        theList.Add(theUser);
                    }
                }
            }
            catch (Exception q)
            {
                //log.Error("el error ocurrio mientras obtenia la lista del Area de la base de datos", q);
                return null;
            }
            return theList;
        }

        private static Departamento FillDepartamentoRecord(DepartamentoDS.DepartamentoRow row)
        {
            Departamento theNewRecord = new Departamento(
                row.departamentoId,
                row.nombreDepartamento);
            return theNewRecord;
        }

    }
}