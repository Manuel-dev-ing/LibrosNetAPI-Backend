using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace LibrosNetAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly IRepositorioLibros repositorioLibros;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private const string contenedor = "libros";

        public LibrosController(IRepositorioLibros repositorioLibros, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.repositorioLibros = repositorioLibros;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> get()
        {

            var libros = await repositorioLibros.obtenerLibros();

            return libros;
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroDTO>> get(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"El libro con el id: {id} no existe");
            }

            var existe_libro = await repositorioLibros.existeLibro(id);

            if (!existe_libro)
            {
                return NotFound("No existe el libro");
            }

            var libro = await repositorioLibros.obtenerLibroPorId(id);

            if (libro == null)
            {
                return NotFound("libro no encontrado");
            }

            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> post([FromForm] LibroCreacionDTO libroCreacionDTO)
        {
            var libro = new Libro()
            {
                IdAutor = libroCreacionDTO.IdAutor,
                IdCategoria = libroCreacionDTO.IdCategoria,
                IdEditorial = libroCreacionDTO.IdEditorial,
                Titulo = libroCreacionDTO.Titulo,
                Precio = libroCreacionDTO.Precio,
                Stock = libroCreacionDTO.Stock,
                Isbn = libroCreacionDTO.Isbn,
                FechaPublicacion = DateTime.UtcNow,
                NumeroPaginas = libroCreacionDTO.NumeroPaginas,
                Idioma = libroCreacionDTO.Idioma,
                Sipnosis = libroCreacionDTO.Sipnosis,
                Estado = true
            };

            if (libroCreacionDTO.Portada is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, libroCreacionDTO.Portada);
                libro.Portada = url;
            }

            await repositorioLibros.guardar(libro);

            var libroDTO = new LibroDTO()
            {
                Id = libro.Id,
                IdAutor = (int)libro.IdAutor,
                IdCategoria = (int)libro.IdCategoria,
                IdEditorial = (int)libro.IdEditorial,
                Titulo = libro.Titulo,
                Precio = (decimal)libro.Precio,
                Stock = (int)libro.Stock,
                Isbn = libro.Isbn,
                FechaPublicacion = DateTime.UtcNow,
                NumeroPaginas = (int)libro.NumeroPaginas,
                Idioma = libro.Idioma,
                Sipnosis = libro.Sipnosis,
                Portada = libro.Portada,
                Estado = (bool)libro.Estado
            };

            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, LibroCreacionDTO libroCreacionDTO) 
        {
            if (id <= 0)
            {
                return BadRequest($"El libro con el id: {id} no existe");
            }

            var existe_libro = await repositorioLibros.existeLibro(id);

            if (!existe_libro)
            {
                return NotFound("No existe el libro");
            }

            var libro = new Libro()
            {
                Id = id,
                IdAutor = libroCreacionDTO.IdAutor,
                IdCategoria = libroCreacionDTO.IdCategoria,
                IdEditorial = libroCreacionDTO.IdEditorial,
                Titulo = libroCreacionDTO.Titulo,
                Precio = libroCreacionDTO.Precio,
                Stock = libroCreacionDTO.Stock,
                Isbn = libroCreacionDTO.Isbn,
                FechaPublicacion = DateTime.UtcNow,
                NumeroPaginas = libroCreacionDTO.NumeroPaginas,
                Idioma = libroCreacionDTO.Idioma,
                Sipnosis = libroCreacionDTO.Sipnosis,
                Estado = true
            };

            if (libroCreacionDTO.Portada is not null)
            {
                var portadaActual = await repositorioLibros.obtenerPortadaLibro(id);

                var url = await almacenadorArchivos.Editar(portadaActual, contenedor, libroCreacionDTO.Portada);
                libro.Portada = url;
            }

            await repositorioLibros.actualizar(libro);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest($"El libro con el id: {id} no existe");
            }

            var existe_libro = await repositorioLibros.existeLibro(id);

            if (!existe_libro)
            {
                return NotFound("No existe el libro");
            }
            var libroDTO = await repositorioLibros.obtenerLibroPorId(id);

            if (libroDTO is null)
            {
                return NotFound();
            }

            var libro = new Libro()
            {
                Id = id,
                IdAutor = libroDTO.IdAutor,
                IdCategoria = libroDTO.IdCategoria,
                IdEditorial = libroDTO.IdEditorial,
                Titulo = libroDTO.Titulo,
                Precio = libroDTO.Precio,
                Stock = libroDTO.Stock,
                Portada = libroDTO.Portada,
                Isbn = libroDTO.Isbn,
                FechaPublicacion = DateTime.UtcNow,
                NumeroPaginas = libroDTO.NumeroPaginas,
                Idioma = libroDTO.Idioma,
                Sipnosis = libroDTO.Sipnosis,
                Estado = libroDTO.Estado
            };

            await repositorioLibros.elminar(libro);
            await almacenadorArchivos.Borrar(libro.Portada, contenedor);

            return NoContent();
        }

    }
}
