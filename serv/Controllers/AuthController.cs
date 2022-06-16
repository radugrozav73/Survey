using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Survey.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private DataContext context { get; set; }
        public static User user = new User();

        public AuthController(DataContext dataContext)
        {
            context = dataContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Users request)
        {
            CreatePassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            try
            {
                var user = await this.context.Users.AddAsync(
                    new Users { Email = request.Email, Name = request.Name, Password = request.Password }
                    );
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            if (user.Email != request.Email)
            {
                return BadRequest("User not found");
            }

            return Ok("Token");
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash == passwordHash;
            }
        }
    }
}
