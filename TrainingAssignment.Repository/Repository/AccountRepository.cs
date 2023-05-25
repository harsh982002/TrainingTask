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

        //get all the countries in register page
        public List<Country> getdetails()
        {
            return _db.Countries.ToList();
        }

        //Method to match the mailid with the database
        public User Login(LoginViewModel model)
        {
            User user = _db.Users.FirstOrDefault(c=>c.Email.Equals(model.Email.ToLower()));
            return user;
        }

        //for not registering again with same Email
        public bool IsValidUserEmail(RegisterViewModel model)
        {
            try
            {
                User? user = _db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if (user == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Registration Method
        public User Registration(RegisterViewModel model)
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
            user.Gender = model.gender;
            user.Avatar = "abcd";
            using (var stream = model.avatar?.OpenReadStream())
            {
                var bytes = new byte[model.avatar.Length];
                stream?.Read(bytes, 0, (int)model.avatar.Length);
                var base64string = Convert.ToBase64String(bytes);
                user.Avatar = "data:image/png;base64," + base64string;
            }
            
            var entry = _db.Users.Add(user);
            _db.SaveChanges();
            return entry.Entity;
        }
        //get all the states from database
        public List<State> getstates(long country)
        {
            var states = _db.States.Where(x=>x.CountryId == country).ToList();
            return states;
        }
        //get all the city from database
        public List<City> getcity(long states)
        {
            var city = _db.Cities.Where(x => x.StateId == states).ToList();
            return city;
        }
    }
}
