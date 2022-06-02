using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlPropiedadCPdal
    {
        public bool mtdInsertarControlPropiedad(clsControlPropiedadCP objCrlPropiedad, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            try
            { 
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlPropiedadClienteProveedor] ([Descripcion],[Caracteristicas],[ProveedorCliente],[FechaIngreso],[FechaSalida],[Observaciones],[FechaRegistro],[IdUsuario],[Nombre])" +
                            "VALUES('{0}','{1}','{2}',CONVERT(date,'{3}',126),CONVERT(date,'{4}',126),'{5}',GETDATE(),{7},'{8}') ",
                            objCrlPropiedad.strDescripcion, objCrlPropiedad.strCaracteristicas, objCrlPropiedad.strProveedorCliente, objCrlPropiedad.dtFechaIngreso, objCrlPropiedad.dtFechaSalida,
                            objCrlPropiedad.strObservaciones, "GETDATE()", objCrlPropiedad.intIdUsuario, objCrlPropiedad.strNombre);
                
                cDatabase.conectar();

                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear al insertar el control de Propiedad del {1}. [{0}]", ex.Message, objCrlPropiedad.strProveedorCliente);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlPropiedad(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT CPCP.[IdControlPropiedadClienteProveeedor], CPCP.[Descripcion], CPCP.[Caracteristicas], CPCP.[ProveedorCliente], CPCP.[FechaIngreso]" +
                ", CPCP.[FechaSalida], CPCP.[Observaciones], CPCP.[FechaRegistro], CPCP.[IdUsuario], US.Usuario, CPCP.[Nombre]"
                    + " FROM [Procesos].[tblControlPropiedadClienteProveedor] AS CPCP"
                    + " inner join Listas.Usuarios as US on US.IdUsuario = CPCP.IdUsuario"
                    );

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el control de propiedad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateControlPropiedad(ref clsControlPropiedadCP objPrograma, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblControlPropiedadClienteProveedor]  SET [Descripcion] = '{0}',[Caracteristicas] = '{1}', " +
                    "[ProveedorCliente] = '{2}',[FechaIngreso] = '{3}',[FechaSalida] = '{4}',[Observaciones] = '{5}',[FechaRegistro] = '{6}',[IdUsuario] = {7}, [Nombre] = '{9}'"+
                    " WHERE IdControlPropiedadClienteProveeedor = {8} ",
                    objPrograma.strDescripcion, objPrograma.strCaracteristicas, objPrograma.strProveedorCliente, objPrograma.dtFechaIngreso, objPrograma.dtFechaSalida
                    , objPrograma.strObservaciones, objPrograma.dtFechaRegistro, objPrograma.intIdUsuario, objPrograma.intIdCrlPropiedad, objPrograma.strNombre);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el control de propiedad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}