using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
	[DataContract]
	public abstract class DtoBase : INotifyPropertyChanged, INotifyPropertyChanging, ICloneable
	{
		private PropertyChangedEventHandler _propertyChangedEvent;
		private PropertyChangingEventHandler _propertyChangingEvent;
		public abstract object Clone();

		public event PropertyChangedEventHandler PropertyChanged
		{
			add { _propertyChangedEvent += value; }
			remove { _propertyChangedEvent -= value; }
		}

		public event PropertyChangingEventHandler PropertyChanging
		{
			add { _propertyChangingEvent += value; }
			remove { _propertyChangingEvent -= value; }
		}

		public abstract string Validate();

		protected void SendPropertyChanged<T>(Expression<Func<T>> expression)
		{
			string propertyName = ExtractPropertyName(expression);
			if (_propertyChangedEvent != null)
			{
				_propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void SendPropertyChanging<T>(Expression<Func<T>> expression)
		{
			string propertyName = ExtractPropertyName(expression);
			if (_propertyChangingEvent != null)
			{
				_propertyChangingEvent(this, new PropertyChangingEventArgs(propertyName));
			}
		}

		private string ExtractPropertyName<T>(Expression<Func<T>> expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("Der Ausdruck ist kein Member-Lamda-Ausdruck (MemberExpression).", "expression");
			}

			var property = memberExpression.Member as PropertyInfo;
			if (property == null)
			{
				throw new ArgumentException("Der Member-Ausdruck greift nicht auf eine Eigenschaft zu.", "expression");
			}

			if (property.DeclaringType != null && !property.DeclaringType.IsAssignableFrom(GetType()))
			{
				throw new ArgumentException("Die referenzierte Eigenschaft gehört nicht zum gewünschten Typ.", "expression");
			}

			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Die referenzierte Eigenschaft hat keine 'get' - Methode.", "expression");
			}

			if (getMethod.IsStatic)
			{
				throw new ArgumentException("Die refrenzierte Eigenschaft ist statisch.", "expression");
			}

			return memberExpression.Member.Name;
		}


		protected abstract int GetIdForComparison();

		public override bool Equals(object obj)
		{
			bool equals = false;

			if (obj != null && obj is DtoBase)
			{
				var other = (DtoBase) obj;
				equals = other.GetIdForComparison().Equals(GetIdForComparison());
			}

			return equals;
		}

		public override int GetHashCode()
		{
			return GetIdForComparison().GetHashCode();
		}
	}
}