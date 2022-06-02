using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace clsLogica
{
    public class clsError
    {
        public void errorMessage(string strPath, string message)
        {
            StreamWriter sWErrorMess = new StreamWriter(strPath, true);

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
