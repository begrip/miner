using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Helpers
{
    public class PropertyChangedHelper : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        public IEnumerable GetErrors(string propertyName)
        {
            _errors.TryGetValue(propertyName, out List<string> errorsForName);
            return errorsForName;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _HasChanges;
        protected bool HasChanges
        {
            get { return _HasChanges; }
            set { SetHasChangesProperty(ref _HasChanges, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        /// <summary>
        /// 
        /// </summary>
        private object _lock = new object();
        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        _errors.TryRemove(kv.Key, out List<string> outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        _errors.TryRemove(prop.Key, out List<string> outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propName"></param>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            Validate();
            HasChanges = _HasChanges || true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnPropertyHasChangesChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            Validate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="isModified"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected void SetProperty<T>(ref T storage, T value, ref bool isModified,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                isModified = false;
                HasChanges = _HasChanges || false;
            }

            storage = value;

            if (storage != null) { isModified = true; }

            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected void SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return;
            storage = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private void SetHasChangesProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return;
            storage = value;
            OnPropertyHasChangesChanged(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected void SetPropertyPersist<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            storage = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="isModified"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected void SetPropertyPersist<T>(ref T storage, T value, ref bool isModified,
        [CallerMemberName] string propertyName = null)
        {
            storage = value;
            if (storage != null) { isModified = true; }

            OnPropertyChanged(propertyName);
        }
    }
}
