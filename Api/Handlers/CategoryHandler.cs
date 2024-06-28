
using Api.DbContext;
using Core.Handlers;
using Core.Models;
using Core.Requests.Categories;
using Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers
{
    public class CategoryHandler(AuthDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            try
            {
                await context.Categories.AddAsync(category);

                await context.SaveChangesAsync();

                return new Response<Category>(data: category, code: 201, message: "Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(data: null, code: 500, message: $"Não foi possível criar a categoria Erro: {ex.Message}");

            }


        }

        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if (category == null)
                return new Response<Category>(data: null, code: 404, message: $"Categoria não encontrada");

            context.Categories.Remove(category);

            try
            {
                await context.SaveChangesAsync();

                return new Response<Category>(data: null, message: "Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(data: null, code: 500, message: $"Não foi possível excluir a categoria");

            }

        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {

                var query = context.Categories
            .AsNoTracking()
            .Where(c => c.UserId == request.UserId)
            .OrderBy(c => c.Title);

                var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                var response = new PagedResponse<List<Category>>(
                     categories,
                     count,
                     request.PageNumber,
                     request.PageSize);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new PagedResponse<List<Category>>(
                    [],
                    0,
                    request.PageNumber,
                    request.PageSize);
            }


        }

        public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return category is null
                ? new Response<Category>(data: null, code: 404, message: "Not Found")
                : new Response<Category>(data: category, message: "Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Response<Category>(data: null, code: 500, message: "Not Found");
            }

        }

        public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);


            if (category == null)
                return new Response<Category>(data: null, code: 404, message: $"Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);

            try
            {
                await context.SaveChangesAsync();

                return new Response<Category>(data: category, message: "Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(data: null, code: 500, message: $"Não foi possível criar a categoria {category.ToString}");

            }
        }
    }
}