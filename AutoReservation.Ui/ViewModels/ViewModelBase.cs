using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;

namespace AutoReservation.Ui.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
	{
		protected readonly IAutoReservationService Service;

		private readonly Dispatcher dispatcher;
		private string errorText;

		private PropertyChangedEventHandler propertyChangedEvent;
		private PropertyChangingEventHandler propertyChangingEvent;

		protected ViewModelBase()
		{
			dispatcher = Dispatcher.CurrentDispatcher;

			if (!IsInDesignTime)
			{
				Service = Creator.GetCreator().CreateInstance();
				Load();
			}
		}

		public Dispatcher Dispatcher
		{
			get { return dispatcher; }
		}

		public string ErrorText
		{
			get { return errorText; }
			set
			{
				if (errorText != value)
				{
					SendPropertyChanging(() => ErrorText);
					errorText = value;
					SendPropertyChanged(() => ErrorText);
				}
			}
		}

		#region Helper Methods

		private bool IsInDesignTime
		{
			get
			{
				DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
				return (bool) DependencyPropertyDescriptor.FromProperty(prop, typeof (FrameworkElement)).Metadata.DefaultValue;
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

			if (!property.DeclaringType.IsAssignableFrom(GetType()))
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

		#endregion

		#region Commands

		private AsyncRelayCommand deleteCommand;
		private RelayCommand loadCommand;
		private RelayCommand newCommand;
		private AsyncRelayCommand saveCommand;

		public ICommand LoadCommand
		{
			get
			{
				if (loadCommand == null)
				{
					loadCommand = new RelayCommand(
						param => Load(),
						param => CanLoad()
						);
				}
				return loadCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				if (saveCommand == null)
				{
					saveCommand = new AsyncRelayCommand(
						async param =>
						{
							await Task.Run(() => SaveData());
							Load();
						},
						param => CanSaveData()
						);
				}
				return saveCommand;
			}
		}

		public ICommand NewCommand
		{
			get
			{
				if (newCommand == null)
				{
					newCommand = new RelayCommand(
						param => New(),
						param => CanNew()
						);
				}
				return newCommand;
			}
		}

		public ICommand DeleteCommand
		{
			get
			{
				if (deleteCommand == null)
				{
					deleteCommand = new AsyncRelayCommand(
						async param =>
						{
							await Task.Run(() => Delete());
							Load();
						},
						param => CanDelete()
						);
				}
				return deleteCommand;
			}
		}

		protected abstract void Load();
		protected abstract bool CanLoad();

		protected abstract void SaveData();
		protected abstract bool CanSaveData();

		protected abstract void New();
		protected abstract bool CanNew();

		protected abstract bool CanDelete();
		protected abstract void Delete();

		#endregion

		public event PropertyChangedEventHandler PropertyChanged
		{
			add { propertyChangedEvent += value; }
			remove { propertyChangedEvent -= value; }
		}

		public event PropertyChangingEventHandler PropertyChanging
		{
			add { propertyChangingEvent += value; }
			remove { propertyChangingEvent -= value; }
		}

		protected void SendPropertyChanged<T>(Expression<Func<T>> expression)
		{
			string propertyName = ExtractPropertyName(expression);
			if (propertyChangedEvent != null)
			{
				propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void SendPropertyChanging<T>(Expression<Func<T>> expression)
		{
			string propertyName = ExtractPropertyName(expression);
			if (propertyChangingEvent != null)
			{
				propertyChangingEvent(this, new PropertyChangingEventArgs(propertyName));
			}
		}
	}
}