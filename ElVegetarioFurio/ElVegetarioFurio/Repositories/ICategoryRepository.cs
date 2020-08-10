using ElVegetarioFurio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElVegetarioFurio.Repositories
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);

        Category CreateCategory(Category category);

        Category UpdateCategory(Category category);

        void DeleteCategory(int id);

    }
}

