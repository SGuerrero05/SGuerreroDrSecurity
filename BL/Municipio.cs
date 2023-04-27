using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.MunicipioGetByEstado(IdEstado).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = obj.IdEstado;
                            municipio.Nombre = obj.Nombre;
                            municipio.Estado = new ML.Estados();
                            municipio.Estado.IdEstado = obj.IdEstado;

                            result.Objects.Add(municipio);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
    }
}
