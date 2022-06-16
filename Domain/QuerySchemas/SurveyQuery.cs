namespace Domain.QuerySchemas
{
    public class SurveyQuery
    {
        public string SurveyName { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<UserInfoSchema> AssignedUsers { get; set; }
    }
}
