using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace ListasSarlaft.Classes
{
    public static class Procs
    {
        /// <summary>
        /// genero log en archivos planos la ruta se toma del tag web.config "LOG_ERRORES"
        /// </summary>
        /// <param name="ex">excepción para log</param>
        public static void LogErrores(Exception ex)
        {
            try
            {
                //if (ex.Source == Constantes.EXCEPTION_SINLOG)
                //{
                //    return; //mensaje de usuario
                //}
                string ruta = System.Configuration.ConfigurationManager.AppSettings["LOG_ERRORES"].ToString();
                string archivo = @ruta + "Deco2017_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                StringBuilder err = new StringBuilder();
                err.AppendLine(DateTime.Now.ToString());
                err.AppendLine(ex.Message);
                if (ex.InnerException != null)
                {
                    err.AppendLine(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        //error interno por ejemplo en triggers
                        err.AppendLine(ex.InnerException.InnerException.Message);
                    }
                }
                if (ex.StackTrace != null)
                {
                    err.AppendLine(ex.StackTrace.ToString());
                }
                err.AppendLine("------------");

                StreamWriter writer = null;


                int intentos = 0;
                while (true)
                {
                    try
                    {
                        //si otro proceso está escribiendo en este instante
                        //se intenta hasta que pueda abrir el log 
                        writer = File.AppendText(archivo);
                        writer.WriteLine(err.ToString());
                        writer.Flush();
                        writer.Close();

                        break;
                    }
                    catch (IOException ioex)
                    {
                        intentos++;
                        if (intentos > 3)
                        {
                            throw new Exception(ioex.Message);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception exep)
            {
                throw new Exception("Error en LogErrores: " + exep.Message + " Error original\n" + ex.Message);
            }
        }

        /// <summary>
        /// creeo parámetro con valor recibido en paramValue
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramType"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static SqlParameter CrearParametro(string paramName, SqlDbType paramType, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.SqlDbType = paramType;
            param.Value = paramValue;
            switch (paramType)
            {
                case SqlDbType.Decimal:
                    param.Value = Convert.ToDecimal(param.Value.ToString().Replace(',', '.'), Constantes.CulturaENUS.NumberFormat);
                    break;
                case SqlDbType.Float:
                    param.Value = Convert.ToDouble(param.Value.ToString().Replace(',', '.'), Constantes.CulturaENUS.NumberFormat);
                    break;
            }
            param.Direction = ParameterDirection.Input;
            param.ParameterName = paramName;
            return param;
        }

        /// <summary>
        /// devuelvo ruta completa + nuevo nombre archivo verifico que no exista en la ruta en el servidor
        /// <param name="path_archivo">ruta completa de web.config</param>
        /// <param name="archivo">nombre</param>
        /// <param name="extension">extension archivo debe venir con el punto</param>
        /// </summary>
        /// <returns>nuevo nombre archivo con ruta completa</returns>
        public static string NombreArchivo(string path_archivo, string nombre, string extension)
        {
            string nuevoArchivo = string.Empty;
            string archlock = string.Empty;
            string file = string.Empty;
            Random ran = new Random();
            //busco que no se repita el archivo
            while (true)
            {
                Int32 n = ran.Next(100000);
                //la extensi{on viene conpunto (.) porque en algunos métodos se usa System.IO.Path se usa para obtener la ext de un archivo 
                nuevoArchivo = nombre + "-" + n.ToString() + extension;
                file = path_archivo + nuevoArchivo;
                if (!System.IO.File.Exists(@file))
                {
                    break;
                }
            }
            return nuevoArchivo;
        }
    }
}