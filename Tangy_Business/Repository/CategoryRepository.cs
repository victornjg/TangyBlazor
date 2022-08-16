using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            category.CreatedDate = DateTime.Now;

            _db.Category.Add(category);
            _db.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public int Delete(long categoryId)
        {
            var category = _db.Category.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                _db.Category.Remove(category);
                return _db.SaveChanges();
            }
            return 0;
        }

        public CategoryDTO Get(long categoryId)
        {
            var category = _db.Category.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Category.ToList());
        }

        public CategoryDTO Update(CategoryDTO categoryDTO)
        {
            var category = _db.Category.FirstOrDefault(c => c.Id == categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                _db.Category.Update(category);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return categoryDTO;
        }
    }
}
