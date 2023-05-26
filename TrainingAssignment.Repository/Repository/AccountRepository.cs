using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Data;
using TrainingAssignment.Entities.Models;
using TrainingAssignment.Entities.ViewModels;
using TrainingAssignment.Repository.Interface;

namespace TrainingAssignment.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TrainingContext _db;

        public AccountRepository(TrainingContext db)
        {
            _db = db;
        }

        //Method to match the mailid with the database
        public User Login(UserLoginModel model)
        {
            User user = _db.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()));
            return user;
        }

        //for not registering again with same Email
        public bool IsValidUserEmail(UserRegisterModel model)
        {
            var user = _db.Users.Any(x => x.Email.Equals(model.Email.ToLower()));
            if (!user)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Registration Method
        public User Registration(UserRegisterModel model)
        {
            string secpass = BCrypt.Net.BCrypt.HashPassword(model.Password);
            User user = new User();
            user.Username = model.FirstName;
            user.Surname = model.SurName;
            user.Phonenumber = model.PhoneNumber;
            user.Email = model.Email;
            user.Password = secpass;
            user.CountryId = model.CountryId;
            user.StateId = model.StateId;
            user.CityId = model.CityId;
            user.Gender = model.GenderId;
            user.Avatar = HelperRepository.UserAvatar(model.avatar);
            var entry = _db.Users.Add(user);
            _db.SaveChanges();
            return entry.Entity;
        }

    }
}
