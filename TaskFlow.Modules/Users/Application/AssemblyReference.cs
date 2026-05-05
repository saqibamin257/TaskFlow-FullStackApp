using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application
{
    /// <summary>
    /// Marker class used to reference the Users Application assembly.
    ///
    /// This is required for MediatR registration so that all handlers
    /// (e.g., GetUsersHandler, CreateUserHandler, UpdateUserHandler)
    /// within this module are automatically discovered and registered.
    ///
    /// Instead of referencing a specific handler type, we reference this
    /// assembly to keep registration clean, stable, and module-based.
    /// </summary>
    public static class AssemblyReference
    {
    }
}
