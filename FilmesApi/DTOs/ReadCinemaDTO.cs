namespace FilmesApi.DTOs;

public class ReadCinemaDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ReadEnderecoDTO endereco { get; set; }
}
