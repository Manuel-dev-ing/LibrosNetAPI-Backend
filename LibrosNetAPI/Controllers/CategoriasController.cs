using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace LibrosNetAPI.Controllers
{

    [ApiController]
    [Route("api/categoria")]
    public class CategoriasController: ControllerBase
    {
        private readonly IRepositorioCategoria repositorioCategoria;

        public CategoriasController(IRepositorioCategoria repositorioCategoria)
        {
            this.repositorioCategoria = repositorioCategoria;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaDTO>> get()
        {
            var entity = await repositorioCategoria.getCategories();

            return entity;
        }


        [HttpGet("{id:int}", Name = "ObtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> get(int id)
        {
            if (id == 0)
            {
                return BadRequest($"La categoria con el id: {id} no puede ser 0");
            }

            var existCategory = await repositorioCategoria.existCategoria(id);

            if (!existCategory)
            {
                return BadRequest($"La categoria con el id: {id} no existe");
            }

            var entity = await repositorioCategoria.getCategoryById(id);

            return entity;
        }



        [HttpPost]
        public async Task<ActionResult> post([FromBody] CategoriaCreacionDTO creacionDTO)
        {
            var categoria = new Categorium()
            {
                Nombre = creacionDTO.Nombre,
                Estado = true
            };

            await repositorioCategoria.savedCategoria(categoria);

            var categoriaDTO = new CategoriaDTO()
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Estado = (bool)categoria.Estado
            };

            return CreatedAtRoute("ObtenerCategoria", new {id = categoria.Id }, categoriaDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, CategoriaCreacionDTO categoriaCreacionDTO)
        {
            if (id == 0)
            {
                return BadRequest($"La categoria con el id: {id} no puede ser 0");
            }

            var existCategory = await repositorioCategoria.existCategoria(id);

            if (!existCategory)
            {
                return BadRequest($"La categoria con el id: {id} no existe");
            }

            var category = new Categorium()
            {
                Id = id,
                Nombre = categoriaCreacionDTO.Nombre,
                Estado = await repositorioCategoria.getStateCategory(id)
            };

            await repositorioCategoria.updateCategory(category);

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {

            if (id == 0)
            {
                return BadRequest($"La categoria con el id: {id} no puede ser 0");
            }

            var existCategory = await repositorioCategoria.existCategoria(id);

            if (!existCategory)
            {
                return BadRequest($"La categoria con el id: {id} no existe");
            }

            var categoryDTO = await repositorioCategoria.getCategoryById(id);

            var category = new Categorium()
            {
                Id = categoryDTO.Id,
                Nombre = categoryDTO.Nombre,
                Estado = !categoryDTO.Estado
            };

            await repositorioCategoria.updateCategory(category);

            return NoContent();
        }


    }
}
