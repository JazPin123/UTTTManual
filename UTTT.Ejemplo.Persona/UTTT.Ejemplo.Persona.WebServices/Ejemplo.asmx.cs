using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UTTT.Ejemplo.Persona.Data;
using System.Data.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Linq.Expressions;

namespace UTTT.Ejemplo.Persona.WebServices
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Ejemplo : System.Web.Services.WebService
    {
        #region Web Metodos Persona

        [WebMethod]
        public bool insertarPersona(UTTT.Ejemplo.Persona.Data.Entity.Persona _persona)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                persona.strClaveUnica = _persona.StrClaveUnica;
                persona.strNombre = _persona.StrNombre;
                persona.strAMaterno = _persona.StrAMaterno;
                persona.strAPaterno = _persona.StrAPaterno;
                persona.idCatSexo = _persona.IdCatSexo;
                dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch(Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool editarPersona(UTTT.Ejemplo.Persona.Data.Entity.Persona _persona)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = 
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == _persona.Id);
                persona.strClaveUnica = _persona.StrClaveUnica;
                persona.strNombre = _persona.StrNombre;
                persona.strAMaterno = _persona.StrAMaterno;
                persona.strAPaterno = _persona.StrAPaterno;
                persona.idCatSexo = _persona.IdCatSexo;
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool eliminarPersona(UTTT.Ejemplo.Persona.Data.Entity.Persona _persona)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == _persona.Id);
                dcTemp.GetTable<Direccion>().DeleteAllOnSubmit(persona.Direccion);
                dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().DeleteOnSubmit(persona);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Persona[] consultarGlobalPersona()
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                List<UTTT.Ejemplo.Linq.Data.Entity.Persona> listaPersona =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().ToList();
                UTTT.Ejemplo.Persona.Data.Entity.Persona[] tempPersona = new Data.Entity.Persona[listaPersona.Count()];
                
                for (int i = 0; i < listaPersona.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.Persona temp = new Data.Entity.Persona();
                    temp.Id = listaPersona[i].id;
                    temp.StrNombre = listaPersona[i].strNombre;
                    temp.StrAPaterno = listaPersona[i].strAPaterno;
                    temp.StrAMaterno = listaPersona[i].strAMaterno;
                    temp.StrClaveUnica = listaPersona[i].strClaveUnica;
                    temp.StrValorSexo = listaPersona[i].CatSexo.strValor;
                    //asignamos el objeto catsexo adjunto al de persona
                    UTTT.Ejemplo.Persona.Data.Entity.CatSexo catSexoTemp = new Data.Entity.CatSexo();
                    catSexoTemp.Id = listaPersona[i].CatSexo.id;
                    catSexoTemp.StrValor = listaPersona[i].CatSexo.strValor;
                    temp.CatSexoTemp = catSexoTemp;
                    tempPersona[i] = temp;
                }
                dcTemp.Dispose();
                return tempPersona;
               
            }
            catch (Exception _e)
            {
                return null;
            }
            
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Persona consultarUnicaPersona(UTTT.Ejemplo.Persona.Data.Entity.Persona _persona)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
             
                //objeto persona
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == _persona.Id);
                UTTT.Ejemplo.Persona.Data.Entity.Persona temp = new Data.Entity.Persona();
                temp.Id = persona.id;
                temp.StrNombre = persona.strNombre;
                temp.StrAPaterno = persona.strAPaterno;
                temp.StrAMaterno = persona.strAMaterno;
                temp.StrValorSexo = persona.CatSexo.strValor;
                //asignamos el objeto catsexo adjunto al de persona
                UTTT.Ejemplo.Persona.Data.Entity.CatSexo catSexoTemp = new Data.Entity.CatSexo();
                catSexoTemp.Id = persona.CatSexo.id;
                catSexoTemp.StrValor = persona.CatSexo.strValor;
                temp.CatSexoTemp = catSexoTemp;
                dcTemp.Dispose();
                return temp;                
            }
            catch (Exception _e)
            {
                return null;
            }            
        }

        #endregion

        #region Web Metodos Direccion

        [WebMethod]
        public bool insertarDireccion(UTTT.Ejemplo.Persona.Data.Entity.Direccion _direccion)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Direccion direccion = new Linq.Data.Entity.Direccion();
                direccion.idPersona = _direccion.IdPersona;
                direccion.strCalle = _direccion.StrCalle;
                direccion.strColonia = _direccion.StrColonia;
                direccion.strNumero = _direccion.StrNumero;
                dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Direccion>().InsertOnSubmit(direccion);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool editarDireccion(UTTT.Ejemplo.Persona.Data.Entity.Direccion _direccion)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Direccion direccion = dcTemp.GetTable<Direccion>().First( c=> c.id == _direccion.Id);
               // direccion.idPersona = _direccion.IdPersona;
                direccion.strCalle = _direccion.StrCalle;
                direccion.strColonia = _direccion.StrColonia;
                direccion.strNumero = _direccion.StrNumero;                
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool eliminarDireccion(UTTT.Ejemplo.Persona.Data.Entity.Direccion _direccion)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Direccion direccion = dcTemp.GetTable<Direccion>().First(c => c.id == _direccion.Id);
                dcTemp.GetTable<Direccion>().DeleteOnSubmit(direccion);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Direccion[] consultarGlobalDireccion(int  _IDpersona)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                
                List<UTTT.Ejemplo.Linq.Data.Entity.Direccion> listaDireccion =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Direccion>().Where(c => c.idPersona == _IDpersona).ToList();
                UTTT.Ejemplo.Persona.Data.Entity.Direccion[] tempDireccion = new Data.Entity.Direccion[listaDireccion.Count()];

                for (int i = 0; i < listaDireccion.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.Direccion temp = new Data.Entity.Direccion();
                    temp.Id = listaDireccion[i].id;
                    temp.IdPersona = listaDireccion[i].idPersona;
                    temp.StrCalle = listaDireccion[i].strCalle;
                    temp.StrColonia = listaDireccion[i].strColonia;
                    temp.StrNumero = listaDireccion[i].strNumero;

                    tempDireccion[i] = temp;
                }
                dcTemp.Dispose();
                return tempDireccion;

            }
            catch (Exception _e)
            {
                return null;
            }
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Direccion consultarUnicaDireccion(UTTT.Ejemplo.Persona.Data.Entity.Direccion _direccion)
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Direccion direccion = dcTemp.GetTable<Direccion>().First(c => c.id == _direccion.Id);
                UTTT.Ejemplo.Persona.Data.Entity.Direccion temp = new Data.Entity.Direccion();
                temp.Id = direccion.id;
                temp.IdPersona = direccion.idPersona;
                temp.StrCalle = direccion.strCalle;
                temp.StrColonia = direccion.strColonia;
                temp.StrNumero = direccion.strNumero;                
                dcTemp.Dispose();
                return temp;               
            }
            catch (Exception _e)
            {
                
            }
            return null;

        }

        #endregion

        #region Catalogo Sexo

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.CatSexo[] consultaGlobalSexo()
        {
            try
            {
                DataContext dcTemp = new DcGeneralDataContext();
                List<UTTT.Ejemplo.Linq.Data.Entity.CatSexo> listaSexo =            dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.CatSexo>().ToList();
                UTTT.Ejemplo.Persona.Data.Entity.CatSexo[] tempSexo = new Data.Entity.CatSexo[listaSexo.Count()];

                for (int i = 0; i < listaSexo.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.CatSexo temp = new Data.Entity.CatSexo();
                    temp.Id = listaSexo[i].id;
                    temp.StrValor = listaSexo[i].strValor;
                    tempSexo[i] = temp;
                }
                dcTemp.Dispose();
                return tempSexo;

            }
            catch (Exception _e)
            {
                return null;
            }
        }

        #endregion


    }
}