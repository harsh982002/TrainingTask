using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAssignment.Repository.Interface
{
    public interface IAllRepository
    {
        public IAccountRepository accountRepository { get; set; }

        public IListRepository listRepository { get; set; }
    }
}
