using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Presentation.Contracts
{
    public class ValidationError
    {
        public string Field { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
