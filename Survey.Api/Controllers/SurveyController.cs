using Domain.Models;
using Domain.QuerySchemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SurveyController : Controller
    {

        private readonly DataContext _dataContext;

        public SurveyController(DataContext data)
        {
            _dataContext = data;
        }

        [HttpGet]
        [Route("/get-all-posts")]
        public async Task<IActionResult> GetSurveys()
        {
            var UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var surveys = _dataContext.Survey
                    .Include(e => e.Questions)
                    .Include(e => e.AssignedUsers)
                    .Select(e => new { e.AssignedUsers, e.SurveyName, e.Questions, e.Id, e.AspNetUsersId })
                    .Where(e => e.AspNetUsersId == UserId).ToList();
                return Ok(surveys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("/post-get/{id}")]
        public async Task<IActionResult> GetSurveyById(Guid id)
        {
            try
            {
                SurveyModel survey = _dataContext.Survey.FirstOrDefault(e => e.Id == id);
                return Ok(survey);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/post-survey")]
        public async Task<IActionResult> StoreSurvey(SurveyQuery survey)
        {
            var UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Questions> questionList = new List<Questions>();
            List<UserInfoSchema> userInfoSchema = new List<UserInfoSchema>();

            foreach (var item in survey.Questions)
            {
                questionList.Add(new Questions { Question = item.Question, AnswerType = item.AnswerType });
            }

            foreach (var item in survey.AssignedUsers)
            {
                userInfoSchema.Add(new UserInfoSchema
                {
                    Email = item.Email,
                    UserName = item.UserName,
                });
            }
            try
            {
                SurveyModel newSurvey = new SurveyModel { AspNetUsersId = UserId, Questions = questionList, SurveyName = survey.SurveyName, AssignedUsers = userInfoSchema };
                _dataContext.Survey.Add(newSurvey);
                _dataContext.SaveChanges();
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("/delete-survey/{surveyId}")]
        public async Task<IActionResult> DeleteSurvey(Guid surveyId)
        {
            try
            {
                var survey = _dataContext.Survey.Include(e => e.Questions).Include(e => e.AssignedUsers).Where(e => e.Id == surveyId).First();

                _dataContext.Survey.Remove(survey);
                _dataContext.SaveChanges();
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
