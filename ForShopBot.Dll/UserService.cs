


using AutoMapper;
using ForShopBot.Core.Dtos;
using ForShopBot.Core.InputModels;
using ForShopBot.DAL;

namespace ForShopBot.Bll
{
    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        private Mapper _mapper;

        public UserService()
        {
            UserRepository = new UserRepository();

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public void AddUser(UserInputModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            UserRepository.AddUser(userDto.Name);
        }


    }
}
