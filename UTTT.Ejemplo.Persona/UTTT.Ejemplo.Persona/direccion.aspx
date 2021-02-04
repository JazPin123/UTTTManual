<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="direccion.aspx.cs" Inherits="UTTT.Ejemplo.Persona.direccion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblPersona" runat="server" Text="Datos Persona"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Persona: "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="txtPersona" runat="server" Text="txtPersona"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Datos Dirección"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Colonia:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtColonia" runat="server" Width="322px"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Calle:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCalle" runat="server" Width="322px"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Número:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtNumero" runat="server" Width="326px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnAgregar" runat="server" Text="Aceptar" 
            onclick="btnAgregar_Click" />
        <br />
        <br />
        <asp:GridView ID="dgvDireccion" runat="server" EnableModelValidation="True" 
            onrowcommand="dgvDireccion_RowCommand">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Editar" HeaderText="Editar" 
                    ShowHeader="True" Text="Editar" />
                <asp:ButtonField ButtonType="Button" CommandName="Eliminar" 
                    HeaderText="Eliminar" ShowHeader="True" Text="Eliminar" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
            onclick="btnRegresar_Click" />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
