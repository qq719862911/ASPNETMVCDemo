﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.FileUpload
{
    public class FileModel
    {
        public IQueryable<FileItem> List { get; set; }
        public FileSearch Search { get; set; }
    }
}
