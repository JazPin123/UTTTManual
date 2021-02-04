using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UTTT.Ejemplo.Persona.Control
{
   public class CtrValidaInyeccion
    {


        public bool sqlInyectionValida(String _inforamacion, ref string _mensaje, string _etiquetaReferente,
             ref System.Web.UI.WebControls.TextBox _control)
        {
            Regex tagRejex = new Regex(@"('(''|[^'])*')|(\b(ALTER|alter|Alter|CREATE|
            create|Create|DELETE|delete|Delete|DROP|drop|Drop|EXEC(UTE){0,1}|exec(ute){0,1}|
            Exec(ute){0,1}|INSERT( +INTO){0,1}|insert( +into){0,1}|Insert( +into){0,1}|MERGE|
            merge|Merge|SELECT|Select|select|UPDATE|update|Update|UNION( +ALL){0,1}|
            union( +all){0,1}|Union( +all){0,1})\b)");
            bool respuesta = tagRejex.IsMatch(_inforamacion);
            if (respuesta)
            {
                _mensaje = "sintaxis no permitida en " + _etiquetaReferente.Replace(":", "");
                _control.Focus();
            }
            return respuesta;
        }

        public bool htmlInyectionValida(String _informacion, ref string _mensaje, string _etiquetaReferente,
        ref System.Web.UI.WebControls.TextBox _control)
        {
            Regex tagRegex = new Regex(@"<.*?>|&.*?;");
            bool respuesta = tagRegex.IsMatch(_informacion);
            if (respuesta)
            {
                _mensaje = "Caracteres no permitidos en " + _etiquetaReferente.Replace(":", "");
                _control.Focus();
            }
            return respuesta;
        }

    }
}
