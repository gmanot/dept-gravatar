﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.Models.GraphAPI
{
    public class ResponsePaging
    {
        public ResponsePagingCursors Cursors { get; set; }
        public string Next { get; set; }
    }
}
