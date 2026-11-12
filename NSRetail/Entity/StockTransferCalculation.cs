using System;

namespace Entity
{
    public static class StockTransferCalculation
    {
        public static decimal Calculate(decimal sourceAmount, bool sourceIsOpen, bool destinationIsOpen,
            decimal sourceFactor, decimal destinationFactor, int sourceBaseUom, int destinationBaseUom)
        {
            if (sourceBaseUom <= 0 || sourceBaseUom != destinationBaseUom || sourceFactor <= 0 || destinationFactor <= 0)
                throw new ArgumentException("Configure compatible base UOMs and positive conversion factors for both SKUs.");
            if (sourceAmount <= 0) throw new ArgumentException("Enter a source amount greater than zero.");
            ValidateAmount(sourceAmount, sourceIsOpen);
            decimal destination = sourceAmount * sourceFactor / destinationFactor;
            ValidateAmount(destination, destinationIsOpen);
            return destination;
        }

        private static void ValidateAmount(decimal amount, bool isOpen)
        {
            if (isOpen && decimal.Round(amount, 2) != amount)
                throw new ArgumentException("Stock weights support two decimal places. Adjust the source amount.");
            if (!isOpen && (decimal.Truncate(amount) != amount || amount > int.MaxValue))
                throw new ArgumentException("The transfer must produce whole packets or units. Adjust the source amount.");
        }
    }
}
