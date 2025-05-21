using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace LibrosNetAPI.Controllers
{
    [ApiController]
    [Route("api/editorial")]
    public class EditorialController: ControllerBase
    {
        private readonly IRepositorioEditorial repositorioEditorial;

        public EditorialController(IRepositorioEditorial repositorioEditorial)
        {
            this.repositorioEditorial = repositorioEditorial;
        }

        [HttpGet]
        public async Task<IEnumerable<EditorialDTO>> get()
        {
            var editorial = await repositorioEditorial.obtenerEditoriales();

            return editorial;
        }


        [HttpGet("{id:int}", Name = "ObtenerEditorial")]
        public async Task<ActionResult<EditorialDTO>> get(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"El id: {id} no valido");
            }

            var existe = await repositorioEditorial.existeEditorial(id);

            if (!existe)
            {
                return NotFound();
            }

            var editorialDTO = await repositorioEditorial.obtenerEditorialPorId(id);

            return editorialDTO;
        }


        [HttpPost]
        public async Task<ActionResult> post(EditorialCreacionDTO editorialCreacionDTO)
        {

            var editorial = new Editorial()
            {
                Nombre = editorialCreacionDTO.Nombre,
                Correo = editorialCreacionDTO.Correo,
                Telefono = editorialCreacionDTO.Telefono,
                Calle = editorialCreacionDTO.Calle,
                Numero = editorialCreacionDTO.Numero,
                Colonia = editorialCreacionDTO.Colonia,
                CodigoPostal = editorialCreacionDTO.CodigoPostal,
                Ciudad = editorialCreacionDTO.Ciudad,
                Estado = editorialCreacionDTO.Estado
            };

            await repositorioEditorial.guardarEditorial(editorial);

            var editorialDTO = new EditorialDTO()
            {
                Id = editorial.Id,
                Nombre = editorial.Nombre,
                Correo = editorial.Correo,
                Telefono = editorial.Telefono,
                Calle = editorial.Calle,
                Numero = editorial.Numero,
                Colonia = editorial.Colonia,
                CodigoPostal = editorial.CodigoPostal,
                Ciudad = editorial.Ciudad,
                Estado = editorial.Estado
            };


            return CreatedAtRoute("ObtenerEditorial", new { id = editorial.Id}, editorialDTO );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, EditorialCreacionDTO editorialCreacionDTO)
        {
            if (id <= 0)
            {
                return BadRequest($"El id: {id} no es valido");
            }

            var resultado = await repositorioEditorial.existeEditorial(id);

            if (!resultado)
            {
                return NotFound();
            }

            var editorial = new Editorial()
            {
                Id = id,
                Nombre = editorialCreacionDTO.Nombre,
                Correo = editorialCreacionDTO.Correo,
                Telefono = editorialCreacionDTO.Telefono,
                Calle = editorialCreacionDTO.Calle,
                Numero = editorialCreacionDTO.Numero,
                Colonia = editorialCreacionDTO.Colonia,
                CodigoPostal = editorialCreacionDTO.CodigoPostal,
                Ciudad = editorialCreacionDTO.Ciudad,
                Estado = editorialCreacionDTO.Estado
            };

            await repositorioEditorial.actualizarEditorial(editorial);

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"El id: {id} no es valido");
            }

            var resultado = await repositorioEditorial.existeEditorial(id);

            if (!resultado)
            {
                return NotFound();
            }

            var result = await repositorioEditorial.eliminarEditorial(id);
            
            if (result == 0)
            {
                return NotFound();
            }


            return NoContent();
        }


    }
}
