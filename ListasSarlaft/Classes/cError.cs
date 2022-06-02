using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace ListasSarlaft.Classes
{
    public class cError : System.Web.UI.Page
    {
        public void errorMessage(string message)
        {
            StreamWriter sWErrorMess = new StreamWriter(Server.MapPath("~/Archivos/Error/Error.txt"), true);

            try
            {
                sWErrorMess.WriteLine(message);
                sWErrorMess.WriteLine(Convert.ToString(DateTime.Now));
                sWErrorMess.Flush();
                sWErrorMess.Close();
            }
            catch
            {
                sWErrorMess.Flush();
                sWErrorMess.Close();
            }
        }
    }
}