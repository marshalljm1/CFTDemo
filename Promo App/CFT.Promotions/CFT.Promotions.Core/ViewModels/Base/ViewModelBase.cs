﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CFT.Promotions.Core.Interfaces;

namespace CFT.Promotions.Core.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly INavigationService Navigation;

        protected ViewModelBase(ICommonServices commonServices)
        {
            Navigation = commonServices.Navigation;
        }

        private bool _isBusy;
        protected bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        protected string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public virtual Task InitializeAsync(object navData)
        {
            return null;
        }
    }
}
