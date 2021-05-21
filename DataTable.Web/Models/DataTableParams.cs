namespace DataTable.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataTableParams
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }
    }
}
