using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult UsuarioGetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.UsuarioGetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult UsuarioForm (int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultPais = BL.Pais.PaisGetAll();
            
            if (resultPais.Correct)
            {
                if (IdUsuario == null)
                {
                    usuario.Colonia = new ML.Colonia();
                    usuario.Colonia.Municipio = new ML.Municipio();
                    usuario.Colonia.Municipio.Estado = new ML.Estados();
                    usuario.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    usuario.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    return View(usuario);
                }
                else
                {
                    ML.Result result = BL.Usuario.UsuarioGetById(IdUsuario.Value);
                    if (result.Correct)
                    {
                        usuario = (ML.Usuario)result.Object;
                        ML.Result resultEstado = BL.Estados.EstadoGetByPais(usuario.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Result resultMunicipio = BL.Municipio.MunicipioGetByEstado(usuario.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultColonia = BL.Colonia.ColoniaGetByMunicipio(usuario.Colonia.Municipio.IdMunicipio);
                        
                        

                        usuario.Colonia.Colonias = resultColonia.Objects;
                        usuario.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        usuario.Colonia.Municipio.Estado.Estadoss = resultEstado.Objects;
                        usuario.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                        return View(usuario);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                        return View("UsuarioModal");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Error en la consulra";
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult UsuarioForm(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuario.UsuarioAdd(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
                }
            }
            else
            {
                ML.Result result = BL.Usuario.UsuarioUpdate(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Modificacion exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }
            return View("UsuarioModal");
        }

        [HttpGet]
        public ActionResult UsuarioDelete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            ML.Result result = BL.Usuario.UsuarioDelete(usuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View("UsuarioModal");
        }
    }
}