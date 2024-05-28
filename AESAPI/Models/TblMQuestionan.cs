using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMQuestionan
    {
        public long QansId { get; set; }
        public long? ExamId { get; set; }
        public string? QansContent { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
