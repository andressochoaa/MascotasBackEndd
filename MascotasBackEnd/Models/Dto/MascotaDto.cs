namespace MascotasBackEnd.Models.Dto
{
    public class MascotaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }

        public string Especie { get; set; }

        public int FechaNacimiento { get; set; }
        public int IdDueno { get; set; }

    }
}
