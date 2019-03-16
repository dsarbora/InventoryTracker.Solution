using System;

namespace InventoryTracker.Models
{
    public class DishQuantity
    {
        private Dish Dish;
        private int Quantity;

        public DishQuantity(Dish dish, int quantity)
        {
            Dish = dish;
            Quantity = quantity;
        }

        public Dish GetDish() { return Dish; }

        public int GetQuantity() { return Quantity; }

        public override bool Equals(System.Object otherDishQuantity)
        {
            if(!(otherDishQuantity is DishQuantity))
            { 
                return false; 
            }
            else
            {
                DishQuantity newDishQuantity = (DishQuantity)otherDishQuantity;
                bool dishEquality = this.GetDish().Equals(newDishQuantity.GetDish());
                bool quantityEquality = this.GetQuantity().Equals(newDishQuantity.GetQuantity());
                return (dishEquality && quantityEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.GetDish().GetId().GetHashCode();
        }
    }
}