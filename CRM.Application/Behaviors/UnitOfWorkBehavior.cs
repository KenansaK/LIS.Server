using Kernal.Contracts;
using CRM.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CRM.Application.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>
 : IPipelineBehavior<TRequest, TResponse>
 where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!typeof(TRequest).Name.EndsWith("Request"))
        {

            return await next();
        }
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();

            await _unitOfWork.SaveChanges();

            transactionScope.Complete();

            return response;
        }
    }
}
