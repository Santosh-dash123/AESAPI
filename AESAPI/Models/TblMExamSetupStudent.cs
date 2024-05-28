using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMExamSetupStudent
    {
        public long ExamsetupstdId { get; set; }
        public long? ExamsetupId { get; set; }
        public long? StdId { get; set; }
        public string? AttndStatus { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
        //This property added for retriving student name using student id
        public virtual TblMStudent Std { get; set; } // Navigation property
    }
}
