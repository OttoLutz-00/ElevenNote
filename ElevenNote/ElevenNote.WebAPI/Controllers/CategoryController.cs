using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            // GetUserId will do the work for me and get the user's access token
            var userId = Guid.Parse(User.Identity.GetUserId());
            // the user will have access to the CRUD methods because the class containing them will have been passed in the userId
            var categoryService = new CategoryService(userId);
            // returns the NoteService object that let's the user access and change data
            return categoryService;
        }

        // GET
        public IHttpActionResult Get()
        {
            CategoryService categoryService = CreateCategoryService();
            var categories = categoryService.GetCategories();
            return Ok(categories);
        }

        // POST
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoryService service = CreateCategoryService();

            if (!service.CreateCategory(category))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // PUT change category name

        // PUT add Notes to a category

    }
}
