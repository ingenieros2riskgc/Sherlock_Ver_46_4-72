using System;
using System.Collections.Generic;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class cPropiedades : System.Web.UI.Page
    {
        protected Int64 idUsuario;
        protected Int64 IdUsuario
        {
            get 
            {
                idUsuario = (Int64)Session["idUsuario"];
                return idUsuario;
            }
            set
            {
                idUsuario = value;
                Session["idUsuario"] = idUsuario;
            }
        }

        protected String nombreUsuario;
        protected String NombreUsuario
        {
            get
            {
                nombreUsuario = (String)Session["nombreUsuario"];
                return nombreUsuario;
            }
            set
            {
                nombreUsuario = value;
                Session["nombreUsuario"] = nombreUsuario;
            }
        }

        protected String loginUsuario;
        protected String LoginUsuario
        {
            get
            {
                loginUsuario = (String)Session["loginUsuario"];
                return loginUsuario;
            }
            set
            {
                loginUsuario = value;
                Session["loginUsuario"] = loginUsuario;
            }
        }

        protected String idRol;
        protected String IdRol
        {
            get
            {
                idRol = (String)Session["idRol"];
                return idRol;
            }
            set
            {
                idRol = value;
                Session["idRol"] = idRol;
            }
        }

        
        protected String nombreRol;
        protected String NNombreRol
        {
            get
            {
                nombreRol = (String)Session["nombreRol"];
                return nombreRol;
            }
            set
            {
                nombreRol = value;
                Session["nombreRol"] = nombreRol;
            }
        }

        protected String idJerarquia;
        protected String IdJerarquia
        {
            get
            {
                idJerarquia = (String)Session["idJerarquia"];
                return idJerarquia;
            }
            set
            {
                idJerarquia = value;
                Session["idJerarquia"] = idJerarquia;
            }
        }        
    }
}