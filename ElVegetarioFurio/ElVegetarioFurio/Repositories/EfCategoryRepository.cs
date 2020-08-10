using ElVegetarioFurio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElVegetarioFurio.Repositories
{
    //Nach Eingabe ICategoryRepository STRG + . drücken und Schnittselle anlegen lassen
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly VegiContext _vegiContext;

        public EfCategoryRepository(VegiContext vegiContext)
        {
            // strg + . neues schreibgeschütztes Feld bei _vegiContext
            _vegiContext = vegiContext;
        }
        public Category CreateCategory(Category category)
        {
            _vegiContext.Categories.Add(category);
            _vegiContext.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _vegiContext.Categories.Find(id);
            _vegiContext.Categories.Remove(category);
            _vegiContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _vegiContext.Categories
                              // performance optimierung
                              .AsNoTracking()
                              .Include(x => x.Dishes)
                              .ToList();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _vegiContext.Categories.Find(id);
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var categoryToUpdate = _vegiContext.Categories.Find(category.Id);
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            _vegiContext.SaveChanges();
            return categoryToUpdate;
        }
    }
}
