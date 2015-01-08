using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Todos.EntityModel;
using Todos.EntityModel.Repositories;

namespace Todos.Web.Controllers
{
    public class TodoesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private UnitOfWork unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        public TodoesController()
        {
            this.unitOfWork = new UnitOfWork();
        }
                
        // GET: Todoes
        public ActionResult Index()
        {
            var todoes = this.unitOfWork.TodoesRepository.Read();

            return View(todoes);
        }

        // GET: Todoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = await this.unitOfWork.TodoesRepository.ReadAsync(id);

            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // GET: Todoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TodoId,Title,Description,DueDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.TodoesRepository.Create(todo);
                await this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Todoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = await this.unitOfWork.TodoesRepository.ReadAsync(id);

            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TodoId,Title,Description,DueDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.TodoesRepository.Update(todo);
                await this.unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Todoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Todo todo = await this.unitOfWork.TodoesRepository.ReadAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {            
            this.unitOfWork.TodoesRepository.Delete(id);
            await this.unitOfWork.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
