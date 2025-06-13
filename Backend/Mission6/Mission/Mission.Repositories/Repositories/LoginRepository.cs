using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Entity.Entities;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mission.Repositories
{
    public class LoginRepository(MissionDbContext cIDbContext) : ILoginRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.User
                .FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address Not Found." };
            }
            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Incorrect Password." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName ?? string.Empty,
                LastName = existingUser.LastName ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                EmailAddress = existingUser.EmailAddress,
                UserType = existingUser.UserType,
                UserImage = existingUser.UserImage != null ? existingUser.UserImage : string.Empty,
                Message = "Login Successfully"
            };
        }


        public async Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest)
        {
            var isEmailExist = await _cIDbContext.User.FirstOrDefaultAsync(u => u.EmailAddress.ToLower() == registerUserRequest.EmailAddress.ToLower());

            if (isEmailExist != null)  throw new Exception("User already exist");

            User user = new User()
            {
              FirstName = registerUserRequest.FirstName,
              LastName = registerUserRequest.LastName,
              EmailAddress=registerUserRequest.EmailAddress,
              Password = registerUserRequest.Password,
              PhoneNumber   =registerUserRequest.PhoneNumber,
              UserType  =registerUserRequest.UserType,
              CreatedDate = DateTime.UtcNow,
            };

            await _cIDbContext.User.AddAsync(user);
            await _cIDbContext.SaveChangesAsync();
            return "User registered!";
        }
        public UserResponseModel LoginUserDetailById(int id)
        {
            var userDetail = _cIDbContext.User
                .Where(u => u.Id == id && !u.IsDeleted)
                .Select(user => new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress,
                    UserType = user.UserType,
                })
                .FirstOrDefault() ?? throw new Exception("User not found.");

            return userDetail;
        }

        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var user = _cIDbContext.User.Where(x => x.Id == requestModel.UserId).FirstOrDefault();

                if (user == null) throw new Exception("Not Found!");

                var userDetails = _cIDbContext.UserDetails.Where(x => x.UserId == requestModel.UserId).FirstOrDefault();

                if (userDetails == null)
                {
                    // Add User Details
                    UserDetail userDetail = new UserDetail()
                    {
                        UserId = requestModel.UserId,
                        Availability = requestModel.Avilability,
                        CityId = requestModel.CityId,
                        CountryId = requestModel.CountryId,
                        Department = requestModel.Department,
                        EmployeeId = requestModel.EmployeeId,
                        LinkedInUrl = requestModel.LinkdInUrl,
                        Manager = requestModel.Manager,
                        MyProfile = requestModel.MyProfile,
                        MySkills = requestModel.MySkills,
                        Surname = requestModel.Surname,
                        Name = requestModel.Name,
                        UserImage = requestModel.UserImage,
                        WhyIVolunteer = requestModel.WhyIVolunteer,
                        Status = requestModel.Status,
                        Title = requestModel.Title,
                        
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    await _cIDbContext.UserDetails.AddAsync(userDetail);
                } else
                {
                    // Update User Details
                    userDetails.UserId = requestModel.UserId;
                    userDetails.Availability = requestModel.Avilability;
                    userDetails.CityId = requestModel.CityId;
                    userDetails.CountryId = requestModel.CountryId;
                    userDetails.Department = requestModel.Department;
                    userDetails.EmployeeId = requestModel.EmployeeId;
                    userDetails.LinkedInUrl = requestModel.LinkdInUrl;
                    userDetails.Manager = requestModel.Manager;
                    userDetails.MyProfile = requestModel.MyProfile;
                    userDetails.MySkills = requestModel.MySkills;
                    userDetails.Surname = requestModel.Surname;
                    userDetails.Name = requestModel.Name;
                    userDetails.UserImage = requestModel.UserImage;
                    userDetails.WhyIVolunteer = requestModel.WhyIVolunteer;
                    userDetails.Status = requestModel.Status;
                    userDetails.Title = requestModel.Title;

                    userDetails.ModifiedDate = DateTime.Now;

                    _cIDbContext.UserDetails.Update(userDetails);
                }

                user.FirstName = requestModel.Name;
                user.LastName = requestModel.Surname;

                _cIDbContext.User.Update(user);
                await _cIDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

    }
}
