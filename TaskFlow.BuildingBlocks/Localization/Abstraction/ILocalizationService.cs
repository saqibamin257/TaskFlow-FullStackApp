using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Localization.Abstraction
{
    public interface ILocalizationService
    {
        string GetString(string key);
    }
}
