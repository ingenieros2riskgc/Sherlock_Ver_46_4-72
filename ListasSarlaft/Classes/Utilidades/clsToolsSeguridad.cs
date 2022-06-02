using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.Utilidades
{
    public class clsToolsSeguridad
    {
        public string mtdEncriptarContrasena(string strCadena)
        {
            cEncriptacion cEncrypt = new cEncriptacion();
            string strResult = string.Empty;

            cEncrypt.EncryptionKey = "ab48495fdjk4950dj39405fk";//"ab48495fdjk4950dj39405fk"
            cEncrypt.mtdInClearText = strCadena;
            cEncrypt.mtdEncrypt();
            strResult = (cEncrypt.CryptedText).Replace("'", "''");


            return strResult;
        }
    }
}