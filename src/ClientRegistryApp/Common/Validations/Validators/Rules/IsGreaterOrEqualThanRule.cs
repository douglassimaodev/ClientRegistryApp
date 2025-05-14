namespace ClientRegistryApp.Common.Validations.Validators.Rules
{
    public class IsGreaterOrEqualThanRule : IValidationRule<int>
    {
        public string ValidationMessage { get; set; }
        public int GreaterThan { get; set; }

        public bool Check(int value)
        {
            if (value >= GreaterThan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class NullableIsGreaterOrEqualThanRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        public int GreaterThan { get; set; }

        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            if (!int.TryParse(value, out int intValue)) return false;

            return intValue >= GreaterThan;
        }
    }
}
