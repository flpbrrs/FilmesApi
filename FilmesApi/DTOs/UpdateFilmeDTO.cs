using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOs
{
    public class UpdateFilmeDTO
    {
        [Required(ErrorMessage = "O campo titulo é obrigatório")]
        public string titulo { get; set; }
        [Required(ErrorMessage = "O campo gênero é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamando máximo é de 50 caracteres")]
        public string genero { get; set; }
        [Required(ErrorMessage = "O campo duração é obrigatório")]
        [Range(0, 200, ErrorMessage = "O tamanho deve ter entre 0 e 200")]
        public int duracao { get; set; }
    }
}
