using AutoMapper;
using Nibo.App.ViewModels;
using Nibo.Business.Models;

namespace Nibo.App.Models
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}
