using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ListasSarlaft
{
    public class Global : System.Web.HttpApplication
    {
        static List<object> _lstSenalVar = new List<object>();
        static List<object> _lstCalidad = new List<object>();
        static bool _booIsOkFormula = false;
        static bool _booIsNew = false;
        static bool _booIsGlobal = false;
        static int _Posicion = 0;
        
        public static int intPosicion
        {
            get { return _Posicion; }
            set { _Posicion = value; }
    }
        public static List<object> LstSenalVar
        {
            get { return _lstSenalVar; }
            set { _lstSenalVar = value; }
        }

        public static List<object> LstCalidad
        {
            get { return _lstCalidad; }
            set { _lstCalidad = value; }
        }

        public static bool BooIsOkFormula
        {
            get { return _booIsOkFormula; }
            set { _booIsOkFormula = value; }
        }

        public static bool BooIsNew
        {
            get { return _booIsNew; }
            set { _booIsNew = value; }
        }

        public static bool BooIsGlobal
        {
            get { return _booIsGlobal; }
            set { _booIsGlobal = value; }
        }

        public static void mtdLimpiarLista()
        {
            LstSenalVar = new List<object>();
            BooIsOkFormula = false;
            BooIsGlobal = false;
        }

        public static void mtdLimpiarListaCalidad()
        {
            intPosicion = 0;
            LstCalidad = new List<object>();
            BooIsOkFormula = false;
        }

        public static void mtdIncrementaPosicion()
        {
            intPosicion++;
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            
            
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}