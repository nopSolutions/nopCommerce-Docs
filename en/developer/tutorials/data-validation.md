---
title: Data Validation
uid: en/developer/tutorials/data-validation
author: git.AndreiMaz
contributors: git.exileDev
---

# Data Validation

Data validation is the process of ensuring that a program operates on clean, correct and useful data. Most .NET developers use Data Annotation Validators. But nopCommerce uses Fluent Validation. It's a small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules for your business objects. You have to complete two steps in order to add a validation to some models in nopCommerce:

1. Create a class derived from AbstractValidator class and put all required logic there. See the source code below to get an idea:

    ```csharp
    public class AddressValidator : BaseNopValidator<AddressModel>
    {
        public AddressValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizationService.GetResource("Admin.Address.Fields.FirstName.Required"))
            .When(x => x.FirstNameEnabled && x.FirstNameRequired);
        }
    }
    ```

1. Annotate your model class with the ValidatorAttribute. Refer to the example below for guidance.

    ```csharp
    [Validator(typeof(AddressValidator))]
    public partial class AddressModel : BaseNopEntityModel
    {
      //...
    }
    ```

    ASP.NET Core will execute the appropriate validator when a view model is posted to a controller.
