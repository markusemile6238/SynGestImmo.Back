namespace Tools.Result
{
    public class Error
    {

        public string ErrorMessage { get;}

        private Error(string message)
        {
            ErrorMessage = message;
        }

        public static Error Create(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error(message);
        }

    }
}
