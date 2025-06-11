using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using LibrosNetAPI.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    //[Authorize]
    public class AutoresController : ControllerBase
    {
        private readonly IRepositorioAutores repositorioAutores;
        private readonly IRepositorioUsuarios repositorioUsuarios;

        public AutoresController(IRepositorioAutores repositorioAutores, IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioAutores = repositorioAutores;
            this.repositorioUsuarios = repositorioUsuarios;
        }

        [HttpGet]
        public async Task<IEnumerable<AutorDTO>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = await repositorioAutores.getAutors();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var autorDTO = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            return autorDTO;
        }



        [HttpGet("{id:int}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest($"El autor con el {id} no puede ser 0");
            }

            var existAutor = await repositorioAutores.existAutor(id);

            if (!existAutor)
            {
                return BadRequest($"El autor con el id: {id} no existe");
            }

            var result = await repositorioAutores.getAutorById(id);

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }


        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = new Autor()
            {
                Nombre = autorCreacionDTO.Nombre,
                PrimerApellido = autorCreacionDTO.PrimerApellido,
                SegundoApellido = autorCreacionDTO.SegundoApellido,
                Telefono = autorCreacionDTO.Telefono,
                Correo = autorCreacionDTO.Correo,
                Estado = true
            };

            await repositorioAutores.saveAutor(autor);

            return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, autor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            if (id == 0)
            {
                return BadRequest($"El autor con el {id} no puede ser 0");
            }

            var existeAutor = await repositorioAutores.existAutor(id);

            if (!existeAutor)
            {
                return BadRequest($"El autor con el id: {id} no existe");
            }

            var autor = new Autor()
            {
                Id = id,
                Nombre = autorCreacionDTO.Nombre,
                PrimerApellido = autorCreacionDTO.PrimerApellido,
                SegundoApellido = autorCreacionDTO.SegundoApellido,
                Telefono = autorCreacionDTO.Telefono,
                Correo = autorCreacionDTO.Correo,
                Estado = await repositorioAutores.getStateAutor(id)
            };

            await repositorioAutores.editAutor(autor);

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest($"El autor con el {id} no puede ser 0");
            }

            var existAutor = await repositorioAutores.existAutor(id);

            if (!existAutor)
            {
                return BadRequest($"El autor con el id: {id} no existe");

            }

            var autorDTO = await repositorioAutores.getAutorById(id);

            var autor = new Autor()
            {
                Id = autorDTO.Id,
                Nombre = autorDTO.Nombre,
                PrimerApellido = autorDTO.PrimerApellido,
                SegundoApellido = autorDTO.SegundoApellido,
                Telefono = autorDTO.Telefono,
                Correo = autorDTO.Correo,
                Estado = !autorDTO.Estado
            };

            await repositorioAutores.editAutor(autor);
            return NoContent();
        } 


    }
}
