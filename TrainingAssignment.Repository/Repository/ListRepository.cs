using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Data;
using TrainingAssignment.Entities.Models;
using TrainingAssignment.Repository.Interface;

namespace TrainingAssignment.Repository.Repository
{
    public class ListRepository : IListRepository
    {

        private readonly TrainingContext _db;
        public ListRepository(TrainingContext db)
        {
            _db = db;
        }

        //get all the countries in register page
        public List<Country> GetInitialDetail()
        {
            return _db.Countries.ToList();
        }

        //get all the states from database
        public List<State> GetStates(long country)
        {
            var States = _db.States.Where(x => x.CountryId == country).ToList();
            return States;
        }

        //get all the city from database
        public List<City> GetCity(long states)
        {
            var City = _db.Cities.Where(x => x.StateId == states).ToList();
            return City;
        }
    }
}
