using Domain.Primitives;

namespace Application.Common.Validators;

public interface IRequestValidator<in TRequest>
    where TRequest : class
{
    Task<Maybe<ErrorResult>> ValidateInput(TRequest input);
}