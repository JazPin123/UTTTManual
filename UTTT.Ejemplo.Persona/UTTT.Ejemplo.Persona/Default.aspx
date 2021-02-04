<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UTTT.Ejemplo.Persona._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Ejemplo MVC </title>
    <link href="controlformato.css" type="text/css" rel="stylesheet" />
    <script>
        
        function validar() {
            //obteniendo el valor que se puso en campo text del formulario
            miCampoTexto = document.getElementById("txtClave").value;
            //la condición
            //obligatorios
            //enteros y cadena
            //expresion regular

            if (miCampoTexto.length == 0) {
                alert('El campo clave está vacio!');
                return false;
            }
            return true;
        }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-family: Andalus; background-color: #FFFF00; position: absolute; top: 15px; left: 10px; width: 77px; height: 20px;"> <asp:Label ID="Label1" runat="server" Text="Clave:"></asp:Label> </div>
       
       <div style="margin-left: 93px"><asp:TextBox ID="txtClave" runat="server" MaxLength="6"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvClave" runat="server" 
               ControlToValidate="txtClave" ErrorMessage="*"></asp:RequiredFieldValidator>
        </div>
         <div style="margin-left: 93px"> <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox> </div>
        <br />
        <div style="font-family: Andalus; background-color: #FFFF00; position: absolute; top: 41px; left: 13px; height: 19px;"><asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label> </div>
        <br />
        <asp:Label ID="Label3" runat="server" Text="A Paterno:"></asp:Label>
        <asp:TextBox ID="txtAPaterno" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="A Materno:"></asp:Label>
        <asp:TextBox ID="txtAMaterno" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblSexo" runat="server" Text="Sexo: "></asp:Label>
        <asp:DropDownList ID="dblSexo" runat="server" Font-Size="Smaller" Height="18px" 
            Width="224px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
            Text="Agregar" CssClass="ButtonControl"  />
        <asp:Button ID="btnConsultar" runat="server" onclick="btnConsultar_Click" 
            Text="Consultar" />
        <asp:Button ID="btnActualizar" runat="server" onclick="btnActualizar_Click" 
            Text="Actualizar" />
        <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
            Text="Eliminar" />
    
        <br />
        <br />
        <asp:GridView ID="dgvPersona" runat="server" 
             EnableModelValidation="True" onrowcommand="dgvPersona_RowCommand" 
            Width="649px">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Direccion" 
                    HeaderText="Direccion" ShowHeader="True" Text="Direccion" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourcePersona" runat="server">
        </asp:ObjectDataSource>
    
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
