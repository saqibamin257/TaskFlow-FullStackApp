using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Localization
{
    public static class ErrorKeys
    {
        //organization
        public const string OrganizationNotFound =
            "error.organization.notfound";

        public const string OrganizationSlugAlreadyExists =
            "error.organization.slug.exists";

        public const string OrganizationNameAlreadyExists =
            "error.organization.name.exists";

        public const string Unauthorized =
            "error.unauthorized";    

        public const string InternalServerError =
            "internal.server.error";
        
        public const string OrganizationAccessDenied = 
            "error.organization.access.denied";
    }
}
