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

        public FoodDetails GetFoodById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                            .Foods
                            .Single(e => e.FoodId == id && e.OwnerId == _userId);
                return new FoodDetails
                {
                    FoodId = entity.FoodId,
                    FoodName = entity.FoodName,
                    FoodPrice = entity.FoodPrice,
                    FoodIngridients = entity.FoodIngridients,
                };
            }
        }

        public bool UpdateFood(FoodEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                          .Foods
                          .Single(e => e.FoodId == model.FoodId && e.OwnerId == _userId);

                entity.FoodName = model.FoodName;
                entity.FoodPrice = model.FoodPrice;
                entity.FoodIngridients = model.FoodIngridients;

                return ctx.SaveChanges() == 1;

            }



        }
       
        public bool DeleteFood(int FoodId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Foods
                        .Single(e => e.FoodId == FoodId && e.OwnerId == _userId);

                ctx.Foods.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}