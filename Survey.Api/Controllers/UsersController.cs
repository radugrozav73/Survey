using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext data)
        {
            _dataContext = data;
        }

        [HttpGet]
        [Route("get-users/{name}")]
        public IActionResult getUsers(string name)
        {
            var users = _dataContext.Users.Select(e => new { e.Id, e.UserName, e.Email }).Where(e => e.UserName.Contains(name)).ToList();
            return Ok(users);
        }
    }
}
