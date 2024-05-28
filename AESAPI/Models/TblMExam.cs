using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMExam
    {
        public long ExamId { get; set; }
        public string? ExamName { get; set; }
        public int? ExamDuration { get; set; }
        public string? ExamSubject { get; set; }
        public int? Tnoqans { get; set; }
        public int? Passmark { get; set; }
        public string? ExamDescr { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
