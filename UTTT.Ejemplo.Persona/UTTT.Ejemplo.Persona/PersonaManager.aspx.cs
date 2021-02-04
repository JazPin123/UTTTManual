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
using EASendMail;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

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
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    catTemp.id = -1;
                    catTemp.strValor = "Seleccionar";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();

                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        DateTime tiempo = new DateTime(2021, 03, 01);

                        this.dteCalendar.TodaysDate = tiempo;
                        this.dteCalendar.SelectedDate = tiempo;
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);


                        DateTime? fechadenacimiento = this.baseEntity.dteFechaNacimiento;
                        if (fechadenacimiento != null)
                        {
                            this.dteCalendar.TodaysDate = (DateTime)fechadenacimiento;
                            this.dteCalendar.SelectedDate = (DateTime)fechadenacimiento;
                        }
                        this.txtcorreoelectronico.Text = this.baseEntity.strCorreoElectronico;
                        this.txtcodigopostal.Text = this.baseEntity.strCodigoPostal;
                        this.txtrfc.Text = this.baseEntity.strRFC;
                    }                
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime fecha1 = this.dteCalendar.SelectedDate.Date;
                int años = ((TimeSpan)(DateTime.Now - fecha1)).Days;
                if (años < 6575)
                {
                    this.showMessage("eres menor de edad");


                }
                else
                {

                    if (!Page.IsValid)
                    {
                        return;
                    }


                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                    if (this.idPersona == 0)
                    {
                        persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                        persona.strNombre = this.txtNombre.Text.Trim();
                        persona.strAMaterno = this.txtAMaterno.Text.Trim();
                        persona.strAPaterno = this.txtAPaterno.Text.Trim();
                        persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                        DateTime fechadenacimiento = this.dteCalendar.SelectedDate.Date;

                        persona.dteFechaNacimiento = fechadenacimiento;
                        persona.strCorreoElectronico = this.txtcorreoelectronico.Text.Trim();
                        persona.strCodigoPostal = this.txtcodigopostal.Text.Trim();
                        persona.strRFC = this.txtrfc.Text.Trim();

                        String mensaje = String.Empty;
                        if (!this.validacion(persona, ref mensaje))
                        {

                            this.lbmensaje.Text = mensaje;
                            this.lbmensaje.Visible = true;
                            return;
                        }

                        if (!this.validaSql(ref mensaje))
                        {

                            this.lbmensaje.Text = mensaje;
                            this.lbmensaje.Visible = true;
                            return;
                        }
                        if (!this.validaHTML(ref mensaje))
                        {
                            this.lbmensaje.Text = mensaje;
                            this.lbmensaje.Visible = true;
                            return;
                        }


                        dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se agrego correctamente.");
                        this.Response.Redirect("~/PersonaPrincipal.aspx", false);

                    }
                    if (this.idPersona > 0)
                    {
                        DateTime fechadeNacimiento2 = this.dteCalendar.SelectedDate.Date;
                        int años2 = ((TimeSpan)(DateTime.Now - fechadeNacimiento2)).Days;
                        if (años2 < 6575)
                        {
                            this.showMessage("Eres menor de edad");
                        }
                        else
                        {
                            persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                            persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                            persona.strNombre = this.txtNombre.Text.Trim();
                            persona.strAMaterno = this.txtAMaterno.Text.Trim();
                            persona.strAPaterno = this.txtAPaterno.Text.Trim();
                            persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                            DateTime fechadenacimiento = this.dteCalendar.SelectedDate.Date;
                            persona.dteFechaNacimiento = fechadenacimiento;
                            persona.strCorreoElectronico = this.txtcorreoelectronico.Text.Trim();
                            persona.strCodigoPostal = this.txtcodigopostal.Text.Trim();
                            persona.strRFC = this.txtrfc.Text.Trim();

                            String mensaje = String.Empty;
                            if (!this.validacion(persona, ref mensaje))
                            {

                                this.lbmensaje.Text = mensaje;
                                this.lbmensaje.Visible = true;
                                return;
                            }

                            if (!this.validaSql(ref mensaje))
                            {

                                this.lbmensaje.Text = mensaje;
                                this.lbmensaje.Visible = true;
                                return;
                            }
                            if (!this.validaHTML(ref mensaje))
                            {
                                this.lbmensaje.Text = mensaje;
                                this.lbmensaje.Visible = true;
                                return;
                            }

                            dcGuardar.SubmitChanges();
                            this.showMessage("El registro se edito correctamente.");
                            this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                        }
                    }
                }
            }
            catch (Exception _e)

            {
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("jazelotitotierno@gmail.com", "Exception", mensaje);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {              
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

       
        #region Metodos

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
           
            if (_persona.idCatSexo == -1)
            {
                _mensaje = "Elige un genero";
                return false;
            }

            
            int i = 0;
            if (int.TryParse(_persona.strClaveUnica, out i) == false)
            {
                _mensaje = "Debe contener 3 caracteres";
                return false;
            }
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "Fuera de rango";
                return false;
            }

         
            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "Campo vacío";
                return false;
            }
            if (_persona.strNombre.Length < 3 || _persona.strNombre.Length > 15)
            {
                _mensaje = "Debe contener 3 a 15 caracteres ";
                return false;
            }

            
            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "Campo vacio";
                return false;
            }
            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "El Apellido Paterno debe contener 3 a 50 caracteres";
                return false;
            }

      
            if (_persona.strAMaterno.Equals(String.Empty))
            {
                _mensaje = "Campo vacio";
                return false;
            }
            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = "El Apellido Materno debe contener 3 a 50 caracteres";
                return false;
            }
            return true;
        }

        private bool validaSql(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();

            string mensajeFuncion = string.Empty;

            if (valida.sqlInyectionValida(this.txtClaveUnica.Text.Trim(), ref mensajeFuncion, "Clave Unica", ref this.txtClaveUnica))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtcorreoelectronico.Text.Trim(), ref mensajeFuncion, "Correo Electronico", ref this.txtcorreoelectronico))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtcodigopostal.Text.Trim(), ref mensajeFuncion, "Codigo Postal", ref this.txtcodigopostal))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtrfc.Text.Trim(), ref mensajeFuncion, "RFC", ref this.txtrfc))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        private bool validaHTML(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();
            string mensajeFuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }

            return true;
        }

        public void EnviarCorreo(string correoDestino, string asunto, string mensajeCorreo)
        {
            string mensaje = "No se pudo enviar el correo";

            try
            {
                SmtpMail objetoCorreo = new SmtpMail("TryIt");

                objetoCorreo.From = "jazelotitotierno@gmail.com ";
                objetoCorreo.To = correoDestino;
                objetoCorreo.Subject = asunto;
                objetoCorreo.TextBody = mensajeCorreo;

                SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");

                objetoServidor.User = "jazelotitotierno@gmail.com ";
                objetoServidor.Password = "Elotitotierno1";
                objetoServidor.Port = 587;
                objetoServidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient objetoCliente = new SmtpClient();
                objetoCliente.SendMail(objetoServidor, objetoCorreo);
                mensaje = "Enviado";


            }
            catch (Exception ex)
            {
                mensaje = "No se pudo enviar el correo" + ex.Message;
            }
            
        }








        #endregion
    }
}