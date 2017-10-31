﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiceMonitor;

namespace ServiceMonitor.Controllers
{
    public class Home2Controller : ApiController
    {
        private SDA_EndProjEntitiesLocalHp db = new SDA_EndProjEntitiesLocalHp();

        // GET: api/Home2
        public IQueryable<Performer> GetPerformers()
        {
            return db.Performers;
        }

        // GET: api/Home2/5
        [ResponseType(typeof(Performer))]
        public IHttpActionResult GetPerformer(int id)
        {
            Performer performer = db.Performers.Find(id);
            if (performer == null)
            {
                return NotFound();
            }

            return Ok(performer);
        }

        // PUT: api/Home2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerformer(int id, Performer performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performer.Id)
            {
                return BadRequest();
            }

            db.Entry(performer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformerExists(id))
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

        // POST: api/Home2
        [ResponseType(typeof(Performer))]
        public IHttpActionResult PostPerformer(Performer performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Performers.Add(performer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = performer.Id }, performer);
        }

        // DELETE: api/Home2/5
        [ResponseType(typeof(Performer))]
        public IHttpActionResult DeletePerformer(int id)
        {
            Performer performer = db.Performers.Find(id);
            if (performer == null)
            {
                return NotFound();
            }

            db.Performers.Remove(performer);
            db.SaveChanges();

            return Ok(performer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerformerExists(int id)
        {
            return db.Performers.Count(e => e.Id == id) > 0;
        }
    }
}