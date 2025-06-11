using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibrosNetAPI.DTOs;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibrosNetAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;
        private readonly IConfiguration config;

        public UsuariosController(IRepositorioUsuarios repositorioUsuarios, IConfiguration config)
        {
            this.repositorioUsuarios = repositorioUsuarios;
            this.config = config;
        }

        [HttpPost("registro")]
        public async Task<ActionResult> registro(RegistroCreacionDTO registroCreacionDTO)
        {
            var resultado = await repositorioUsuarios.existeUsuario(registroCreacionDTO.Correo);

            if (resultado)
            {
                return BadRequest("El usuario ya existe");
            }

            var usuarios = new Usuario()
            {
                IdRol = registroCreacionDTO.IdRol,
                Nombre = registroCreacionDTO.Nombre,
                PrimerApellido = registroCreacionDTO.PrimerApellido,
                SegundoApellido = registroCreacionDTO.SegundoApellido,
                Correo = registroCreacionDTO.Correo,
                PasswordHash = hashPassword(registroCreacionDTO.Contrasena),
            };

            await repositorioUsuarios.guardarUsuario(usuarios);

            var token = generarToken(usuarios);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            var usuario = await repositorioUsuarios.obtenerUsuarioPorCorreo(login);

            if (usuario == null || !VerifyPassword(login.Password, usuario.PasswordHash))
            {
                return Unauthorized("Credenciales incorrectas");
            }

            var token = generarToken(usuario);

            return Ok(token);
        }

        [HttpGet("renovar-token")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> renovarToken()
        {
            var nameIdenfier = await repositorioUsuarios.obetenerIdUsuarioToken();

            var id = Convert.ToInt32(nameIdenfier);

            var usuario = await repositorioUsuarios.obtenerUsuarioPorId(id);

            if (usuario is null)
            {
                return NotFound();
            }   

            var respuestaAutenticacion = generarToken(usuario);

            return respuestaAutenticacion;
        }

        private RespuestaAutenticacionDTO generarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["llavejwt"]));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);

            return new RespuestaAutenticacionDTO
            {
                Token = token,
                Expiracion = expiracion
            };

        }

        private string hashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string entered, string storedHash)
        {
            return hashPassword(entered) == storedHash;
        }

    }
}
