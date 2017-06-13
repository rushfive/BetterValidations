using CoreAbstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerAbstractions.Services
{
    public interface IUserService
    {
		Task<Guid> AddAsync(UserAdd user);
    }
}
