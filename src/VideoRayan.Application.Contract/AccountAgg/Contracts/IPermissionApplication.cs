using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IPermissionApplication
    {
        Task<IEnumerable<PermissionVM>> GetAll();
    }
}