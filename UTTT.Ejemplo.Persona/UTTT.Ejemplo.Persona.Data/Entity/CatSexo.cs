using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class CatSexo
    {
        private int id;
        private String strValor;
        private String strDescripcion;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }      

        public String StrValor
        {
            get { return strValor; }
            set { strValor = value; }
        }       

        public String StrDescripcion
        {
            get { return strDescripcion; }
            set { strDescripcion = value; }
        }

        public override string ToString()
        {
            return this.strValor;
        }
       
    }
}
