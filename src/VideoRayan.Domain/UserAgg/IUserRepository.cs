using System;
using Framework.Domain;
using VideoRayan.Application.Contract.UserAgg;

namespace VideoRayan.Domain.UserAgg
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetBy(string mobile);
        Task<EditUserDto> GetDetailForEditBy(Guid id);
    }
}