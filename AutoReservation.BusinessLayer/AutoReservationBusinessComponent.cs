using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
	public class AutoReservationBusinessComponent
	{
		#region FindAll

		public List<Auto> FindAllAutos()
		{
			using (var context = new AutoReservationEntities())
			{
				return FindAll(context.Autos);
			}
		}

		public List<Kunde> FindAllKunden()
		{
			using (var context = new AutoReservationEntities())
			{
				return FindAll(context.Kunden);
			}
		}

		public List<Reservation> FindAllReservationen()
		{
			using (var context = new AutoReservationEntities())
			{
				// FIXME: fugly eager loading. Better way?
				return context.Reservationen.Include(r => r.Auto).Include(r => r.Kunde).ToList();
			}
		}

		public List<T> FindAll<T>(DbSet<T> entries) where T : class
		{
			return entries.ToList();
		}

		#endregion FindAll

		#region Find

		public Auto FindAuto(int id)
		{
			using (var context = new AutoReservationEntities())
			{
				return Find(context, context.Autos, id);
			}
		}

		public Kunde FindKunde(int id)
		{
			using (var context = new AutoReservationEntities())
			{
				return Find(context, context.Kunden, id);
			}
		}

		public Reservation FindReservation(int id)
		{
			using (var context = new AutoReservationEntities())
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
			using (var context = new AutoReservationEntities())
			{
				return Update(context, context.Autos, original, modified);
			}
		}

		public Kunde UpdateKunde(Kunde original, Kunde modified)
		{
			using (var context = new AutoReservationEntities())
			{
				return Update(context, context.Kunden, original, modified);
			}
		}

		public Reservation UpdateReservation(Reservation original, Reservation modified)
		{
			using (var context = new AutoReservationEntities())
			{
				return Update(context, context.Reservationen, original, modified);
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
			using (var context = new AutoReservationEntities())
			{
				return Insert(context, context.Autos, auto);
			}
		}

		public Kunde InsertKunde(Kunde kunde)
		{
			using (var context = new AutoReservationEntities())
			{
				return Insert(context, context.Kunden, kunde);
			}
		}

		public Reservation InsertReservation(Reservation reservation)
		{
			using (var context = new AutoReservationEntities())
			{
				// FIXME: UGLY: Prevent saving car/client again
				Auto auto = reservation.Auto;
				reservation.Auto = null;
				Kunde kunde = reservation.Kunde;
				reservation.Kunde = null;
				reservation = Insert(context, context.Reservationen, reservation);
				reservation.Auto = auto;
				reservation.Kunde = kunde;
				return reservation;
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

		public Auto DeleteAuto(Auto auto)
		{
			using (var context = new AutoReservationEntities())
			{
				List<Reservation> reservations = context.Reservationen.Where(reservation => reservation.Auto.Id == auto.Id).ToList();
				if (reservations.Count > 0)
				{
					throw new RelationExistsException(auto, new List<object>(reservations));
				}

				return Delete(context, context.Autos, auto);
			}
		}

		public Kunde DeleteKunde(Kunde kunde)
		{
			using (var context = new AutoReservationEntities())
			{
				List<Reservation> reservations =
					context.Reservationen.Where(reservation => reservation.Kunde.Id == kunde.Id).ToList();
				if (reservations.Count > 0)
				{
					throw new RelationExistsException(kunde, new List<object>(reservations));
				}

				return Delete(context, context.Kunden, kunde);
			}
		}

		public Reservation DeleteReservation(Reservation reservation)
		{
			using (var context = new AutoReservationEntities())
			{
				return Delete(context, context.Reservationen, reservation);
			}
		}

		private T Delete<T>(AutoReservationEntities context, DbSet<T> dbSet, T entry) where T : class
		{
			try
			{
				dbSet.Attach(entry);
				entry = dbSet.Remove(entry);
				context.SaveChanges();
				return entry;
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
			DbPropertyValues databaseValue = context.Entry(original).GetDatabaseValues();
			context.Entry(original).CurrentValues.SetValues(databaseValue);

			throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof (T).Name),
				original);
		}
	}
}