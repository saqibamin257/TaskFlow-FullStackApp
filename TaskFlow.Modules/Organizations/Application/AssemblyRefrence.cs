using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application
{
    /// <summary>
    /// Marker class used to reference the Organization Application assembly.
    ///
    /// This is required for MediatR registration so that all handlers
    /// (e.g., GetOrganizationHandler, CreateOrganizationHandler, UpdateOrganizationHandler)
    /// within this module are automatically discovered and registered.
    ///
    /// Instead of referencing a specific handler type, we reference this
    /// assembly to keep registration clean, stable, and module-based.
    /// </summary>
    public static class AssemblyReference
    {
    }
}
