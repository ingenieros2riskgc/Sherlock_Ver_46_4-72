using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;
using ListasSarlaft.Classes;

namespace ListasSarlaft.Formularios.WebService
{
    /// <summary>
    /// Descripción breve de AutoCompletar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompletar : System.Web.Services.WebService
    {
        cParametrizacionRiesgos cParamRiesgos = new cParametrizacionRiesgos();

        [WebMethod]
        [ScriptMethod()]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetCausas(string prefixText, int count)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParamRiesgos.mtdInfoCausas();

            // Get the Products From Data Source. Change this method to use Database
            List<string> LstCausas = new List<string>();
            string dbValues = string.Empty;

            foreach (DataRow row in dtInfo.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = mtdQuitarLetrasEspeciales(row["NombreCausas"].ToString());//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(mtdQuitarLetrasEspeciales(row["NombreCausas"].ToString()), row["IdCausas"].ToString());
                dbValues = dbValues.ToLower();

                LstCausas.Add(dbValues);
            }

            // Find All Matching Products
            var list = from p in LstCausas
                       where p.Contains(mtdQuitarLetrasEspeciales(prefixText))
                       select p;

            //Convert to Array as We need to return Array
            string[] prefixTextArray = list.ToArray<string>();

            //Return Selected Products
            return prefixTextArray;
        }

        [WebMethod]
        [ScriptMethod()]
        public string[] GetConsecuencias(string prefixText, int count)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cParamRiesgos.mtdInfoConsecuencia();

            // Get the Products From Data Source. Change this method to use Database
            List<string> LstConsecuencias = new List<string>();
            string dbValues = string.Empty;

            foreach (DataRow row in dtInfo.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = mtdQuitarLetrasEspeciales(row["NombreConsecuencia"].ToString());
                dbValues = dbValues.ToLower();

                LstConsecuencias.Add(dbValues);
            }

            // Find All Matching Products
            var list = from p in LstConsecuencias
                       where p.Contains(mtdQuitarLetrasEspeciales(prefixText))
                       select p;

            //Convert to Array as We need to return Array
            string[] prefixTextArray = list.ToArray<string>();

            //Return Selected Products
            return prefixTextArray;
        }

        private string mtdQuitarLetrasEspeciales(string strCadenaIn)
        {
            string strLetraEspecial = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ",
                strLetraNormal = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";
            string strCadenaOut = string.Empty;

            for (int v = 0; v < strLetraNormal.Length; v++)
            {
                string i = strLetraEspecial.Substring(v, 1);
                string j = strLetraNormal.Substring(v, 1);
                strCadenaOut = strCadenaIn.Replace(i, j);
            }

            return strCadenaOut;
        }
        
    }
}
