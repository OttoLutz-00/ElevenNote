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
    //this class contains our controllers, or endpoints.
    [Authorize]
    public class NoteController : ApiController
    {
        private NoteService CreateNoteService()
        {
            // GetUserId will do the work for me and get the user's access token
            var userId = Guid.Parse(User.Identity.GetUserId());
            // the user will have access to the CRUD methods because the class containing them will have been passed in the userId
            var noteService = new NoteService(userId);
            // returns the NoteService object that let's the user access and change data
            return noteService;
        }

        // GET
        public IHttpActionResult Get()
        {
            //authorizes the user and gives 'noteService' object access to the CRUD methods
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        // GET by Id
        public IHttpActionResult Get(int id)
        {
            NoteService noteService = CreateNoteService();
            var note = noteService.GetNoteById(id);
            return Ok(note);
        }

        // POST
        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NoteService service = CreateNoteService();

            if (!service.CreateNote(note))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // PUT
        public IHttpActionResult Put(NoteEdit note)
        {
            // makes sure NoteEdit is in correct state/ format before doing anyhting
            if (!ModelState.IsValid)
            {
                // incorrect state will return a bad request
                return BadRequest(ModelState);

            }

            // service that allows for Note CRUD methods
            var service = CreateNoteService();

            // if the notes update method did NOT complete for some reason:
            if (!service.UpdateNote(note))
            {
                return InternalServerError();
            }

            return Ok();


        }

        // DELETE
        public IHttpActionResult Delete(int id)
        {
            var service = CreateNoteService();

            if (!service.DeleteNote(id))
            {
                return InternalServerError();
            }

            return Ok();
        }


    }
}
