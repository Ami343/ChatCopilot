using System.Net;
using Application.Behaviours;
using Application.Common;
using Application.Common.Validators;
using Application.Helpers;
using Domain.Primitives;
using Domain.Repositories;
using FluentValidation;

namespace Application.Chats.Commands.Chat;

public class ChatCommandValidator : AbstractValidator<ChatCommand>, IRequestValidator<ChatCommand>
{
    private readonly IChatSessionRepository _chatSessionRepository;

    public ChatCommandValidator(IChatSessionRepository chatSessionRepository)
    {
        RuleFor(x => x.Input)
            .NotEmpty()
            .WithMessage("Input cannot be empty.")
            .WithErrorCode("Input");

        _chatSessionRepository = chatSessionRepository;
    }

    public async Task<Maybe<ErrorResult>> ValidateInput(ChatCommand input)
    {
        var validationResult = await ValidateAsync(input);
        if (!validationResult.IsValid)
            return Maybe<ErrorResult>.From(ValidationResultHelper.GetRequestError(validationResult.Errors));

        var chatSessionExistValidationResult = await ValidateChatSessionExist(input.ChatSessionId);

        if (chatSessionExistValidationResult.HasValue)
            return chatSessionExistValidationResult;

        return Maybe<ErrorResult>.None;
    }

    private async Task<Maybe<ErrorResult>> ValidateChatSessionExist(string chatSessionId)
    {
        var chatSession = await _chatSessionRepository.GetById(chatSessionId);

        if (!chatSession.HasValue)
            return Maybe<ErrorResult>.From(new ErrorResult(
                "Chat session doesn't exist",
                "Chat session doesn't exist",
                HttpStatusCode.BadRequest));

        return Maybe<ErrorResult>.None;
    }
}