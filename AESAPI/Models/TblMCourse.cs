using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMCourse
    {
        public long CoursId { get; set; }
        public string? CoursName { get; set; }
        public string? CoursDuration { get; set; }
        public decimal? CoursFee { get; set; }
        public string? TrnerName { get; set; }
        public string? TrnerDescr { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
