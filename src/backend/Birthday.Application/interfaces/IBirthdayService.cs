using Birthday.Application.contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Application
{
    public interface IBirthdayService
    {
        Task<CreateBirthday.Response> Create(CreateBirthday.Request request, CancellationToken cancellationToken);
        Task Edit(EditBirthday.Request request, CancellationToken cancellationToken);
        Task Delete(DeleteBirthday.Request request, CancellationToken cancellationToken);
        Task<GetPagedBirthday.Response> GetPaged(GetPagedBirthday.Request request, CancellationToken cancellationToken);
        Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken);
        Task<GetNext.Response> GetNext(GetNext.Request request, CancellationToken cancellationToken);

    }
}
