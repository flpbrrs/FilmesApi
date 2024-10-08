using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOs
{
    public class ReadFilmeDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string genero { get; set; }
        public int duracao { get; set; }
    }
}