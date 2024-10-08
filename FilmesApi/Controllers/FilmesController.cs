using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTOs;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private FilmesContext _context;
        private IMapper _mapper;

        public FilmesController(FilmesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult RegistraFilme([FromBody] CreateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);

            _context.filmes.Add(filme);
            _context.SaveChanges();

            var readFilmeDTO = _mapper.Map<ReadFilmeDTO>(filme);

            return CreatedAtAction(
                nameof(BuscarFilmePorId),
                new { filme.id },
                readFilmeDTO
             );
        }

        [HttpGet]
        public IEnumerable<ReadFilmeDTO> BuscarTodosFilmes(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 5
        )
        {
            return _mapper.Map<List<ReadFilmeDTO>>(_context.filmes.Skip(take * skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarFilmePorId(int id) {
            var filmeBuscado = _context.filmes.FirstOrDefault((filme) => filme.id == id);

            if (filmeBuscado == null) return NotFound();
            return Ok(filmeBuscado);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilmePorId(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            var filmeBuscado = _context.filmes.FirstOrDefault((filme) => filme.id == id);

            if (filmeBuscado == null) return NotFound();

            var filmeAtualizado = _mapper.Map(filmeDTO, filmeBuscado);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult AtualizaParcialFilmePorId(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
        {
            var filmeBuscado = _context.filmes.FirstOrDefault((filme) => filme.id == id);

            if (filmeBuscado == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filmeBuscado);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if(!TryValidateModel(filmeParaAtualizar)) return ValidationProblem();

            _mapper.Map(filmeParaAtualizar, filmeBuscado);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilmePorId(int id)
        {
            var filmeBuscado = _context.filmes.FirstOrDefault((filme) => filme.id == id);

            if (filmeBuscado == null) return NotFound();

            _context.Remove(filmeBuscado);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
