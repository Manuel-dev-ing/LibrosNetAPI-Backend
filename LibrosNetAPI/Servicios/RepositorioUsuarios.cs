using System.Security.Claims;
using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibrosNetAPI.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task<bool> existeUsuario(string correo);
        Task guardarUsuario(Usuario usuario);
        Task<string> obetenerIdUsuarioToken();
        Task<Usuario> obtenerUsuarioPorCorreo(LoginDTO loginDTO);
        Task<Usuario> obtenerUsuarioPorId(int id);
    }


    public class RepositorioUsuarios:IRepositorioUsuarios
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor contextAccessor;

        public RepositorioUsuarios(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> obetenerIdUsuarioToken()
        {
            var NameIdentifier = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (NameIdentifier is null)
            {
                return null;
            }

            return NameIdentifier;
        }

        public async Task<bool> existeUsuario(string correo)
        {
            var resultado = await context.Usuarios.AnyAsync(x => x.Correo == correo);

            return resultado;
        }

        public async Task<Usuario> obtenerUsuarioPorId(int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            return usuario;
        }

        public async Task<Usuario> obtenerUsuarioPorCorreo(LoginDTO loginDTO)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Correo == loginDTO.Correo);

            return usuario;
        }

        public async Task guardarUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }


    }
}
