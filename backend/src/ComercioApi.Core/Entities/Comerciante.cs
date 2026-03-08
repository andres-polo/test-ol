namespace ComercioApi.Core.Entities;

public class Comerciante
{
    public int Id { get; set; }
    public string NombreRazonSocial { get; set; } = string.Empty;
    public int MunicipioId { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaActualizacion { get; set; }
    public string UsuarioModifica { get; set; } = string.Empty;

    public Municipio Municipio { get; set; } = null!;
    public ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();
}
