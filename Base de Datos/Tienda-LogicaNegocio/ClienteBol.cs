using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_AccesoDatos;
using Tienda_Entidades;

namespace Tienda_LogicaNegocio
{
    public class ClienteBol
    {
        private ClienteDal _clienteDal = new ClienteDal();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void Registrar(ECliente cliente)
        {
            if(ValidarProducto(cliente))
            {
                if(_clienteDal.ObtenerIDCliente(cliente.id)==null)
                {
                    _clienteDal.Insert(cliente);
                }else
                {
                    _clienteDal.Actualizar(cliente);
                }
            }
        }
        private bool ValidarProducto(ECliente cliente)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(cliente.nombre)) stringBuilder.Append("El campo nombre  es obligatorio");
            if (string.IsNullOrEmpty(cliente.direccion)) stringBuilder.Append(Environment.NewLine + "El campo direccion es obligatorio");
            if (string.IsNullOrEmpty(cliente.telefono)) stringBuilder.Append(Environment.NewLine + "El campo telefono es obligatorio");
            //if (cliente.telefono.Length >= 8) stringBuilder.Append(Environment.NewLine + "El campo debe tener por lo menos 8 digitos");
            return stringBuilder.Length == 0;
        }
        public List<ECliente> Todos()
        {
            return _clienteDal.GetAll();
        }
    }
}
