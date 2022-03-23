---
title: Data Validation
uid: en/developer/tutorials/data-validation
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Data Validation

Data validation is the process of ensuring that a program operates on clean, correct, and useful data. Most .NET developers use `Data Annotation Validators`. But nopCommerce uses **`Fluent Validation`**. It's a small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules for your business objects. To add validation for some models in nopCommerce, you need to do the following:

Create a class derived from the `AbstractValidator` class and put all required logic there.

```csharp
public partial class AddressValidator : BaseNopValidator<AddressModel>
{
    public AddressValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("AddressFields.FirstName.Required"));            
    }
}
```

ASP.NET Core will execute the appropriate validator when a view model is posted to a controller.
