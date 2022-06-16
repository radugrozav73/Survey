using Microsoft.AspNetCore.Mvc;


namespace Survey.Api.Controllers;
[ApiController]
[Route("[Controller]")]
public class SurveyController : ControllerBase
{
    private DataContext context { get; set; }
    public SurveyController(DataContext dataContext)
    {
        context = dataContext;
    }
}
