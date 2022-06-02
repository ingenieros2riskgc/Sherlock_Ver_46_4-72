using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.Utilidades
{
    public static class ObtenerCorreoJerarquia
    {
        public static string ObtenerDireccionCorreo(int idJerarquia)
        {
            try
            {
                string direccionCorreo = string.Empty;
                cDataBase cDataBase = new cDataBase();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdJerarquia", SqlDbType = SqlDbType.Int, Value = idJerarquia  },
                };
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarCorreo]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    direccionCorreo = dt.Rows[0][0].ToString();
                }
                return direccionCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}