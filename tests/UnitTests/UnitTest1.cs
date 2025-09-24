using LabCoreSoft.Application.Commands;
using LabCoreSoft.Application.Handlers;
using LabCoreSoft.Application.Interfaces;
using LabCoreSoft.Domain.Entities;
using LabCoreSoft.Domain.Enums;
using Moq;

namespace LabCoreSoft.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Patient_ShouldCreate_WhenAgeIsValid()
    {
        // Arrange
        var birthDate = DateTime.Now.AddYears(-25);

        // Act
        var patient = new Patient("John", "Doe", DocumentType.CedulaCiudadania, "123456789", birthDate, "Bogota", "3001234567", "john@example.com");

        // Assert
        Assert.Equal("John", patient.FirstName);
        Assert.Equal(DocumentType.CedulaCiudadania, patient.DocumentType);
        Assert.True(patient.IsActive);
    }

    [Fact]
    public void Patient_ShouldThrowException_WhenAgeIsInvalid()
    {
        // Arrange
        var birthDate = DateTime.Now.AddYears(-130); // Too old

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Patient("John", "Doe", DocumentType.CedulaCiudadania, "123456789", birthDate, "Bogota", null, null));
    }

    [Fact]
    public async Task RegisterPatientCommandHandler_ShouldReturnId_WhenPatientIsRegistered()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        mockRepo.Setup(r => r.GetByDocumentAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Patient?)null);
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Patient>())).Returns(Task.CompletedTask)
                .Callback<Patient>(p => p.GetType().GetProperty("Id")?.SetValue(p, 1));

        var handler = new RegisterPatientCommandHandler(mockRepo.Object);
        var command = new RegisterPatientCommand
        {
            FirstName = "Jane",
            LastName = "Smith",
            DocumentType = DocumentType.CedulaCiudadania,
            DocumentNumber = "987654321",
            BirthDate = DateTime.Now.AddYears(-30),
            City = "Medellin"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Patient>()), Times.Once);
    }
}
