using starter_serv.Model;
using starter_serv.Models;
using starter_serv.ViewModel.Base;
using starter_serv.ViewModel.Users;

namespace starter_serv.DataProviders
{
    public interface IUsersDataProvider
    {
        public Task<List<UsrUser>> List(string? search, int limit, int page);
        public Task<int> CountFilter(string? search);
        public Task<UsrUser> GetById(int id);
        public Task<UsrUser> GetByEmail(string email);
        public Task<ListPagedResults<UsrUser>> QueryPagedList(QueryPagedFilterModel filter);
        public Task<string> AvatarWriteFile(IFormFile file);
        public Task<ReturnViewModel> Insert(UsrUser data);
        public Task<ReturnViewModel> Update(UsrUser data);
    }
}
