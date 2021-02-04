using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public static class CtrlMessage
    {
        public static void showMessage(this System.Web.UI.Page _page, String _message)
        {
            _page.ClientScript.RegisterStartupScript(_page.GetType(),
                   Guid.NewGuid().ToString(),
                   "alert( '" + _message + "');", true);

            //_page.ClientScript.RegisterClientScriptBlock(_page.GetType(), "ClientScript", "<script type='text/javascript'> $(function(){ $('#dlgResultado').dialog({ modal: true, resizable: false, autoOpen: true, draggable: false, open: function(type, data){$(this).parent().appendTo('form')} }); }); </script>");

        }

        public static void showMessageException(this System.Web.UI.Page _page, String _message)
        {
            String mensaje = "Error de tipo " + _message + ". Ponerse en contacto con su administrador de sistema";
            _page.ClientScript.RegisterStartupScript(_page.GetType(),
                   "ClientScript",
                   "<SCRIPT>alert( '" + mensaje + "');</SCRIPT>");

        }
    }
}
