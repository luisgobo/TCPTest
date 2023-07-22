using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public string IdCliente { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Genero { get; set; }
        public bool Estado { get; set; }

        public string NombreCompleto {
            get 
            { 
                return $"{Nombre} {PrimerApellido} {SegundoApellido}"; 
            } 
        }

        public Cliente(string idCliente, string nombre, string primerApellido, string segundoApellido, DateTime fechaNacimiento, char genero, bool estado)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
            Genero = genero;
            Estado = estado;
        }

        public Cliente() { }
    }
}
