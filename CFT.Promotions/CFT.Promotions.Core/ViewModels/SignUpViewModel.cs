using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using CFT.Promotions.Core.Validation;
using CFT.Promotions.Core.Validation.Rules;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Plugin.Toasts;
using Xamarin.Forms;

namespace CFT.Promotions.Core.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        public ICommand ValidateFirstNameCommand => new Command(() => ValidateFirstName());
        public ICommand ValidateLastNameCommand => new Command(() => ValidateLastName());
        public ICommand ValidateEmailCommand => new Command(() => ValidateEmail());

        private Command _payCommand;
        public ICommand PayCommand => _payCommand ?? (_payCommand = new Command( async (e) => await OnPayAsync(e)));
        
        private Command _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(async () => await OnRefreshAsync()));

        private string _promo;

        public string Promo
        {
            get => _promo;
            set => SetProperty(ref _promo, value);
        }

        private ObservableCollection<Trips> _trips;
        public ObservableCollection<Trips> Trips
        {
            get => _trips ?? (_trips = new ObservableCollection<Trips>());
            set => SetProperty(ref _trips, value);
        }

        private bool _refreshing;
        public bool IsRefreshing
        {
            get => _refreshing;
            set => SetProperty(ref _refreshing, value);
        }

        /** Validatable properties **/
        /********************************************/
        private ValidatableObject<string> _firstName;
        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private ValidatableObject<string> _lastName;
        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        /********************************************/

        private IEnumerable<Messages> _promoMessages;
        private Messages currentPromo;
        private readonly IToastNotificator _toaster;
        private readonly IUnitOfWork _unit;

        public SignUpViewModel(ICommonServices services, IUnitOfWork unit) : base(services)
        {
            _toaster = services.Notificator;
            _unit = unit;
            _trips = new ObservableCollection<Trips>();
            BuildValidationObjects();
            
        }

        private async Task OnRefreshAsync()
        {
            IsRefreshing = true;
            Trips = new ObservableCollection<Trips>(await _unit.Trips.GetItemsAsync(true));
            _promoMessages = await _unit.Messages.GetItemsAsync(true);
            currentPromo = _promoMessages.FirstOrDefault(x => x.StartDate <= DateTime.Today && DateTime.Today <= x.EndDate);
            Promo = currentPromo?.Message;
            IsRefreshing = false;
        }

        private async Task OnPayAsync(object parameter)
        {
            if (!Validate())
                return;

            if (!(parameter is Trips trip))
            {
                var opt = new NotificationOptions()
                {
                    Title = "No Trip Selected"
                };
                await _toaster.Notify(opt);
                return;
            }

            var type = await _unit.TripTypes.GetItemAsync(trip.TripType);

            var tripString = trip.DestinationCity 
                             + " " + type.Description 
                             + ": " + trip.DepartureDate .ToString("d")
                             + " - " + trip.ReturnDate.ToString("d");

            var result = await CrossPayPalManager.Current.Buy(
                new PayPalItem(tripString, GetFinalProductPrice(type), "USD"), 0);

            var options = new NotificationOptions()
            {
                Title = "Payment Successful"
            };

            switch (result.Status)
            {
                    

                case PayPalStatus.Successful:
                    CrossPayPalManager.Current.ClearUserData();
                    await SendPassengerManifest(trip);
                    ClearEntryFields();
                    await _toaster.Notify(options);
                    break;

                case PayPalStatus.Cancelled:
                    options.Title = "Payment Cancelled";
                    await _toaster.Notify(options);
                    break;

                case PayPalStatus.Error:
                    options.Title = "Payment Error";
                    await _toaster.Notify(options);
                    break;

                default:

                    throw new ArgumentOutOfRangeException();
            }
        }

        private decimal GetFinalProductPrice(TripTypes type)
        {
            if (currentPromo != null)
                return type.Price * ((decimal)1 - (decimal)currentPromo.DiscountPercentage / (decimal)100);
            else
                return type.Price;
        }

        private void ClearEntryFields()
        {
            FirstName.Value = string.Empty;
            LastName.Value = string.Empty;
            Email.Value = string.Empty;

            _firstName.IsValid = true;
            _lastName.IsValid = true;
            _email.IsValid = true;
        }

        private Task SendPassengerManifest(Trips trip)
        {
            var record = new TripManifests
            {
                EmailAddress = Email.Value,
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                Paid = true,
                TripId = trip.Id,
                DatePaid = DateTime.Now
            };
            return _unit.Manifests.AddItemAsync(record);
        }

        private void BuildValidationObjects()
        {
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();

            AddValidations();
        }

        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "First name must not be blank"
            });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Last name must not be blank"
            });
            _email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email address must not be blank"
            });
            _email.Validations.Add(new ValidEmailRule<string>
            {
                ValidationMessage = "Invalid email address"
            });
        }

        private bool Validate()
        {
            var val1 = ValidateFirstName();
            var val2 = ValidateLastName();
            var val3 = ValidateEmail();

            return val1 && val2 && val3;
        }

        private bool ValidateFirstName()
        {
            return _firstName.Validate();
        }

        private bool ValidateLastName()
        {
            return _lastName.Validate();
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

    }
}
