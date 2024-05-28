using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMStudentCourse
    {
        public long StdcoursId { get; set; }
        public long? StdId { get; set; }
        public long? CoursId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
