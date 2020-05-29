using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmService.Models
{
    public class OutstockModel
    {
        public OutstockMaster master { get; set; }
        public List<OutstockDetail> detail { get; set; }
    }
    public class OutstockMaster
    {
        public string pjlx { get; set; }//单据类型
        public string lsbh { get; set; }//流水编号
        public string djbh { get; set; }//单据编号
        public string djrq { get; set; }//单据日期
        public string dwbh { get; set; }//单位编号
        public string dwmc { get; set; }//单位名称
        public string bmbh { get; set; }//领用部门编号
        public string bmmc { get; set; }//领用部门名称
        public string zgbh { get; set; }//领用人编号
        public string zgxm { get; set; }//领用人名称
        public string ckbh { get; set; }//仓库编号
        public string ckmc { get; set; }//仓库名称
        public string note { get; set; }//备注
    }
    public class OutstockDetail
    {
        public string lsbh { get; set; }
        public string flbh { get; set; }
        public string tdls { get; set; }
        public string tdfl { get; set; }
        public string wlbh { get; set; }
        public string wlmc { get; set; }
        public string pch { get; set; }
        public string jldw { get; set; }
        public decimal sl { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
        public string jh { get; set; }//件号
    }

}