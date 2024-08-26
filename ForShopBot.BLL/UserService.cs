﻿using AutoMapper;
using ForShopBot.Core.Dtos;
using ForShopBot.Core.InputModels;
using ForShopBot.Core.OutputModels;
using ForShopBot.DAL;
using ForShopBot.BLL.Mappings;
using System;

namespace ForShopBot.BLL
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
            Console.WriteLine("coming");
            var userDto = _mapper.Map<UserDto>(user);

            UserRepository.AddUser(userDto.Name);
        }

        public List<UserOutputModel> GetAllUsers()
        {

            var users = UserRepository.GetAllUsers();
            var usersOutput = _mapper.Map<List<UserOutputModel>>(users);
            return usersOutput;
        }


    }
    
}
