using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/team")]
    public class TeamController : Controller
    {
        private readonly DataContext _dataContext;

        public TeamController(DataContext data)
        {
            _dataContext = data;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }
    }
}
