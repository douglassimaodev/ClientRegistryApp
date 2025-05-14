using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientRegistryApp.Common.Validations.Validators
{
    public partial class ValidatableObject<T> : ObservableObject, IValidatable<T>
    {
        [ObservableProperty]
        private List<IValidationRule<T>> validations;

        [ObservableProperty]
        private List<string> errors;

        [ObservableProperty]
        private bool cleanOnChange;

        [ObservableProperty]
        public T value;

        [ObservableProperty]
        private bool isValid;// { get; set; } = true;

        public ValidatableObject()
        {
            IsValid = true;
            Validations = new List<IValidationRule<T>>();
            Errors = new List<string>();
            cleanOnChange = true;
        }

        public virtual bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> pErrors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = pErrors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        partial void OnValueChanged(T value)
        {
            Validate();
        }
    }
}