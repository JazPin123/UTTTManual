using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class Direccion
    {
        private int id;
        private int idPersona;
        private String strCalle;
        private String strNumero;
        private String strColonia;

        public String StrColonia
        {
            get { return strColonia; }
            set { strColonia = value; }
        }

        public String StrNumero
        {
            get { return strNumero; }
            set { strNumero = value; }
        }

        public String StrCalle
        {
            get { return strCalle; }
            set { strCalle = value; }
        }

        public int IdPersona
        {
            get { return idPersona; }
            set { idPersona = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
