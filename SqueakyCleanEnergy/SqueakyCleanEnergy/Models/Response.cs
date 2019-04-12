using System;
using System.Collections.Generic;
using System.Text;

namespace SqueakyCleanEnergy.Models
{
    // Class to bo used as a Response for the services
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
