using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMUser
    {
        public long UId { get; set; }
        public string? UName { get; set; }
        public string? UMobno { get; set; }
        public string? UEmail { get; set; }
        public string? UAddrss { get; set; }
        public string? UType { get; set; }
        public string? UPwd { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
