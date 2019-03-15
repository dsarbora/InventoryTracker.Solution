using System;

namespace InventoryTracker.Models
{
    public class IngredientQuantity
    {
        private  Ingredient Ingredient;
        private int Quantity;

        public  IngredientQuantity( Ingredient ingredient, int quantity)
        {
            Ingredient = ingredient;
            Quantity = quantity;
        }

        public  Ingredient GetIngredient() { return Ingredient; }

        public int GetQuantity() { return Quantity; }

        public override bool Equals(System.Object otherIngredientQuantity)
        {
            if(!(otherIngredientQuantity is  IngredientQuantity))
            { 
                return false; 
            }
            else
            {
                IngredientQuantity newIngredientQuantity = (IngredientQuantity)otherIngredientQuantity;
                bool IngredientEquality =this.GetIngredient().Equals(newIngredientQuantity.GetIngredient());
                bool quantityEquality = this.GetQuantity().Equals(newIngredientQuantity.GetQuantity());
                return ( IngredientEquality && quantityEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.GetIngredient().GetId().GetHashCode();
        }
    }
}