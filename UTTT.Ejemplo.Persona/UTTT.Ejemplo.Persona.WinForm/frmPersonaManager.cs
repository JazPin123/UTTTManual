using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ejemplo = UTTT.Ejemplo.Persona.WinForm.Ejemplo;

namespace UTTT.Ejemplo.Persona.WinForm
{
    public partial class frmPersonaManager : Form
    {
        bool resultado = false;
        ejemplo.Persona personaGlobal;
        ejemplo.EjemploSoapClient example = new ejemplo.EjemploSoapClient();

        public frmPersonaManager()
        {
            InitializeComponent();
        }
       
        public bool setForm(Form _parent, ejemplo.Persona _persona)
        {
            this.personaGlobal = _persona;
            this.setInformation();
            this.ShowDialog(_parent);
            return this.resultado;
        }

        private void setInformation()
        {
            try
            {
                if (this.personaGlobal == null)
                {
                    this.lblAccion.Text = "Agregar";
                }
                else
                {
                    this.lblAccion.Text = "Editar";
                    this.txtClave.Text = this.personaGlobal.StrClaveUnica;
                    this.txtNombre.Text = this.personaGlobal.StrNombre;
                    this.txtAParterno.Text = this.personaGlobal.StrAPaterno;
                    this.txtAMaterno.Text = this.personaGlobal.StrAMaterno;
                }
                
                ejemplo.CatSexo[] listaSexo = this.example.consultaGlobalSexo();
                this.cmbSexo.DataSource = listaSexo;
                this.cmbSexo.ValueMember = "Id";
                this.cmbSexo.DisplayMember = "StrValor";
            }
            catch (Exception _e)
            {
                throw _e;
            }
        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ejemplo.Persona personaTemp = new ejemplo.Persona();
                personaTemp.StrClaveUnica = this.txtClave.Text.Trim();
                personaTemp.StrNombre = this.txtNombre.Text.Trim();
                personaTemp.StrAPaterno = this.txtAParterno.Text.Trim();
                personaTemp.StrAMaterno = this.txtAMaterno.Text.Trim();
                personaTemp.IdCatSexo = ((ejemplo.CatSexo)this.cmbSexo.SelectedItem).Id;
                bool resultado = this.example.insertarPersona(personaTemp);
                MessageBox.Show("El registro se inserto correctamente");
                this.Close();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }
    }
}
