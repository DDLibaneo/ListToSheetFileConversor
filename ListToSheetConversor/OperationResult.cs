namespace ListToSheetConversor
{
    public class OperationResult
    {
        public OperationResult(bool wasSuccessful)
        {
            _wasSuccessful = wasSuccessful;
        }

        private bool _wasSuccessful;

        public bool WasSuccessful => _wasSuccessful;
    }
}