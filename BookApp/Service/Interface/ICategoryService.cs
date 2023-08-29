using BookApp.Models;
using BookApp.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookApp.Service.Interface;

public interface ICategoryService
{
    Task<BaseResponseModel> CreateCategory(CreateCategoryViewModel createCategoryDto);
    Task<BaseResponseModel> DeleteCategory(string categoryId);
    Task<BaseResponseModel> UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto);
    Task<CategoryResponseModel> GetCategory(string categoryId);
    Task<CategoriesResponseModel> GetAllCategory();
    Task<IEnumerable<SelectListItem>> SelectCategories();
}
