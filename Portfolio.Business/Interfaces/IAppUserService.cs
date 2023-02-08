using Portfolio.Dtos;

namespace Portfolio.Business.Interfaces
{
    public interface IAppUserService
    {
        bool LoginisValid(AppUserLoginDto dto);
        bool RegisterisValid(AppUserRegisterDto dto);
    }
}
