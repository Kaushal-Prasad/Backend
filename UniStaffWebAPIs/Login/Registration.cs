﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniStaffWebAPIs.Login
{
    public class Registration
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public string Employee_Number { get; set; }

    }
}