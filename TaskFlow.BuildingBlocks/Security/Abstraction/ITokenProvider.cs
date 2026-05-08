using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Models;

namespace TaskFlow.BuildingBlocks.Security.Abstraction
{
    public interface ITokenProvider
    {
        string Generate(AuthenticatedUser user);
    }
}
