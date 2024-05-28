using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMStudentAdemic
    {
        public long SacdmcId { get; set; }
        public long? StdId { get; set; }
        public string? SacdmcType { get; set; }
        public DateTime? SacdmcPasyer { get; set; }
        public decimal? SacdmcResult { get; set; }
        public string? SacdmcInstute { get; set; }
        public string? SacdmcBord { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
