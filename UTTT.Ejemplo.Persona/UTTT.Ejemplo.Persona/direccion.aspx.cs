using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using UTTT.Ejemplo.Persona.Data.Entity;
using UTTT.Ejemplo.Persona.Control;


namespace UTTT.Ejemplo.Persona
{
    public partial class direccion : System.Web.UI.Page
    {
        SessionManager session = new SessionManager();
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.session = (SessionManager)this.Session["SessionManager"];
            if (!Page.IsPostBack)
            {                
                this.txtPersona.Text = this.session.StrNombrePersona;
                this.setGridView();
            }
            
        }

        private void setGridView()
        {
            try
            {
                CtrlDireccion ctrlPersona = new CtrlDireccion();
                List<UTTT.Ejemplo.Persona.Data.Entity.Direccion> lista = new List<Data.Entity.Direccion>();
                List<Object> listaObject = new List<object>();
                Direccion dirTemp = new Direccion();
                dirTemp.IdPersona = this.session.IdPersona;
                object obj = dirTemp;
                listaObject = ctrlPersona.consultarLista(obj);
                for (int i = 0; i < listaObject.Count; i++)
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion temp = new Data.Entity.Direccion();
                    temp = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)listaObject[i];

                    lista.Add(temp);
                }
                this.dgvDireccion.DataSource = lista;
                this.dgvDireccion.DataBind();
            }
            catch (Exception _e)
            {

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.session.AccionAgregar)
                {
                    CtrlDireccion ctrlDireccion = new CtrlDireccion();
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = new Data.Entity.Direccion();
                    direccion.IdPersona = this.session.IdPersona;
                    direccion.StrCalle = this.txtCalle.Text.Trim();
                    direccion.StrColonia = this.txtColonia.Text.Trim();
                    direccion.StrNumero = this.txtNumero.Text.Trim();
                    object objeto = direccion;
                    bool resultado = ctrlDireccion.insertar(objeto);
                    if (resultado)
                    {
                        this.limpiar();
                        this.setGridView();
                        Response.Write("<script type=\"text/javascript\">alert('El registro se guardo correctamente');</script>");
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('El registro no se guardo correctamente');</script>");
                    }
                }
                if (this.session.AccionEditar)
                {
                    CtrlDireccion ctrlDireccion = new CtrlDireccion();
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = new Data.Entity.Direccion();
                    direccion.Id = this.session.IdManager;
                    direccion.StrCalle = this.txtCalle.Text.Trim();
                    direccion.StrColonia = this.txtColonia.Text.Trim();
                    direccion.StrNumero = this.txtNumero.Text.Trim();
                    object objeto = direccion;
                    bool resultado = ctrlDireccion.actualizar(objeto);
                    if (resultado)
                    {
                        this.limpiar();
                        this.setGridView();
                        Response.Write("<script type=\"text/javascript\">alert('El registro se actualizó correctamente');</script>");

                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('El registro no se actualizó correctamente');</script>");
                    }  
                }

                this.session.AccionEditar = false;
                this.session.AccionAgregar = true;
            }
            catch (Exception _e)
            {

            }
        }

        private void limpiar()
        {
            this.txtCalle.Text = String.Empty;
            this.txtColonia.Text = String.Empty;
            this.txtNumero.Text = String.Empty;           
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void dgvDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = this.dgvDireccion.Rows[index];
                  ListItem item = new ListItem();
                    item.Text = Server.HtmlDecode(row.Cells[6].Text);
                this.session.IdManager = int.Parse(item.Text);
                if (e.CommandName.Equals("Editar"))
                {                    
                    CtrlDireccion ctrlDireccion = new CtrlDireccion();
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = new Data.Entity.Direccion();
                    direccion.Id = this.session.IdManager;
                    object objeto = direccion;
                    Object resultado = ctrlDireccion.consultarItem(objeto);
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion personaDireccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)resultado;
                    if (personaDireccion.Id > 0)
                    {
                        this.txtCalle.Text = personaDireccion.StrCalle;
                        this.txtColonia.Text = personaDireccion.StrColonia;
                        this.txtNumero.Text = personaDireccion.StrNumero;
                        this.session.AccionAgregar = false;
                        this.session.AccionEditar = true;                       
                    }                
                   
                }
                if (e.CommandName.Equals("Eliminar"))
                {
                    CtrlDireccion ctrlDireccion = new CtrlDireccion();
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = new Data.Entity.Direccion();
                    direccion.Id = this.session.IdManager;
                    object objeto = direccion;
                    bool resultado = ctrlDireccion.eliminar(objeto);
                    if (resultado)
                    {
                        this.limpiar();
                        this.setGridView();
                        Response.Write("<script type=\"text/javascript\">alert('El registro se elimino correctamente');</script>");
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('El registro no se elimino correctamente');</script>");
                    }  
                }
            }
            catch (Exception _e)
            { 
            
            }
        }

    }
}