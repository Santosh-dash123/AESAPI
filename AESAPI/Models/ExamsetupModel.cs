namespace AESAPI.Models
{
    public class ExamsetupModel
    {
        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int DFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyIp { get; set; }

        public List<StudentModel> Students { get; set; } 
    }

    public class StudentModel
    {
        public int StudentId { get; set; }
    }
}
