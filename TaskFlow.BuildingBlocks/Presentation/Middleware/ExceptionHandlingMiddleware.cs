using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.Json;
using TaskFlow.BuildingBlocks.Presentation.Contracts;


namespace TaskFlow.BuildingBlocks.Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) 
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception) 
            {
                _logger.LogError(
                                exception,
                                "Unhandled exception occurred");

                await HandleExceptionAsync(context, exception);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception) 
        {
            context.Response.ContentType = "application/json";
            switch (exception) 
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;                    

                    var validationResponse =
                        new ValidationErrorResponse
                        {
                            Errors = validationException.Errors
                            .Select(error => new ValidationError
                            {
                                Field = error.PropertyName,
                                Code = error.ErrorMessage
                            })
                            .ToList()
                        };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var response = new ErrorResponse
                    {
                        Error = "internal.server.error"
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    break;
            }
        }
    }
}
