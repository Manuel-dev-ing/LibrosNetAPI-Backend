using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Servicios
{
    public interface IRepositorioAutores
    {
        Task editAutor(Autor autor);
        Task<bool> existAutor(int id);
        Task<AutorDTO> getAutorById(int id);
        Task<IQueryable<AutorDTO>> getAutors();
        Task<bool> getStateAutor(int id);
        Task saveAutor(Autor autor);
    }

    public class RepositorioAutores: IRepositorioAutores
    {
        private readonly ApplicationDbContext context;

        public RepositorioAutores(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<AutorDTO>> getAutors()
        {
            var autorDTO = context.Autors
                .Where(x => x.Estado == true)
                .Select(x => new AutorDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    PrimerApellido = x.PrimerApellido,
                    SegundoApellido = x.SegundoApellido,
                    Telefono = x.Telefono,
                    Correo = x.Correo,
                    Estado = (bool)x.Estado

                }).AsQueryable();

            

            return autorDTO;
        }

        public async Task<bool> existAutor(int id)
        {
            var entity = await context.Autors.AnyAsync(x => x.Id == id);

            return entity;

        }

        public async Task<AutorDTO> getAutorById(int id)
        {

            var entidad = await context.Autors
                .Select(x => new AutorDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    PrimerApellido = x.PrimerApellido,
                    SegundoApellido = x.SegundoApellido,
                    Telefono = x.Telefono,
                    Correo = x.Correo,
                    Estado = (bool)x.Estado

                }).FirstOrDefaultAsync(x => x.Id == id);


            return entidad;
        }

        public async Task saveAutor(Autor autor)
        {
            context.Autors.Add(autor);
            await context.SaveChangesAsync();
        }

        public async Task editAutor(Autor autor)
        {
            context.Autors.Update(autor);
            await context.SaveChangesAsync();
        }

        public async Task<bool> getStateAutor(int id)
        {
            var state = await context.Autors
                .Where(x => x.Id == id)
                .Select(x => x.Estado)
                .FirstOrDefaultAsync();

            return (bool)state;

        }


    }
}
