using PsttTask.Domain.Data;
using PsttTask.Infrastucture;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace PsttTask.Infrastructure.Data
{
    public class PsttTaskUnitOfWork(PsttTaskContext context) : IPsttTaskUnitOfWork
    {
        private readonly PsttTaskContext context = context;
        private DbContextTransaction dbContextTransaction;


        //public TId Add<TEntity, TId>(TEntity entity)
        //{
        //    context.Add(entity);
        //}

        public void BeginTransaction()
        {
            dbContextTransaction = (DbContextTransaction)context.Database.BeginTransaction();
        }

        public void Commit()
        {
            context.SaveChanges();
            dbContextTransaction?.Commit();
        }

        public void RollBack()
        {
            dbContextTransaction?.Rollback();
        }

        public int Save()
        {
            var contextTransaction = context.Database.BeginTransaction();
            try
            {
                var result = context.SaveChanges();
                contextTransaction.Commit();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                contextTransaction.Rollback();
                var correlationId = Guid.NewGuid();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName, validationError.ErrorMessage);

                        //_logger.ErrorInDetail(validationError, correlationId,
                        //    $"{nameof(ELBaytUnitOfWork)}_{nameof(DbEntityValidationException)}", ex, 0,
                        //    _userIdentity.Name);
                    }
                }

                throw;
            }
            catch
            {
                contextTransaction.Rollback();
                //_logger.ErrorInDetail(nameof(Exception), Guid.NewGuid(),
                //    $"{nameof(ELBaytUnitOfWork)}_{nameof(Exception)}", ex, 0, _userIdentity.Name);
                throw;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            var contextTransaction = context.Database.BeginTransaction();
            try
            {
                var result = await context.SaveChangesAsync(cancellationToken);
                contextTransaction.Commit();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                contextTransaction.Rollback();
                var correlationId = Guid.NewGuid();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName, validationError.ErrorMessage);

                        //_logger.ErrorInDetail(validationError, correlationId,
                        //    $"{nameof(ELBaytUnitOfWork)}_{nameof(DbEntityValidationException)}", ex, 0,
                        //    _userIdentity.Name);
                    }
                }

                throw;
            }
            catch
            {
                //contextTransaction.Rollback();
                //_logger.ErrorInDetail(nameof(Exception), Guid.NewGuid(),
                //    $"{nameof(ELBaytUnitOfWork)}_{nameof(Exception)}", ex, 0, _userIdentity.Name);
                throw;
            }
        }

    }
}
