﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.ResourceInfo
{
    public class ResourceInfoModel
    {
        public IQueryable<ResourceInfoItem> List { get; set; }
    }
}
