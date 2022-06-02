using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.BLL
{
    public class CargueMasivo
    {
       public DataTable LoadRiesgosControlesCausas(DataTable data)
        {
            DataTable dtDatos = new DataTable();
            dtDatos.Columns.Add("Id Riesgo vs Control");
            dtDatos.Columns.Add("Código");
            dtDatos.Columns.Add("Nombre Riesgo");
            dtDatos.Columns.Add("Código");
            dtDatos.Columns.Add("Nombre Control");
            dtDatos.Columns.Add("Causas");

            foreach(DataRow fila in data.Rows)
            {

            }
            return dtDatos;
        }
    }
}