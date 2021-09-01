using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.DbModel;
using EPM.Repository.Base;

namespace EPM.Repository.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext dbContext)
      : base(dbContext, dbContext.Menus)
        {

        }
    }
}
