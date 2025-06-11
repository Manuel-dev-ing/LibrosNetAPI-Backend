using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Servicios
{
    public interface IRepositorioLibros
    {
        Task actualizar(Libro libro);
        Task elminar(Libro libro);
        Task<bool> existeLibro(int id);
        Task guardar(Libro libro);
        Task<LibroDTO> obtenerLibroPorId(int id);
        Task<IEnumerable<LibroDTO>> obtenerLibros();
        Task<string> obtenerPortadaLibro(int id);
    }

    public class RepositoriosLibros: IRepositorioLibros
    {
        private readonly ApplicationDbContext context;

        public RepositoriosLibros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> obtenerPortadaLibro(int id)
        {
            var portada = await context.Libros.Where(x => x.Id == id).Select(x => x.Portada).FirstAsync();

            return portada;
        }

        public async Task<IEnumerable<LibroDTO>> obtenerLibros()
        {
            var libroDTO = await context.Libros
                .Select(x => new LibroDTO()
                {
                    Id = x.Id,
                    IdAutor = (int)x.IdAutor,
                    IdCategoria = (int)x.IdCategoria,
                    IdEditorial = (int)x.IdEditorial,
                    Titulo = x.Titulo,
                    Precio = (decimal)x.Precio,
                    Stock = (int)x.Stock,
                    Isbn = x.Isbn,
                    FechaPublicacion = DateTime.UtcNow,
                    NumeroPaginas = (int)x.NumeroPaginas,
                    Idioma = x.Idioma,
                    Sipnosis = x.Sipnosis,
                    Portada = x.Portada,
                    Estado = (bool)x.Estado
                }).ToListAsync();

            return libroDTO;
        }

        public async Task<LibroDTO> obtenerLibroPorId(int id)
        {
            var libroDTO = await context.Libros
                .Select(x => new LibroDTO()
                {
                    Id = x.Id,
                    IdAutor = (int)x.IdAutor,
                    IdCategoria = (int)x.IdCategoria,
                    IdEditorial = (int)x.IdEditorial,
                    Titulo = x.Titulo,
                    Precio = (decimal)x.Precio,
                    Stock = (int)x.Stock,
                    Isbn = x.Isbn,
                    FechaPublicacion = DateTime.UtcNow,
                    NumeroPaginas = (int)x.NumeroPaginas,
                    Idioma = x.Idioma,
                    Sipnosis = x.Sipnosis,
                    Portada = x.Portada,
                    Estado = (bool)x.Estado
                }).FirstOrDefaultAsync(x => x.Id == id);

            return libroDTO;
        }

        public async Task<bool> existeLibro(int id)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == id);
            return existeLibro;
        }

        public async Task guardar(Libro libro)
        {
            context.Libros.Add(libro);
            await context.SaveChangesAsync();
        }

        public async Task actualizar(Libro libro)
        {
            context.Libros.Update(libro);
            await context.SaveChangesAsync();
        }

        public async Task elminar(Libro libro)
        {
            context.Libros.Remove(libro);
            await context.SaveChangesAsync();
        }




    }
}
