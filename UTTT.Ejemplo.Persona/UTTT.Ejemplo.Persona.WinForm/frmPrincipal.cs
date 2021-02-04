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
    public partial class frmPrincipal : Form
    {
        ejemplo.EjemploSoapClient example = new ejemplo.EjemploSoapClient();
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                
                ejemplo.Persona[] persona = example.consultarGlobalPersona();
                this.dgvPersona.DataSource = persona;

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmPersonaManager view = new frmPersonaManager();
                bool resultado = view.setForm(this, null);
                if (resultado)
                {
                    ejemplo.Persona[] persona = example.consultarGlobalPersona();
                    this.dgvPersona.DataSource = persona;
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
    }
}
