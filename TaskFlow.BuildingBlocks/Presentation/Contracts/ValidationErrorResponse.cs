using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Presentation.Contracts
{
    public sealed class ValidationErrorResponse
    {
        public List<ValidationError> Errors { get; set; } = [];
    }
}
