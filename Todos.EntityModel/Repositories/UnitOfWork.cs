using System;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.EntityModel.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        private ToDoListEntities context;
        /// <summary>
        /// 
        /// </summary>
        private GenericRepository<Todo> todoesRepository;
        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork()
        {
            this.context = new ToDoListEntities();
        }        
        /// <summary>
        /// 
        /// </summary>
        public GenericRepository<Todo> TodoesRepository
        {
            get 
            {
                if (this.todoesRepository == null)
                {
                    this.todoesRepository = new GenericRepository<Todo>(this.context);
                }

                return todoesRepository; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}