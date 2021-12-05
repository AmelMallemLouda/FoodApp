using fooddeliveryapp.Data;
using fooddeliveryapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddelievryapp.Services
{
    public class FoodService
    {
        private readonly Guid _userId;
        public FoodService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFood(FoodCreate model)
        {
            var entity = new Food()
            {
                OwnerId = _userId,
                FoodName = model.FoodName,
                FoodPrice = model.FoodPrice,
                FoodIngridients = model.FoodIngridients

            };
            using (var ctx = new ApplicationDbContext())// Access database
            {
                ctx.Foods.Add(entity);// access items Table and add items
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<FoodListItem> GetFood()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                            .Foods
                            .Where(e => e.OwnerId == _userId)
                            .Select(
                                e =>
                                    new FoodListItem
                                    {
                                        FoodId = e.FoodId,
                                        FoodName = e.FoodName,
                                        FoodPrice = e.FoodPrice,

                                        FoodIngridients = e.FoodIngridients




                                    }
                    );
                return query.ToArray();



            }

        }
    }
}