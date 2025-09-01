namespace Education.Application.DTOs;

public class CursoDTO
{
    public Guid CursoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaPublicacion { get; set; }
    public decimal Precio { get; set; }
    public int Duracion { get; set; }

}
