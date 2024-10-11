using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTOs;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmesContext _context;
    private IMapper _mapper;

    public CinemaController(FilmesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarCinema([FromBody] CreateCinemaDTO cinemaDTO)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();

        return CreatedAtAction(nameof(RecuperaCinemaPorId), new { Id = cinema.Id }, cinemaDTO);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDTO> RecuperarCinemas()
    {
        return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.toList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();

        ReadCinemaDTO cinemaDto = _mapper.Map<ReadCinemaDTO>(cinema);
        return Ok(cinemaDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDTO cinemaDTO)
    {
        Cinema cinema = _context.Cinema.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();

        _mapper.Map(cinemaDTO, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        Cinema cinema = _context.Cinema.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}
