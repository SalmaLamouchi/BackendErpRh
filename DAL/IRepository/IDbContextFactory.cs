
using Core.Model;

namespace DAL
{
    public interface IDbContextFactory
    {
        ErpRhDbContext DbContext { get; }
    }
}
