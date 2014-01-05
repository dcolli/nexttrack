using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using eftesting.Models;
using eftesting.DAL;

namespace eftesting.Controllers
{
    public class StreamController : ApiController
    {
        private NexttrackContext db = new NexttrackContext();

        // GET api/Stream
        public IQueryable<Stream> GetStreams()
        {
            return db.Streams;
        }

        // GET api/Stream/5
        [ResponseType(typeof(Stream))]
        public IHttpActionResult GetStream(int id)
        {
            Stream stream = db.Streams.Find(id);
            if (stream == null)
            {
                return NotFound();
            }

            return Ok(stream);
        }

        // PUT api/Stream/5
        public IHttpActionResult PutStream(int id, Stream stream)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stream.StreamId)
            {
                return BadRequest();
            }

            db.Entry(stream).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Stream
        [ResponseType(typeof(Stream))]
        public IHttpActionResult PostStream(Stream stream)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Streams.Add(stream);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stream.StreamId }, stream);
        }

        // DELETE api/Stream/5
        [ResponseType(typeof(Stream))]
        public IHttpActionResult DeleteStream(int id)
        {
            Stream stream = db.Streams.Find(id);
            if (stream == null)
            {
                return NotFound();
            }

            db.Streams.Remove(stream);
            db.SaveChanges();

            return Ok(stream);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StreamExists(int id)
        {
            return db.Streams.Count(e => e.StreamId == id) > 0;
        }
    }
}