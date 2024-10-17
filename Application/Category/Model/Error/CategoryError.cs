namespace Application.Category.Model.Error;
using Domain.Common.Result;

public class CategoryError
{
    public static Error CategoryNotFound => new ("Category_0", "CategoryNotFound");
}