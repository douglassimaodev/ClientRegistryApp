using System.ComponentModel;

namespace ClientRegistryApp.Common.Validations.Validators
{
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }
        List<string> Errors { get; set; }
        bool Validate();
        bool IsValid { get; set; }
    }
}
