namespace Application.Subcategory.Model.Error;

public static class SubcategoryError
{
    public static Domain.Common.Result.Error SubcategoryNotFound => new ("Subcategory_0", "SubcategoryNotFound");
}