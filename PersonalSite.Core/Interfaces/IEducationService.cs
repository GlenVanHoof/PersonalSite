using PersonalSite.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Core.Interfaces;

public interface IEducationService
{
    Task<IEnumerable<Education>> GetAllEducationsAsync();
    Task<Education?> GetEducationByIdAsync(int id);
    Task<Education> CreateEducationAsync(Education education);
    Task UpdateEducationAsync(Education education);
    Task DeleteEducationAsync(int id);
}