using CoreAbstractions.Models;
using System.Threading.Tasks;

namespace ServerAbstractions.Validators
{
    public interface IUserValidator
    {
		Task ValidateAddAsync(UserAdd user);
    }
}
