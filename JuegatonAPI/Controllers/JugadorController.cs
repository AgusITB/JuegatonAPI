﻿using JuegatonAPI.Models;
using JuegatonAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace JuegatonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JugadorController : ControllerBase
    {
        private readonly JugadorRepository jugadorRepository;

        public JugadorController(JugadorRepository repository)
        {
            jugadorRepository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetAllJugadores()
        {
            return Ok(await jugadorRepository.GetAllJugadores());
        }

        // GET: api/Juegaton/Nickname
        [HttpGet("{nickname}")]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetJugador(string nickname)
        {
            return Ok(await jugadorRepository.GetJugador(nickname));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJugador([FromBody] Jugador jugador)
        {
            if (jugador == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await jugadorRepository.InsertJugador(jugador);
            return Created("creado!", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlayer([FromBody] Jugador jugador)
        {
            if (jugador == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var update = await jugadorRepository.UpdateJugador(jugador);
            return Created("actualizado!", update);

        }
        [HttpDelete("{nickname}")]
        public async Task<IActionResult> DeletePlayer(string nickname)
        {
            var deleted = await jugadorRepository.DeletePlayer(new Jugador { Nickname = nickname});
            return Created("borrado!", deleted);
        }

        [HttpPut("{puntuacion} {nickname}")]

        public async Task<IActionResult> UpdateScore([FromRoute] int puntuacion,[FromRoute] string nickname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var update = await jugadorRepository.UpdateScore(puntuacion, nickname);
            return Created("actualizado!", update);

        }

    }
}
