using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UTTT.Ejemplo.Persona.Control.Interface;
using UTTT.Ejemplo.Persona.Data.Entity;
using System.Data.SqlClient;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlDireccion : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Direccion (idPersona, strCalle, strNumero, strColonia) VALUES( '"
                 + direccion.IdPersona + "','"
                 + direccion.StrCalle + "','"
                 + direccion.StrNumero + "','"
                 + direccion.StrColonia + "')", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public bool eliminar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from Direccion where id=" + direccion.Id, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public bool actualizar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Update Direccion  set  strCalle='" + direccion.StrCalle +
                     "', strColonia ='" + direccion.StrColonia+
                     "', strNumero ='" + direccion.StrNumero +
                     "' where id=" + direccion.Id, conn);

                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public List<object> consultarLista(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from Direccion where IdPersona ="+ direccion.IdPersona , conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion direccionTemp = new Data.Entity.Direccion();
                    direccionTemp.Id = int.Parse(reader["id"].ToString());
                    direccionTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    direccionTemp.StrCalle = reader["strCalle"].ToString();
                    direccionTemp.StrColonia = reader["strColonia"].ToString();
                    direccionTemp.StrNumero = reader["strnumero"].ToString();

                    Object objeto = direccionTemp;
                    lista.Add(objeto);
                }
                conn.Close();
                return lista;
            }
            catch (Exception _e)
            {

            }
            return null;
        }

        public object consultarItem(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccion = (UTTT.Ejemplo.Persona.Data.Entity.Direccion)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from Direccion where id=" + direccion.Id + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Direccion direccionTemp = new Data.Entity.Direccion();
                while (reader.Read())
                {
                    direccionTemp.Id = int.Parse(reader["id"].ToString());
                    direccionTemp.IdPersona = int.Parse(reader["IdPersona"].ToString());
                    direccionTemp.StrCalle = reader["strCalle"].ToString();
                    direccionTemp.StrColonia = reader["strColonia"].ToString();
                    direccionTemp.StrNumero = reader["strnumero"].ToString();

                }
                conn.Close();
                Object objeto = direccionTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
            return null;
        }
    }
}
