﻿using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Contracts.DTO.Supplier
{
    public class SupplierStatusDTO
    {
        [NotNullValidator]
        public string Id { get; set; }
        [NotNullValidator]
        public int Status { get; set; }
    }
}
