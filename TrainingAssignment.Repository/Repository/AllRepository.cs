using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Entities.Data;
using TrainingAssignment.Repository.Interface;

namespace TrainingAssignment.Repository.Repository
{
    public class AllRepository : IAllRepository
    {
        private readonly TrainingContext _db;

        public AllRepository(TrainingContext db)
        {
            _db = db;



            accountRepository = new AccountRepository(_db);

            listRepository = new ListRepository(_db);
        }
        public IListRepository listRepository { get; set; }

        public IAccountRepository accountRepository { get; set; }
    }
}
