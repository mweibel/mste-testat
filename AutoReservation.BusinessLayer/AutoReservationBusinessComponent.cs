using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        #region Find

        #endregion Find

        #region Update
        public Auto UpdateAuto(Auto original, Auto modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Update<Auto>(context, context.Autos, original, modified);
            }
        }

        private T Update<T>(AutoReservationEntities context, DbSet<T> dbSet, T original, T modified) where T : class
        {
            try
            {
                dbSet.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                HandleDbConcurrencyException(context, original);
            }
            return modified;
        }
        #endregion Update

        #region Insert
        public Auto InsertAuto(Auto auto) 
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Insert<Auto>(context, context.Autos, auto);
            }
        }

        private T Insert<T>(AutoReservationEntities context, DbSet<T> dbSet, T entry) where T : class
        {
            return dbSet.Add(entry);
        }
        #endregion Insert

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }
    }
}