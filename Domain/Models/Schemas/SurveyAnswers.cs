
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class SurveyAnswers
    {
        [Key]
        public Guid AnswerId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerType { get; set; }
    }
}
