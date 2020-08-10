using ElVegetarioFurio.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ElVegetarioFurio.Repositories
{
    public class FileDishRepository : IDishRepository
    {
        private readonly string _path;

        public FileDishRepository(IWebHostEnvironment env)
        {
            // Path System.IO
            _path = Path.Combine(env.ContentRootPath, "data", "dishes.json");
        }
        public Dish CreateDish(Dish dish)
        {
            // Ausgabe in Liste, wenn keine vorhanden wird eine erstellt
            var dishes = GetDishes()?.ToList() ?? new List<Dish>();

            if(dishes.Count == 0)
            {
                dish.Id = 1;
            }
            else
            {
                // Dient nur zu Demo zwecken, bei gleichzeiten Zugriffen kann es hier zu problemen kommen.
                dish.Id = dishes.Max(x => x.Id) + 1;
            }

            dishes.Add(dish);

            var options = new JsonSerializerOptions
            {
                // Mit Einrückung
                WriteIndented = true
            };
            var json = System.Text.Json.JsonSerializer.Serialize(dishes, options);
            File.WriteAllText(_path, json);
            return dish;
        }

        public void DeleteDish(int id)
        {
            throw new NotImplementedException();
        }

        public Dish GetDishById(int id)
        {
            // Da es NULL sein könnte mit ? operator
            return GetDishes()?.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Dish> GetDishes()
        {
            var json = File.ReadAllText(_path);

            // Serializer beibringen nicht auf gross und kleinschreibung sowie auf angehängte kommas zu achten
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            var dishes = System.Text.Json.JsonSerializer.Deserialize<Dish[]>(json, options);

            return dishes;
        }

        public Dish UpdateDish(Dish dish)
        {
            throw new NotImplementedException();
        }
    }
}
