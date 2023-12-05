using System.Net;
using Application.ChatSessions.Commands.Create;
using Domain.Repositories;
using FluentAssertions;
using FluentAssertions.Execution;
using IntegrationTests.Setup;

namespace IntegrationTests.ChatSessions;

public class CreateChatSessionTests : IntegrationTestsBase
{
    private const string Uri = "/chat-sessions";

    public CreateChatSessionTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task CreateChatSession_WhenCalled_ShouldCreateChatSessionAndInitialBotMessage()
    {
        // Arrange
        var request = new CreateChatSessionRequest();

        // Act
        var (httpStatusCode, result) =
            await HttpClient.PostAsync<CreateChatSessionRequest, CreateChatSessionCommandResponse>(Uri, request);

        // Assert
        using (new AssertionScope())
        {
            httpStatusCode.Should()
                .Be(HttpStatusCode.OK);

            result.InitialMessage.Should()
                .NotBeNull();

            var chatSession = await GetService<IChatSessionRepository>().GetById(result.Id);

            chatSession.HasValue.Should()
                .BeTrue();

            var chatMessage =
                (await GetService<IChatMessageRepository>().GetByChatSessionId(result.Id)).SingleOrDefault();

            chatMessage.Should()
                .NotBeNull();
        }
    }
}