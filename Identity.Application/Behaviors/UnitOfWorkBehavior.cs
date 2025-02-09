using Kernal.Contracts;
using MediatR;
using System.Transactions;

namespace Identity.Application.Behaviors;
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
