using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Moq;
using WebApi.Models.Request;
using WebApi.Services;

namespace ChatCopilot.UnitTests.WebApi.Services;

public class ChatServiceTests
{
    private readonly Mock<IKernel> _kernelMock = new();

    [Fact]
    public async Task Ask_WhenCalledWithGivenParam_ShouldCallKernelApi()
    {
        // Arrange
        SetupKernel();
        var sut = GetSut();
        // Act
        var result = await sut.Ask(new AskRequest { Input = string.Empty });

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

    private ChatService GetSut() => new(_kernelMock.Object);
}