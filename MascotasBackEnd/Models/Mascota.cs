namespace MascotasBackEnd.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Raza { get; set; }

        public string Especie { get; set; }

        public int FechaNacimiento { get; set; }
        public int IdDueno { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
