using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Servicios
{
    public interface IRepositorioCategoria
    {
        Task<bool> existCategoria(int id);
        Task<IEnumerable<CategoriaDTO>> getCategories();
        Task<CategoriaDTO> getCategoryById(int id);
        Task<bool> getStateCategory(int id);
        Task savedCategoria(Categorium categoria);
        Task updateCategory(Categorium categoria);
    }


    public class RepositorioCategoria:IRepositorioCategoria
    {
        private readonly ApplicationDbContext context;

        public RepositorioCategoria(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> getStateCategory(int id)
        {
            var state = await context.Categoria
                .Where(x => x.Id == id)
                .Select(x => x.Estado)
                .FirstOrDefaultAsync();

            return (bool)state;
        }


        public async Task<IEnumerable<CategoriaDTO>> getCategories()
        {
            var entity = await context.Categoria
                .Where(x => x.Estado == true)
                .Select(x => new CategoriaDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Estado = (bool)x.Estado
                
                }).ToListAsync();

            return entity;
        }

        public async Task<CategoriaDTO> getCategoryById(int id)
        {

            var entity = await context.Categoria
                .Select(x => new CategoriaDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Estado = (bool)x.Estado
                }).FirstOrDefaultAsync(x => x.Id == id);
            
            return entity;
        }

        public async Task<bool> existCategoria(int id)
        {
            var existCategoria = await context.Categoria.AnyAsync(x => x.Id == id);
         
            return existCategoria;
        }

        public async Task savedCategoria(Categorium categoria)
        {
            context.Categoria.Add(categoria);
            await context.SaveChangesAsync();

        }

        public async Task updateCategory(Categorium categoria)
        {
            context.Categoria.Update(categoria);
            await context.SaveChangesAsync();

        }

    }
}
