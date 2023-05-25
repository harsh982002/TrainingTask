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
        public User Login(UserLoginModel model);
        public User Registration(UserRegisterModel model);
        public bool IsValidUserEmail(UserRegisterModel model);
       
    }
}
