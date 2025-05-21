using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Servicios
{
    public interface IRepositorioEditorial
    {
        Task actualizarEditorial(Editorial editorial);
        Task<int> eliminarEditorial(int id);
        Task<bool> existeEditorial(int id);
        Task guardarEditorial(Editorial editorial);
        Task<IEnumerable<EditorialDTO>> obtenerEditoriales();
        Task<EditorialDTO> obtenerEditorialPorId(int id);
    }


    public class RepositorioEditorial : IRepositorioEditorial
    {
        private readonly ApplicationDbContext context;

        public RepositorioEditorial(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<EditorialDTO>> obtenerEditoriales()
        {
            var editoriales = await context.Editorials
                .Select(x => new EditorialDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Telefono = x.Telefono,
                    Calle = x.Calle,
                    Numero = x.Numero,
                    Colonia = x.Colonia,
                    CodigoPostal = x.CodigoPostal,
                    Ciudad = x.Ciudad,
                    Estado = x.Estado

                }).ToListAsync();

            return editoriales;
        }


        public async Task<bool> existeEditorial(int id)
        {
            var existe = await context.Editorials.AnyAsync(x => x.Id == id);

            return existe;
        }

        public async Task<EditorialDTO> obtenerEditorialPorId(int id)
        {
            var editorial = await context.Editorials
                .Select(x => new EditorialDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Telefono = x.Telefono,
                    Calle = x.Calle,
                    Numero = x.Numero,
                    Colonia = x.Colonia,
                    CodigoPostal = x.CodigoPostal,
                    Ciudad = x.Ciudad,
                    Estado = x.Estado
                }).FirstOrDefaultAsync(x => x.Id == id);

            return editorial;
        }


        public async Task guardarEditorial(Editorial editorial)
        {
            context.Editorials.Add(editorial);
            await context.SaveChangesAsync();

        }

        public async Task actualizarEditorial(Editorial editorial)
        {
            context.Editorials.Update(editorial);
            await context.SaveChangesAsync();
        }
        
        public async Task<int> eliminarEditorial(int id)
        {

            var resultado = await context.Editorials.Where(x => x.Id == id).ExecuteDeleteAsync();

            return resultado;
        }


    }
}
