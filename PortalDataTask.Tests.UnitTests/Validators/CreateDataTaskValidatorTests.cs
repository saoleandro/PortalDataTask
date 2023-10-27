using FluentValidation.TestHelper;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Validators;

namespace PortalDataTask.Tests.UnitTests.Validators;

public class CreateDataTaskValidatorTests
{
    private readonly CreateDataTaskValidator _validator;

	public CreateDataTaskValidatorTests()
	{
		_validator= new CreateDataTaskValidator();
	}

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Name_ShouldHaveValidationError_When_Empty(string description)
    {
        //Arrange
        var createDataTaskRequest = new CreateDataTaskRequest();
        createDataTaskRequest.Description = description;

        //Act
        var result = _validator.TestValidate(createDataTaskRequest);

        //Assert
        result.ShouldHaveValidationErrorFor(e => e.Description);
    }

    [Theory]
    [InlineData("teste")]
    public void Name_ShouldHaveValidationError_When_Filled(string description)
    {
        //Arrange
        var createDataTaskRequest = new CreateDataTaskRequest();
        createDataTaskRequest.Description = description;

        //Act
        var result = _validator.TestValidate(createDataTaskRequest);

        //Assert
        result.ShouldNotHaveValidationErrorFor(e => e.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ValidateDate_ShouldHaveValidationError_When_Empty(string validateDate)
    {
        //Arrange
        var createDataTaskRequest = new CreateDataTaskRequest();
        createDataTaskRequest.ValidateDate = string.IsNullOrWhiteSpace(validateDate) ? (DateTime?)null : DateTime.Parse(validateDate);

        //Act
        var result = _validator.TestValidate(createDataTaskRequest);

        //Assert
        result.ShouldHaveValidationErrorFor(e => e.ValidateDate);
    }

    [Theory]
    [InlineData("2000-1-1")]
    public void ValidateDate_ShouldHaveValidationError_When_Filled(string validateDate)
    {
        //Arrange
        var createDataTaskRequest = new CreateDataTaskRequest();
        createDataTaskRequest.ValidateDate = DateTime.Parse(validateDate);

        //Act
        var result = _validator.TestValidate(createDataTaskRequest);

        //Assert
        result.ShouldNotHaveValidationErrorFor(e => e.ValidateDate);
    }
}
