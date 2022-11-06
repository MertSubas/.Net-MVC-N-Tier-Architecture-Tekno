using Project.BLL.Designpattern.GenericRepository.BaseRepository;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Designpattern.GenericRepository.ConcRep
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public override void Add(Category item)
        {
            base.Add(item);
        }


    }
}
