using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmService.Models
{
    public class BillModel
    {
        public string route { get; set; }
        public string user { get; set; }
        public Dictionary<string,object> head { get; set; }
        public List<Dictionary<string,object>> body { get; set; }
    }



}