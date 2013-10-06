using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Contracts.DTO.Supplier
{
    public class SupplierInfo
    {
        [NotNullValidator]
        public string Id { get; set; }
        [NotNullValidator]
        public string CustomerName { get; set; }
        [NotNullValidator]
        public string ContactName { get; set; }
        public string CustomerPhomeNum { get; set; }
        public string CustomerQQ { get; set; }
        public string CustomerFaxNum { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerEmail { get; set; }
        [NotNullValidator]
        public decimal CustomerInitOwing { get; set; }
        [NotNullValidator]
        public decimal CustomerNowOwing { get; set; }
        [NotNullValidator]
        public DateTime Created { get; set; }
        [NotNullValidator]
        public int Status { get; set; }
        public string OtherInfo { get; set; }
    }
}
