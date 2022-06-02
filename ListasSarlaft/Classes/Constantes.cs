
using System;
using System.Globalization;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class Constantes
    {
        /// <summary>
        /// para decimales 1.82 1,82 
        /// </summary>
        private static CultureInfo cultura = CultureInfo.CreateSpecificCulture("en-US");
        public static CultureInfo CulturaENUS
        {
            get { return cultura; }
        }

        /// <summary>
        /// cultura francesa para el servidor de produccion
        /// </summary>
        private static CultureInfo culturaFR = CultureInfo.CreateSpecificCulture("fr-FR");
        public static CultureInfo CulturaFR
        {
            get { return culturaFR; }
        }

        public static string NOMBRE_CARGAS = "Carga"; //prefijo nombre archivo generar
        public static string EXCEL_EXT_2010 = ".xlsx"; //Excel2010
        public static string EXCEL_CON_2010 = "Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0;HDR=YES'"; //oledb conexion Excel2010
        public static string TXT = ".txt"; //Excel2010
        public static char TAB = '\t';
        public static string FECHA_INICIO = "01/01/2011"; 
        public static string YMD = "yyyyMMdd"; //para SP servidor sql
        public static string DMY = "dd/MM/yyyy";
        public static int MCE = 1048576; //Maximas columnas admitidas en excel 2010       
        public static int MAX_CARGA_ORA = 50000;  //para consultas oracle y que cada reg puede tener carios movimientos 
    }
}