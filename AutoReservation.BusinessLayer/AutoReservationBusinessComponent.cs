using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        #region FindAll
        public List<Auto> FindAllAutos()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return FindAll<Auto>(context.Autos);
            }
        }
        public List<Kunde> FindAllKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return FindAll<Kunde>(context.Kunden);
            }
        }
        public List<Reservation> FindAllReservationen()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                // FIXME: fugly eager loading. Better way?
                return context.Reservationen.Include(r=>r.Auto).Include(r=>r.Kunde).ToList();
            }
        }

        public List<T> FindAll<T>(DbSet<T> entries) where T : class
        {
            return entries.ToList<T>();
        }
        #endregion FindAll

        #region Find
        public Auto FindAuto(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Find<Auto>(context, context.Autos, id);
            }
        }
        public Kunde FindKunde(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Find<Kunde>(context, context.Kunden, id);
            }
        }
        public Reservation FindReservation(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                // FIXME: fugly eager loading && fugly WHERE stuff. Better way?
                return context.Reservationen.Include(r => r.Auto)
                    .Include(r => r.Kunde).First(r => r.ReservationNr == id);
            }
        }

        private T Find<T>(AutoReservationEntities context, DbSet<T> dbSet, int id) where T : class
        {
            return dbSet.Find(id);
        }
        #endregion Find

        #region Update
        public Auto UpdateAuto(Auto original, Auto modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Update<Auto>(context, context.Autos, original, modified);
            }
        }
        public Kunde UpdateKunde(Kunde original, Kunde modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Update<Kunde>(context, context.Kunden, original, modified);
            }
        }
        public Reservation UpdateReservation(Reservation original, Reservation modified)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Update<Reservation>(context, context.Reservationen, original, modified);
            }
        }

        private T Update<T>(AutoReservationEntities context, DbSet<T> dbSet, T original, T modified) where T : class
        {
            try
            {
                dbSet.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                context.SaveChanges();
                return modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                HandleDbConcurrencyException(context, original);
            }
            // why ever that's needed.. as it never gets here
            return null;
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
        public Kunde InsertKunde(Kunde auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Insert<Kunde>(context, context.Kunden, auto);
            }
        }
        public Reservation InsertReservation(Reservation auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Insert<Reservation>(context, context.Reservationen, auto);
            }
        }

        private T Insert<T>(AutoReservationEntities context, DbSet<T> dbSet, T entry) where T : class
        {
            entry = dbSet.Add(entry);
            context.SaveChanges();
            return entry;
        }
        #endregion Insert

        #region Delete
        public Auto DeleteAuto(Auto entry)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Delete<Auto>(context, context.Autos, entry);
            }
        }
        public Kunde DeleteKunde(Kunde entry)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Delete<Kunde>(context, context.Kunden, entry);
            }
        }
        public Reservation DeleteReservation(Reservation entry)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return Delete<Reservation>(context, context.Reservationen, entry);
            }
        }

        private T Delete<T>(AutoReservationEntities context, DbSet<T> dbSet, T entry) where T : class
        {
            try
            {
                dbSet.Attach(entry);
                return dbSet.Remove(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                HandleDbConcurrencyException(context, entry);
            }
            // why ever that's needed.. as it never gets here
            return null;
        }
        #endregion Delete

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }
    }
}