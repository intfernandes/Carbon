
using Core.Models;
using Core.Requests.Categories;
using Core.Responses;

namespace Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category>> CreateAsync(CreateCategoryRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
        Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
    }
}