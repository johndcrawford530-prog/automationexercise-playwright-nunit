

namespace AutomationExerciseDemo.UI.Models
{
    // CartItem class stores details of products displayed in the cart
    public class CartItem
    {
        public string Name{get; set;}
        public string Description{get; set;}
        public decimal Price{get; set;}
        public int Quantity{get; set;}
        public decimal Total{get; set;}

    }
}