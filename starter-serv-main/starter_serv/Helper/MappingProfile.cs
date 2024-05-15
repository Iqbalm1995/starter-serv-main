using AutoMapper;
using starter_serv.Models;
using starter_serv.ViewModel.Users;

namespace starter_serv.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsrUser, UsersViewModel>();
        }
    }
}
