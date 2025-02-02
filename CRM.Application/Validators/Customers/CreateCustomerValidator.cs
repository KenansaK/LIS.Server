using CRM.Application.Requests;
using FluentValidation;

namespace CRM.Application.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Model.CompanyCommercialName)
            .NotEmpty().WithMessage("Company Commercial Name is required");

        RuleFor(x => x.Model.CompanyLegalName)
            .NotEmpty().WithMessage("Company Legal Name is required");

        RuleFor(x => x.Model.BusinessType)
            .NotEmpty().WithMessage("Business Type is required");

        RuleFor(x => x.Model.CustomerCode)
            .NotEmpty().WithMessage("Customer Code is required");

        RuleFor(x => x.Model.CustomerNumber)
            .NotEmpty().WithMessage("Customer Number is required");

        RuleFor(x => x.Model.RegistrationNumber)
            .NotEmpty().WithMessage("Registration Number is required");


        RuleFor(x => x.Model.BillingExternalCode)
            .NotEmpty().WithMessage("Billing External Code is required");

        RuleFor(x => x.Model.ExternalCode)
            .NotEmpty().WithMessage("External Code is required");

        RuleFor(x => x.Model.StoreBarcodePrefix)
            .NotEmpty().WithMessage("Store Barcode Prefix is required");
    }
}

