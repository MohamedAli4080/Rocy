using System.Collections.Generic;

namespace Rocy.Models.ViewModels
{
    public class ProductUserVm
    {
    public ApplicationUser User { get; set; }
    public IEnumerable<Product> ProductList { get; set; }
    
    
        
        
    }
}