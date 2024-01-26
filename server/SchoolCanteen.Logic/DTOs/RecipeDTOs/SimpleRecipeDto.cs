﻿using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class SimpleRecipeDto
{
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public float Quantity { get; set; } = 0;
    public List<SimpleRecipeDetailsDto> Details { get; set; }
}