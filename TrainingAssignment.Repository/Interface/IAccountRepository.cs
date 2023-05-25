using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Models;
using TrainingAssignment.Entities.ViewModels;

namespace TrainingAssignment.Repository.Interface
{
    public interface IAccountRepository
    {
        public User Login(LoginViewModel model);
        public User Registration(RegisterViewModel model);
        
        public List<Country> getdetails();
        public bool IsValidUserEmail(RegisterViewModel model);
        public List<State> getstates(long country);
        public List<City> getcity(long states);

    }
}
