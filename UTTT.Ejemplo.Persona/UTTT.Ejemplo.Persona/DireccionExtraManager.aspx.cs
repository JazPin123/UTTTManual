
#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class DireccionExtraManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Direccion baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idDireccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idDireccion = this.session.Parametros["idDireccion"] != null ?
                    int.Parse(this.session.Parametros["idDireccion"].ToString()) : 0;

                if (this.idDireccion == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Direccion();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Direccion>().First(c => c.id == this.idDireccion);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idDireccion == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtColonia.Text = this.baseEntity.strColonia;
                        this.txtNumero.Text = this.baseEntity.strNumero;
                        this.txtCalle.Text = this.baseEntity.strCalle;
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/DireccionManager.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Direccion direccion = new Linq.Data.Entity.Direccion();
                if (this.idDireccion == 0)
                {
                    direccion.idPersona = this.idPersona;
                    direccion.strCalle = this.txtCalle.Text.Trim();
                    direccion.strColonia = this.txtColonia.Text.Trim();
                    direccion.strNumero = this.txtNumero.Text.Trim();
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Direccion>().InsertOnSubmit(direccion);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");                  
                    this.Response.Redirect("~/DireccionManager.aspx");
                }
                if (this.idDireccion > 0)
                {
                    direccion = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Direccion>().First(c => c.id == this.idDireccion);
                    direccion.strCalle = this.txtCalle.Text.Trim();
                    direccion.strColonia = this.txtColonia.Text.Trim();
                    direccion.strNumero = this.txtNumero.Text.Trim();
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");                   
                    this.Server.Transfer("~/DireccionManager.aspx");

                }
            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/DireccionManager.aspx");               
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion
    }
}