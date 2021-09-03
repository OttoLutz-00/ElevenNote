using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        // Create
        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Read
        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new CategoryListItem
                        {
                            CategoryId = e.CategoryId,
                            Name = e.Name
                        }
                        );
                return query.ToArray();
            }
        }
    }
}
