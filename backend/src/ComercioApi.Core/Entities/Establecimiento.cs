namespace ComercioApi.Core.Entities;

public class Establecimiento
{
    public int Id { get; set; }
    public string NombreEstablecimiento { get; set; } = string.Empty;
    public decimal Ingresos { get; set; }
    public int NumeroEmpleados { get; set; }
    public int ComercianteId { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public string UsuarioModifica { get; set; } = string.Empty;

    public Comerciante Comerciante { get; set; } = null!;
}
