﻿using ElVegetarioFurio.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            throw new NotImplementedException();
        }

        public void DeleteDish(int id)
        {
            throw new NotImplementedException();
        }

        public Dish GetDishById(int id)
        {
            throw new NotImplementedException();
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
