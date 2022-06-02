using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
//using ListasSarlaft.App_Code;

namespace ListasSarlaft.Modelo
{
    public class UsuarioLinQ
    {
        LinQ_SherlockDataContext usuario = new LinQ_SherlockDataContext();

        internal DataTable BuscarUsuarios()
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                //ds.Locale = 

                // query linq para obtener la lista de maestros
                return from m in usuario.Parametrizacion_JerarquiaOrganizacional
                       select m;

            }
            catch (Exception ex)
            {
                throw ex;
            } // end try
        } // end BuscarMaestroById
    }
    
}