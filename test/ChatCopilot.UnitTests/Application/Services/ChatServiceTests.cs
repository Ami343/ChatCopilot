using Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Moq;

namespace ChatCopilot.UnitTests.Application.Services;

public class ChatServiceTests
{
    private readonly Mock<IKernel> _kernelMock = new();
    private readonly Mock<ILogger<ChatService>> _loggerMock = new();

    [Fact]
    public async Task Ask_WhenCalledWithGivenParam_ShouldCallKernelApi()
    {
        // Arrange
        SetupKernel();
        var sut = GetSut();
        // Act
        _ = await sut.Ask(prompt: string.Empty);
        // Assert
        _kernelMock.Verify();
    }

    private void SetupKernel()
    {
        _kernelMock.Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<ISKFunction[]>()))
            .ReturnsAsync(new KernelResult())
            .Verifiable(Times.Once);
    }

    private ChatService GetSut() => new(_kernelMock.Object, _loggerMock.Object);
}