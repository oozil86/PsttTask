using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record DeleteCompanyCommand(Guid reference) : IRequest<bool>;

public class DeleteCompanyCommandHandler(IGetCompanySpecification getCompanySpecification
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork) : IRequestHandler<DeleteCompanyCommand, bool>
{

    public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        getCompanySpecification.SetCompanyReference(request.reference);
        var Company = await getCompanySpecification.Query(cancellationToken) ?? throw new Exception("This Company Not Found.");
        Company.SoftDelete();
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return true;
    }
}
