using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace runner
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        //private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> EventArgCache = new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        #endregion

        #region Methods

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void NotifyPropertyChanged(Expression<Func<object>>  property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (PropertyChanged == null)
            {
                return;
            }

            if (!(property.Body is UnaryExpression))
            {
                return;
            }

            var unaryExpression = property.Body as UnaryExpression;

            if (unaryExpression == null)
            {
                return;
            }

            NotifyPropertyChanged(((MemberExpression)unaryExpression.Operand).Member.Name);
        }


        #endregion
    }
}
