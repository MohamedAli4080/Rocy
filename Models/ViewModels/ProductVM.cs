
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rocy.Models;
namespace Rocy.Models.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ApplicationType { get; set; }
        
        
        
        
    }
}