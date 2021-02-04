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
    public class CtrlSexo : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            throw new NotImplementedException();
        }

        public bool eliminar(object _o)
        {
            throw new NotImplementedException();
        }

        public bool actualizar(object _o)
        {
            throw new NotImplementedException();
        }

        public List<object> consultarLista(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Persona persona = (UTTT.Ejemplo.Persona.Data.Entity.Persona)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from CatSexo ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.CatSexo catSexo = new Data.Entity.CatSexo();
                    catSexo.Id = int.Parse(reader["id"].ToString());
                    catSexo.StrValor = reader["strValor"].ToString();
                    catSexo.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = catSexo;
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
            throw new NotImplementedException();
        }
    }
}
