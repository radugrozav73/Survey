using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class CompletedSurveyUsersList
    {
        public Guid Id { get; set; }
        [ForeignKey("AspNetUsers")]
        public Guid AspNetUsersId { get; set; }
        [ForeignKey("Survey")]
        public Guid SurveyId { get; set; }
        public IEnumerable<SurveyAnswers> Answers { get; set; }
    }
}
