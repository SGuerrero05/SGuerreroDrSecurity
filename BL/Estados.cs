using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estados
    {
        public static ML.Result EstadoGetByPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroDrSecurityEntities context = new DL.SGuerreroDrSecurityEntities())
                {
                    var query = context.EstadoGetByPais(IdPais).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Estados estado = new ML.Estados();

                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;
                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = obj.IdPais;

                            result.Objects.Add(estado);
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
                result.Correct= false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
