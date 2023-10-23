using Application.Options;
using Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Moq;

namespace ChatCopilot.UnitTests.Application.Services;

public class ChatServiceTests
{
    private readonly Mock<IKernel> _kernelMock = new();
    private readonly Mock<ILogger<ChatService>> _loggerMock = new();
    private readonly Mock<IOptions<PromptOptions>> _promptOptionsMock = new();

    [Fact]
    public async Task Ask_WhenCalledWithGivenParam_ShouldCallKernelApi()
    {
        // Arrange
        Setup();
        var sut = GetSut();
        // Act
        _ = await sut.GetBotResponse(
            prompt: string.Empty,
            chatSessionId: Guid.Empty);
        
        // Assert
        _kernelMock.Verify();
    }

    private void Setup()
    {
        _kernelMock.Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<ISKFunction[]>()))
            .ReturnsAsync(new KernelResult())
            .Verifiable(Times.Once);

        _promptOptionsMock.Setup(x => x.Value)
            .Returns(new PromptOptions());
    }

    private ChatService GetSut()
        => new(
            _kernelMock.Object,
            _loggerMock.Object,
            _promptOptionsMock.Object);
}