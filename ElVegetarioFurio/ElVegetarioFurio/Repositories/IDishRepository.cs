using ElVegetarioFurio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElVegetarioFurio.Repositories
{
    public interface IDishRepository
    {
        IEnumerable<Dish> GetDishes();
        Dish GetDishById(int id);
        Dish CreateDish(Dish dish);
        Dish UpdateDish(Dish dish);
        void DeleteDish(int id);
    }
}
