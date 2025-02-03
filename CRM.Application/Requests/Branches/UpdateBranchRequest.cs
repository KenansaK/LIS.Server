using CRM.Application.Commands;
using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;

public class UpdateBranchRequest : IRequest<Result<BranchDto>>
{
    public required long Id { get; set; }
    public required BranchModel Model { get; set; }
}
public class UpdateBranchRequestHandler(Dispatcher dispatcher) : IRequestHandler<UpdateBranchRequest, Result<BranchDto>>
{
    public async Task<Result<BranchDto>> Handle(UpdateBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await dispatcher.DispatchAsync(new GetBranchQuery { Id = request.Id });

        if (branch == null)
        {
            return Result.NotFound("Branch not found");
        }

        branch.AllowedCODCurencies = request.Model.AllowedCODCurencies;
        branch.BillingExternalCode = request.Model.BillingExternalCode;
        branch.BranchCode = request.Model.BranchCode;
        branch.BranchName = request.Model.BranchName;
        branch.ConsolidationQuery = request.Model.ConsolidationQuery;
        branch.CurrencyCode = request.Model.CurrencyCode;
        branch.StatusId = request.Model.Status;
        branch.CustomerCode = request.Model.CustomerCode;
        branch.DimensionUnit = request.Model.DimensionUnit;
        branch.EORI = request.Model.EORI;
        branch.ExternalCode = request.Model.ExternalCode;
        branch.GPI = request.Model.GPI;
        branch.IOSS = request.Model.IOSS;
        branch.LicenseRegistrationNumber = request.Model.LicenseRegistrationNumber;
        branch.ProductService = request.Model.ProductService;
        branch.ProductTypeCode = request.Model.ProductTypeCode;
        branch.ShipmentService = request.Model.ShipmentService;
        branch.SupplierCode = request.Model.SupplierCode;
        branch.VATNumber = request.Model.VATNumber;
        branch.WeightUnit = request.Model.WeightUnit;
        branch.CustomerId = request.Model.CustomerId;

        await dispatcher.DispatchAsync(new UpdateBranchCommand { Branch = branch });

        return Result.Success(branch.ToDto()!);
    }
}
