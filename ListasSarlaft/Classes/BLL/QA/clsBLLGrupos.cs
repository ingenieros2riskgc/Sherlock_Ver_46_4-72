using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLGrupos
    {
        /// <summary>
        /// Realiza la consulta de las frecuencias de Medición
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOGrupos> mtdConsultarEstado(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOGrupos> lstEstado = new List<clsDTOGrupos>();
            clsDALEstado cDtEstado = new clsDALEstado();
            clsDTOGrupos objEstado = new clsDTOGrupos();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtEstado.mtdConsultaDeEstado(ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objEstado = new clsDTOGrupos();
                        objEstado.IdGrupoSoporte = Convert.ToInt32(dr["idGrupoSoporte"].ToString().Trim());
                        objEstado.NombreGruposoporte = dr["NombreGrupoSoporte"].ToString().Trim();
                        lstEstado.Add(objEstado);
                    }
                }
                else
                {
                    lstEstado = null;
                    strErrMsg = "Error con la consulta de grupos";
                }
            }
            else
                objEstado = null;

            return lstEstado;
        }


        public List<clsDTOIntegrantes> mtdConsultarIntegrantes(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOIntegrantes> lstEstado = new List<clsDTOIntegrantes>();
            clsDALEstado cDtEstado = new clsDALEstado();
            clsDTOIntegrantes objIntegrantes = new clsDTOIntegrantes();
            bool booResutl = false;
            #endregion Vars

            booResutl = cDtEstado.mtdConsultaDeIntegrantes(ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objIntegrantes = new clsDTOIntegrantes();
                        objIntegrantes.IdUsuarioSoporte = Convert.ToInt32(dr["IdUsuarioSoporte"].ToString().Trim());
                        objIntegrantes.NombreUsuarioSoporte = dr["NombreUsuarioSoporte"].ToString().Trim();
                        lstEstado.Add(objIntegrantes);
                    }
                }
                else
                {
                    lstEstado = null;
                    strErrMsg = "Error con la consulta de grupos";
                }
            }
            else
                objIntegrantes = null;

            return lstEstado;
        }

    }
}