using LoginWebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApi.Responses
{
    public class StatusModel
    {
        public Status Status { get; set; }
        public String Message { get; set; }
    }
}
