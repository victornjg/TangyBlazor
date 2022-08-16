using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            category.CreatedDate = DateTime.Now;

            _db.Category.Add(category);
            await _db.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public async Task<int> Delete(long categoryId)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category != null)
            {
                _db.Category.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(long categoryId)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category != null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(await _db.Category.ToListAsync());
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                _db.Category.Update(category);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return categoryDTO;
        }
    }
}
