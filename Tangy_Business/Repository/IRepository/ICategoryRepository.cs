using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public CategoryDTO Create(CategoryDTO categoryDTO);

        public CategoryDTO Update(CategoryDTO categoryDTO);

        public int Delete(long categoryId);

        public CategoryDTO Get(long categoryId);

        public IEnumerable<CategoryDTO> GetAll();
    }
}
