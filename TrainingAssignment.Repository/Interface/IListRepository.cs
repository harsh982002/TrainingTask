using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Models;

namespace TrainingAssignment.Repository.Interface
{
    public interface IListRepository
    {
        public List<State> GetStates(long country);
        public List<City> GetCity(long states);
        public List<Country> GetInitialDetail();
    }
}
