using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class ESecurity
    {
        public string Ruc { get; set; }
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public string FullName { get; set; }

        public int IdSignInType { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Nombres { get; set; }        
        public int IdTablaUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }        
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int TotalElements { get; set; }


        //LOGIN

        public string ErrorMessage { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Perfil2 { get; set; }

        //CLIENTE

        public int IdCliente { get; set; }        

        //CONTRATISTA

        public int IdContratista { get; set; }
        public string RazonSocial { get; set; }

        //EMPLEADO

        public int IdEmpleado { get; set; }        
        public string NroDocumento { get; set; }        

        //ROLES
        public int idRol { get; set; }
        public string nameRol { get; set; }
        public int idUserTablaUsuario { get; set; }
        public string nametablaUser { get; set; }
        public int IdTipoTablaUsuario { get; set; }
        public string User_cliente_contra { get; set; }

        public string IdTipoTablaUsuarioName { get; set; }
        public string idRolName { get; set; }
        public string idUserTablaUsuarioName { get; set; }
    }
}
