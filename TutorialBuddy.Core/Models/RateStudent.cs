﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class RateStudent : BaseEntity
    {
        public int Rate { get; set; }
        public Session Session { get; set; }
    }
}
