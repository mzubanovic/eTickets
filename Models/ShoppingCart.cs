using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
