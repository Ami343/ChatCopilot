using Application.Common.Exceptions;
using Application.Common.Validators;
using MediatR;

namespace Application.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    private readonly IRequestValidator<TRequest>? _requestValidator;

    public ValidationBehaviour(
        IRequestValidator<TRequest>? requestValidator = null)
    {
        _requestValidator = requestValidator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_requestValidator is null)
            return await next();

        var validationErrorResult = await _requestValidator.ValidateInput(request);

        if (validationErrorResult.HasValue)
            throw new ValidationResultException(validationErrorResult.Value);

        return await next();
    }
}