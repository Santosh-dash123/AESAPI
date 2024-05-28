namespace AESAPI.Models
{
    public class QuestionModel
    {
        public int ExamId { get; set; }
        public string QuestionContent { get; set; }
        public List<OptionModel> Options { get; set; }
    }

    public class OptionModel
    {
        public string content { get; set; }
        public string status { get; set; }
    }
}
