using System.Net;
using Application.Common;
using FluentValidation.Results;

namespace Application.Helpers;

public static class ValidationResultHelper
{
    public static ErrorResult GetRequestError(IEnumerable<ValidationFailure> failures)
    {
        var errors = failures
            .Select(x => new Error(x.ErrorMessage, x.ErrorCode))
            .ToList();

        return new ErrorResult("Request error", "Request error", HttpStatusCode.BadRequest, errors);
    }

}