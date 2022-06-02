using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class VerSegmentacion : System.Web.UI.UserControl
    {
        cSegmentacion cSegmentacion = new cSegmentacion();
        cCuenta cCuenta = new cCuenta();
		String IdFormulario = "6002";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadTreeSegmentacion();                
            }
        }

        private void loadTreeSegmentacion()
        {
            DataTable dtInfoFactorRiesgo = new DataTable();
            TreeNode raizSegmentacion = new TreeNode();
            raizSegmentacion.Text = "Segmentación";
            raizSegmentacion.ImageUrl = "~/Imagenes/Icons/mapaweb.png";
            dtInfoFactorRiesgo = cSegmentacion.loadInfoFactorRiesgo();
            if (dtInfoFactorRiesgo.Rows.Count > 0)
            {
                for (int rowFactorRiesgo = 0; rowFactorRiesgo < dtInfoFactorRiesgo.Rows.Count; rowFactorRiesgo++)
                {
                    DataTable dtInfoSegmento = new DataTable();
                    TreeNode factorRiesgo = new TreeNode();
                    factorRiesgo.Text = "Factor de riesgo SARLAFT: " + dtInfoFactorRiesgo.Rows[rowFactorRiesgo]["Nombre"].ToString().Trim();
                    factorRiesgo.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                    factorRiesgo.Expanded = true;
                    dtInfoSegmento = cSegmentacion.loadInfoSegmento(dtInfoFactorRiesgo.Rows[rowFactorRiesgo]["IdFactorRiesgo"].ToString().Trim());
                    if (dtInfoSegmento.Rows.Count > 0)
                    {
                        for (int rowSegmento = 0; rowSegmento < dtInfoSegmento.Rows.Count; rowSegmento++)
                        {
                            DataTable dtInfotipoSegmento = new DataTable();
                            TreeNode segmento = new TreeNode();
                            segmento.Text = "Segmentación de factores de riesgo: " + dtInfoSegmento.Rows[rowSegmento]["Nombre"].ToString().Trim();
                            segmento.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                            segmento.Expanded = true;
                            dtInfotipoSegmento = cSegmentacion.loadInfoTipoSegmento(dtInfoSegmento.Rows[rowSegmento]["IdSegmento"].ToString().Trim());
                            if (dtInfotipoSegmento.Rows.Count > 0)
                            {
                                for (int rowTipoSegmento = 0; rowTipoSegmento < dtInfotipoSegmento.Rows.Count; rowTipoSegmento++)
                                {
                                    DataTable dtInfoAtributo = new DataTable();
                                    TreeNode tipoSegmento = new TreeNode();
                                    tipoSegmento.Text = "Tipo segmento: " + dtInfotipoSegmento.Rows[rowTipoSegmento]["Nombre"].ToString().Trim();
                                    tipoSegmento.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                                    tipoSegmento.Expanded = true;
                                    dtInfoAtributo = cSegmentacion.loadInfoAtributo(dtInfotipoSegmento.Rows[rowTipoSegmento]["IdTipoSegmento"].ToString().Trim());
                                    if (dtInfoAtributo.Rows.Count > 0)
                                    {
                                        for (int rowAtributo = 0; rowAtributo < dtInfoAtributo.Rows.Count; rowAtributo++)
                                        {
                                            DataTable dtPerfilSegmento = new DataTable();
                                            TreeNode atributo = new TreeNode();
                                            atributo.Text = "Atributo: " + dtInfoAtributo.Rows[rowAtributo]["Nombre"].ToString().Trim();
                                            atributo.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                                            atributo.Expanded = true;
                                            dtPerfilSegmento = cSegmentacion.loadInfoPerfilSegmento(dtInfoAtributo.Rows[rowAtributo]["IdAtributo"].ToString().Trim());
                                            if (dtPerfilSegmento.Rows.Count > 0)
                                            {
                                                for (int rowPerfilSegmento = 0; rowPerfilSegmento < dtPerfilSegmento.Rows.Count; rowPerfilSegmento++)
                                                {
                                                    DataTable dtIndicador = new DataTable();
                                                    TreeNode perfilSegmento = new TreeNode();
                                                    perfilSegmento.Text = "Perfil de segmento: " + dtPerfilSegmento.Rows[rowPerfilSegmento]["Nombre"].ToString().Trim();
                                                    perfilSegmento.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                                                    perfilSegmento.Expanded = true;
                                                    dtIndicador = cSegmentacion.loadInfoIndicador(dtPerfilSegmento.Rows[rowPerfilSegmento]["IdPerfil"].ToString().Trim());
                                                    if(dtIndicador.Rows.Count > 0)
                                                    {
                                                        for (int rowIndicador = 0; rowIndicador < dtIndicador.Rows.Count; rowIndicador++)
                                                        {
                                                            TreeNode indicador = new TreeNode();
                                                            indicador.Text = "Indicador: " + dtIndicador.Rows[rowIndicador]["Nombre"].ToString().Trim();
                                                            indicador.ImageUrl = "~/Imagenes/Icons/segmentacion.png";
                                                            indicador.Expanded = true;
                                                            perfilSegmento.ChildNodes.Add(indicador);
                                                        }
                                                    }
                                                    atributo.ChildNodes.Add(perfilSegmento);
                                                }
                                            }
                                            tipoSegmento.ChildNodes.Add(atributo);
                                        }
                                    }
                                    segmento.ChildNodes.Add(tipoSegmento);
                                }                                
                            }
                            factorRiesgo.ChildNodes.Add(segmento);
                        }                        
                    }                    
                    raizSegmentacion.ChildNodes.Add(factorRiesgo);
                }                
            }
            TreeView1.Nodes.Add(raizSegmentacion);
        }
    }
}