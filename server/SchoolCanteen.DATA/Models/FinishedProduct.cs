﻿
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class FinishedProduct
{
    [Required] public int FinishedProductId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    [Required] public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    [MaxLength (100)]
    [Required] public string Name { get; set; }
    public float Costs { get; set; } = 0;
    public float Profit { get; set; } = 0;
    public float Price { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    //public List<ProductFinishedProduct> ProductFinishedProducts { get; set; }
    //public List<Product> Products { get; set;} = new List<Product>();
    public List<ProductStorage> ProductStorages { get; set; } = new List<ProductStorage>();

}
