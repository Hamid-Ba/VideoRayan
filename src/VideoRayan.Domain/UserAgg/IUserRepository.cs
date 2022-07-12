using System;
using Framework.Domain;

namespace VideoRayan.Domain.UserAgg
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetBy(string mobile);
	}
}