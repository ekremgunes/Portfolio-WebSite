using FluentValidation;
using Portfolio.Business.Interfaces;
using Portfolio.Dtos;


namespace Portfolio.Business.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IValidator<AppUserLoginDto> _loginValidator;
        private readonly IValidator<AppUserRegisterDto> _registerValidator;

        public AppUserService(IValidator<AppUserLoginDto> loginValidator, IValidator<AppUserRegisterDto> registerValidator)
        {
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        public bool LoginisValid(AppUserLoginDto dto) => _loginValidator.Validate(dto).IsValid;

        public bool RegisterisValid(AppUserRegisterDto dto) => _registerValidator.Validate(dto).IsValid;
    
    }
}
