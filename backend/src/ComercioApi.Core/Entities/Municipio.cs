namespace ComercioApi.Core.Entities;

public class Municipio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Comerciante> Comerciantes { get; set; } = new List<Comerciante>();
}
