using Domain.QuerySchemas;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class SurveyModel
    {
        [ForeignKey("AspNetUsers")]
        public Guid AspNetUsersId { get; set; }
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<UserInfoSchema> AssignedUsers { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
