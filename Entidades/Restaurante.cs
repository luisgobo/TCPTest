using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Restaurante
    {
        public Restaurante(int idRestaurante, string nombre, string direccion, string telefono, bool estado) 
        { 
            IdRestaurante = idRestaurante;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Estado = estado;
        }
        
        public int IdRestaurante { get; set; }
        public string Nombre { get;set; }
        public string Direccion { get; set; }
        public string Telefono { get; set;}
        public bool Estado { get; set; }
    }
}
