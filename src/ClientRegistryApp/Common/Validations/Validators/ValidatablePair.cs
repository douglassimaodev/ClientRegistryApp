using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientRegistryApp.Common.Validations.Validators
{
    public partial class ValidatablePair<T> : ObservableObject, IValidatable<ValidatablePair<T>>
    {
        public List<IValidationRule<ValidatablePair<T>>> Validations { get; } = new List<IValidationRule<ValidatablePair<T>>>();

        [ObservableProperty]
        bool isValid = true;

        [ObservableProperty]
        List<string> errors = new List<string>();

        [ObservableProperty]
        ValidatableObject<T> item1 = new ValidatableObject<T>();

        [ObservableProperty]
        ValidatableObject<T> item2 = new ValidatableObject<T>();

        public bool Validate()
        {
            var item1IsValid = Item1.Validate();
            var item2IsValid = Item2.Validate();
            if (item1IsValid && item2IsValid)
            {
                Errors.Clear();

                IEnumerable<string> errors = Validations.Where(v => !v.Check(this))
                    .Select(v => v.ValidationMessage);

                Errors = errors.ToList();
                Item2.Errors = Errors;
                Item2.IsValid = !Errors.Any();
            }

            IsValid = !Item1.Errors.Any() && !Item2.Errors.Any();

            return this.IsValid;
        }
    }
}