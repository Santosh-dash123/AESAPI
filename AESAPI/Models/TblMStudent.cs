using System;
using System.Collections.Generic;

namespace AESAPI.Models
{
    public partial class TblMStudent
    {
        public long StdId { get; set; }
        public string? StdFrstname { get; set; }
        public string? StdLstname { get; set; }
        public string? StdEmail { get; set; }
        public string? StdGnder { get; set; }
        public DateTime? StdDob { get; set; }
        public string? StdAdharno { get; set; }
        public string? StdMobNo { get; set; }
        public string? StdPhoto { get; set; }
        public string? StdPermntadrs { get; set; }
        public string? StdCurntadrs { get; set; }
        public string? StdBldgrp { get; set; }
        public string? StdNationality { get; set; }
        public string? StdReligion { get; set; }
        public string? StdState { get; set; }
        public string? StdPwd { get; set; }
        public int? DFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedIp { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyIp { get; set; }
    }
}
