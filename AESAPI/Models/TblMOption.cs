using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMOption
    {
        public long OptionId { get; set; }
        public long? ExamId { get; set; }
        public long? QansId { get; set; }
        public string? OptionContent { get; set; }
        public string? OptionStatus { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
