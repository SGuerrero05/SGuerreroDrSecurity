using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using ML;
using CURP;
using CURP.Enums;
namespace BL
{ 
    public class Usuario
    {
        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                usuario.CURP = GenerarCurp(usuario);
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaNacimiento, usuario.Sexo, usuario.EstadoNacimiento, usuario.Telefono, usuario.Calle, usuario.NumeroInterior, usuario.NumeroExterior, usuario.Colonia.IdColonia, usuario.CURP);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage=ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioDelete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.UsuarioDelete(usuario.IdUsuario);
                    if (query > 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se elimino el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct=!false;
                result.ErrorMessage =ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                usuario.CURP = GenerarCurp(usuario);
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.UsuarioUPDATE(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaNacimiento, usuario.Sexo, usuario.EstadoNacimiento, usuario.Telefono, usuario.Calle, usuario.NumeroInterior, usuario.NumeroExterior, usuario.Colonia.IdColonia, usuario.CURP);
                    if (query >= 1)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se actualizo correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.UsuarioGetAll().ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value;
                            usuario.Sexo = obj.Sexo;
                            usuario.EstadoNacimiento= obj.EstadoNacimiento;
                            usuario.Telefono = obj.Telefono;
                            usuario.Calle = obj.Calle;
                            usuario.NumeroInterior = obj.NumeroInterior;
                            usuario.NumeroExterior = obj.NumeroExterior;

                            usuario.Colonia = new ML.Colonia();
                            usuario.Colonia.IdColonia = obj.IdColonia;
                            usuario.Colonia.Nombre = obj.NombreColonia;

                            usuario.Colonia.Municipio = new ML.Municipio();
                            usuario.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Colonia.Municipio.Estado = new ML.Estados();
                            usuario.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            usuario.CURP = obj.CURP;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;                       
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Fallo la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result UsuarioGetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var obj = context.UsuarioGetById(IdUsuario).FirstOrDefault();
                    result.Objects = new List<object>();
                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.FechaNacimiento = obj.FechaNacimiento.Value;
                        usuario.Sexo = obj.Sexo;
                        usuario.EstadoNacimiento = obj.EstadoNacimiento;
                        usuario.Telefono = obj.Telefono;
                        usuario.Calle = obj.Calle;
                        usuario.NumeroInterior = obj.NumeroInterior;
                        usuario.NumeroExterior = obj.NumeroExterior;

                        usuario.Colonia = new ML.Colonia();
                        usuario.Colonia.IdColonia = obj.IdColonia;
                        usuario.Colonia.Nombre = obj.NombreColonia;

                        usuario.Colonia.Municipio = new ML.Municipio();
                        usuario.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                        usuario.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                        usuario.Colonia.Municipio.Estado = new ML.Estados();
                        usuario.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                        usuario.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                        usuario.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais; 
                        usuario.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                        usuario.CURP = obj.CURP;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static string GenerarCurp(ML.Usuario usuario)
        {
            try
            {
                Estado result;
                Enum.TryParse(usuario.EstadoNacimiento, out result); 

                var Sex = Sexo.Mujer; 
                bool H = usuario.Sexo.Contains("Hombre");

                if (H == true)
                {
                    Sex = Sexo.Hombre;
                }
                string CURP = Curp.Generar(usuario.Nombre,
                                           usuario.ApellidoPaterno,
                                           usuario.ApellidoMaterno,
                                           Sex,
                                           usuario.FechaNacimiento,
                                           result);
                if (CURP != null)
                {
                    usuario.CURP = CURP;
                }
            }
            catch (Exception ex)
            {

            }
            return usuario.CURP;
        }
    }
}
