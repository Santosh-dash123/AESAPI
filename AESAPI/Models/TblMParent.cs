using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMParent
    {
        public long PrntId { get; set; }
        public long? StdId { get; set; }
        public string? PrntName { get; set; }
        public string? PrntType { get; set; }
        public string? PrntOcuption { get; set; }
        public decimal? PrntIncom { get; set; }
        public string? PrntMobno { get; set; }
        public string? PrntEmail { get; set; }
        public string? PrntAdrs { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
