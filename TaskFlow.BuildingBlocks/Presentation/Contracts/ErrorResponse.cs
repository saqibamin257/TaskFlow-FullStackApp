using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Presentation.Contracts
{
    public sealed class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
    }
}
