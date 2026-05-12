using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskFlow.BuildingBlocks.Models;

namespace TaskFlow.BuildingBlocks.Security.Abstraction
{
    public interface ITokenValidator
    {
        ClaimsPrincipal? Validate(string token);
    }
}
