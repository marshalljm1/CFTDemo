using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using CFT.Promotions.Core.Interfaces;
using CFT.Promotions.Core.Models;
using CFT.Promotions.Core.Validation;
using CFT.Promotions.Core.Validation.Rules;
using Xamarin.Forms;

namespace CFT.Promotions.Core.ViewModels
{
    public class CreditCardViewModel : ViewModelBase
    {
        private const string API_LOGIN_ID = "482hb3GZLd87";
        private const string TRANSACTION_KEY = "9JS8xap24nK429e5";

        public ICommand ValidateCcNumberCommand => new Command(() => ValidateCcNumber());
        public ICommand ValidateExpiryCommand => new Command(() => ValidateExpiry());
        public ICommand ValidateCvvCommand => new Command(() => ValidateCvv());
        public ICommand PayCommand => _payCommand ?? (_payCommand = new Command(async () => await OnPayAsync()));
        private Command _payCommand;
        
        private bool _ccinfovalid;

        public bool CCInfoValid
        {
            get => _ccinfovalid;
            set => SetProperty(ref _ccinfovalid, value);
        }

        /** Validatable properties **/
        /********************************************/
        private ValidatableObject<string> _cardNumber;
        public ValidatableObject<string> CardNumber
        {
            get => _cardNumber;
            set => SetProperty(ref _cardNumber, value);
        }

        private ValidatableObject<string> _expDate;
        public ValidatableObject<string> ExpDate
        {
            get => _expDate;
            set => SetProperty(ref _expDate, value);
        }

        private ValidatableObject<string> _cvv;
        public ValidatableObject<string> CVV
        {
            get => _cvv;
            set => SetProperty(ref _cvv, value);
        }

        public CCTransData Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }
        /********************************************/

        private readonly IUnitOfWork _unit;
        private CCTransData _data;

        public CreditCardViewModel(ICommonServices commonServices, IUnitOfWork unit) : base(commonServices)
        {
            _unit = unit;
            BuildValidationObjects();
            _data = new CCTransData();
        }

        public override async Task InitializeAsync(object parameter)
        {
            //await new Task(() => { _data = parameter as CCTransData; });
            await Task.Run(() => { Data = parameter as CCTransData; });
        }

        private async Task SendPassengerManifest(CCTransData data, CctransInfo info)
        {
            var record = new TripManifests
            {
                EmailAddress = data.CustomerEmail,
                FirstName = data.CustomerFirstName,
                LastName = data.CustomerLastName,
                Paid = true,
                TripId = data.Trip.Id,
                DatePaid = DateTime.Now
            };

            //await _unit.Manifests.AddItemAsync(record);

            info.Manifest = record;
            info.ManifestId = record.Id;

            var result = await _unit.CCTransInfo.AddItemAsync(info);

            await Navigation.NavigateBack();
        }

        private void BuildValidationObjects()
        {
            _cardNumber = new ValidatableObject<string>();
            _expDate = new ValidatableObject<string>();
            _cvv = new ValidatableObject<string>();

            AddValidations();
        }

        private void AddValidations()
        {
            _cardNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Card number must not be blank"
            });
            _expDate.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Expiration date must not be blank"
            });
            _expDate.Validations.Add(new ValidExpiryDateRule<string>
            {
                ValidationMessage = "Expiration date is not valid"
            });
            _cvv.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "CVV must not be blank"
            });
        }

        private bool Validate()
        {
            var val1 = ValidateCcNumber();
            var val2 = ValidateExpiry();
            var val3 = ValidateCvv();

            _ccinfovalid = val1 && val2 && val3;
            return _ccinfovalid;
        }

        private bool ValidateCcNumber()
        {
            return _cardNumber.Validate();
        }

        private bool ValidateExpiry()
        {
            return _expDate.Validate();
        }

        private bool ValidateCvv()
        {
            return _cvv.Validate();
        }

        private async Task OnPayAsync()
        {
            if (!Validate()) return;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = API_LOGIN_ID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = TRANSACTION_KEY
            };
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            var creditCard = new creditCardType
            {
                cardNumber = _cardNumber.Value.Replace(" ", string.Empty),
                expirationDate = _expDate.Value,
                cardCode = _cvv.Value
            };

            var paymentType = new paymentType {Item = creditCard};

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = 100M,
                payment = paymentType
            };

            var request = new createTransactionRequest {transactionRequest = transactionRequest};
            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        var transData = new CctransInfo
                        {
                            AuthCode = response.transactionResponse.authCode,
                            Description = response.transactionResponse.messages[0].description,
                            MessageCode = response.transactionResponse.messages[0].code,
                            TransactionId = response.transactionResponse.transId,
                            ResponseCode = response.transactionResponse.responseCode
                        };

                        await SendPassengerManifest(_data, transData);
                    }
                    else
                    {
                        if (response.transactionResponse.errors != null)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error processing transaction", response.transactionResponse.errors[0].errorText, "OK");
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error processing transaction",
                        response.transactionResponse?.errors != null
                            ? response.transactionResponse.errors[0].errorText
                            : response.messages.message[0].text, "OK");
                }
            }
            else
            {
                Console.WriteLine("Null Response.");
            }
        }
    }
}
