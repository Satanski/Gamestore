using FluentValidation;
using Gamestore.DAL.Entities;

namespace Gamestore.BLL.Helpers;

internal static class ValidationHelpers
{
    internal static void CyclicReferenceHelper(List<Category> genres, List<Category> forbiddenList, Guid parentId)
    {
        var childGenres = genres.Where(x => x.ParentCategoryId == parentId);
        if (childGenres.Any())
        {
            forbiddenList.AddRange(childGenres);

            foreach (var genre in childGenres)
            {
                CyclicReferenceHelper(genres, forbiddenList, genre.Id);
            }
        }
    }
}
