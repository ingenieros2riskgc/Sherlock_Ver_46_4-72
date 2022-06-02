using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLRoles
    {
        /// <summary>
        /// Realiza la actualizacion de los campos editados
        /// </summary>
        /// <param>Informacion del Rol</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarRoles(String NombreRol, String IdRol)
        {
            bool booResult = false;
            clsDALLUpdRoles cDtroles = new clsDALLUpdRoles();

            booResult = cDtroles.mtActualizarRol(NombreRol, IdRol);

            return booResult;
        }
    }
}