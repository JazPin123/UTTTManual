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
    public partial class _Default : System.Web.UI.Page
    {
        SessionManager session = new SessionManager();
        String pantallaDireccion = "~/direccion.aspx";
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.setGridView();
                CtrlSexo ctrlSexo = new CtrlSexo();
                List<Object> listaObject = ctrlSexo.consultarLista(null);
                List<UTTT.Ejemplo.Persona.Data.Entity.CatSexo> listaSexo = new List<CatSexo>();
                for (int i = 0; i < listaObject.Count; i++)
                {
                    CatSexo tempCatSexo = (CatSexo)listaObject[i];
                    listaSexo.Add(tempCatSexo);

                }

                CatSexo catSexoInicio = new CatSexo();
                catSexoInicio.Id = -1;
                catSexoInicio.StrValor = "";
                listaSexo.Insert(0, catSexoInicio);
                this.dblSexo.DataSource = listaSexo;
                this.dblSexo.DataValueField = "Id";
                // Hace el enlace del campo au_fname para el texto
                this.dblSexo.DataTextField = "StrValor";
                // Llena el DropDownList con los datos de la fuente de datos
                this.dblSexo.DataBind();
            }
        }

        private void setGridView()
        {
            try
            {
                CtrlPersona ctrlPersona = new CtrlPersona();
                List<UTTT.Ejemplo.Persona.Data.Entity.Persona> lista = new List<Data.Entity.Persona>();
                List<Object> listaObject = new List<object>();
                listaObject = ctrlPersona.consultarLista(null);
                for (int i = 0; i < listaObject.Count; i++)
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Persona temp = new Data.Entity.Persona();
                    temp = (UTTT.Ejemplo.Persona.Data.Entity.Persona)listaObject[i];

                    lista.Add(temp);
                }
                this.dgvPersona.DataSource = lista;
                this.dgvPersona.DataBind();
            }
            catch (Exception _e)
            { 
            
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }
                CtrlPersona ctrlPersona = new CtrlPersona();
                UTTT.Ejemplo.Persona.Data.Entity.Persona persona = new Data.Entity.Persona();
                persona.StrClaveUnica = this.txtClave.Text.Trim();
                persona.StrNombre = this.txtNombre.Text.Trim();
                persona.StrAPaterno = this.txtAPaterno.Text.Trim();
                persona.StrAMaterno = this.txtAMaterno.Text.Trim();
               
                persona.IdCatSexo = int.Parse(this.dblSexo.SelectedValue);
                object objeto = persona;
                bool resultado = ctrlPersona.insertar(objeto);
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
            catch (Exception _e)
            { 
            
            }

        }

        private void alert(string p)
        {
            throw new NotImplementedException();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrlPersona ctrlPersona = new CtrlPersona();
                UTTT.Ejemplo.Persona.Data.Entity.Persona persona = new Data.Entity.Persona();
                persona.StrClaveUnica = this.txtClave.Text.Trim();              
                object objeto = persona;
                Object resultado = ctrlPersona.consultarItem(objeto);
                UTTT.Ejemplo.Persona.Data.Entity.Persona personaMuestra = (UTTT.Ejemplo.Persona.Data.Entity.Persona)resultado;
                if (personaMuestra.StrClaveUnica != null && !personaMuestra.StrClaveUnica.Equals(String.Empty))
                {
                    
                    this.txtClave.Text = personaMuestra.StrClaveUnica;
                    this.txtNombre.Text = personaMuestra.StrNombre;
                    this.txtAPaterno.Text = personaMuestra.StrAPaterno;
                    this.txtAMaterno.Text = personaMuestra.StrAMaterno;                    
                    this.setGridView();
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('El registro no existe');</script>");
                }   
                

            }
            catch (Exception _e)
            {

            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrlPersona ctrlPersona = new CtrlPersona();
                UTTT.Ejemplo.Persona.Data.Entity.Persona persona = new Data.Entity.Persona();
                persona.StrClaveUnica = this.txtClave.Text.Trim();
                persona.StrNombre = this.txtNombre.Text.Trim();
                persona.StrAPaterno = this.txtAPaterno.Text.Trim();
                persona.StrAMaterno = this.txtAMaterno.Text.Trim();
                object objeto = persona;
                bool resultado = ctrlPersona.actualizar(objeto);
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
            catch (Exception _e)
            {

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrlPersona ctrlPersona = new CtrlPersona();
                UTTT.Ejemplo.Persona.Data.Entity.Persona persona = new Data.Entity.Persona();
                persona.StrClaveUnica = this.txtClave.Text.Trim();
                object objeto = persona;
                Boolean resultado = ctrlPersona.eliminar(objeto);
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
            catch (Exception _e)
            {

            }
        }

        private void limpiar()
        {
            this.txtAMaterno.Text = String.Empty;
            this.txtAPaterno.Text = String.Empty;
            this.txtClave.Text = String.Empty;
            this.txtNombre.Text = String.Empty;
        }

        protected void dgvPersona_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               if(e.CommandName.Equals("Direccion"))
               {
                   int index = Convert.ToInt32(e.CommandArgument.ToString());                 
                   GridViewRow row = this.dgvPersona.Rows[index];              

                   ListItem item = new ListItem();
                   item.Text = Server.HtmlDecode(row.Cells[1].Text);
                   this.session.IdPersona = int.Parse(item.Text);

                  

                   ListItem itemName = new ListItem();
                   itemName.Text = Server.HtmlDecode(row.Cells[3].Text) + " " +
                    Server.HtmlDecode(row.Cells[4].Text) + " " + Server.HtmlDecode(row.Cells[5].Text);

                   this.session.StrNombrePersona = itemName.Text;
                   this.Session["SessionManager"] = this.session;
                   this.session.Pantalla = this.pantallaDireccion;
                   Response.Redirect(this.session.Pantalla);
               }
            }
            catch (Exception _e)
            { 
            
            }
        }

       

       
        
    }
}