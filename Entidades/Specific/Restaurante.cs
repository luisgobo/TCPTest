using System.Collections.Generic;

namespace Entidades.Specific
{
    public class Restaurante
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public List<Plato> Platos { get; set; }

        public Restaurante() { }

        public Restaurante(int id, int nombre, string direccion, string telefono, bool estado, List<Plato> platos)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Estado = estado;
            Platos = platos;
        }
    }
}