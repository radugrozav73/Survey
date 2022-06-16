using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    public class CompletedSurveyController : Controller
    {
        private readonly DataContext _dataContext;
        public CompletedSurveyController(DataContext data)
        {
            _dataContext = data;
        }

        [HttpGet]
        [Route("/get/post-completed/{id}")]
        public async Task<IActionResult> GetCompletedSurveys(Guid id)
        {
            try
            {
                var completed = _dataContext.CompletedSurveyUsersList.Include(x => x.Answers).Select(x => x).Where(x => x.SurveyId == id).ToList();
                return Ok(completed);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/post-completed-survey")]
        public async Task<IActionResult> StoreCompletedSurvey([FromBody] CompletedSurveyUsersList survey)
        {
            try
            {
                var checkIfExists = _dataContext.CompletedSurveyUsersList.Select(e => e)
                    .Where(x => x.SurveyId == survey.SurveyId && x.AspNetUsersId == survey.AspNetUsersId)
                    .Count();
                if (checkIfExists != 0) return Conflict("User already answared this question.");
                else
                {
                    CompletedSurveyUsersList completed = new CompletedSurveyUsersList { Answers = survey.Answers, AspNetUsersId = survey.AspNetUsersId, SurveyId = survey.SurveyId };
                    _dataContext.CompletedSurveyUsersList.Add(completed);
                    _dataContext.SaveChanges();
                    return Ok("Completed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
