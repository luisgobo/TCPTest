namespace Entidades.Specific
{
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public double Precio { get; set; }

        public Plato()
        {
        }

        public Plato(int id, string nombre, int categoria, double precio)
        {
            Id = id;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
        }
    }
}