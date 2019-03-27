namespace Domain.PurchaseOrderLine
{
    using System;
    using Time;

    public class PurchaseOrderLine
    {
        readonly int id;
        readonly DateTime expectedDeliveryDate;
        readonly int expectedQuantity;

        bool isCancelled;

        public PurchaseOrderLine(
            int id,
            DateTime expectedDeliveryDate,
            int expectedQuantity)
        {
            this.id = id;
            this.expectedDeliveryDate = expectedDeliveryDate;
            this.expectedQuantity = expectedQuantity;
        }

        public int ExpectedQuantity => this.expectedQuantity;

        public void Cancel()
        {
            isCancelled = true;
        }

        public ReviewOutcome Review()
        {
            if(isCancelled)
            {
                return ReviewOutcome.None;
            }

            if(Clock.Instance.Now + TimeSpan.FromDays(60) > expectedDeliveryDate)
            {
                return new CancellationSuggested();
            }
            
            return ReviewOutcome.None;
        }
    }

    public class ReviewOutcome
    {
        public static readonly ReviewOutcome None = new ReviewOutcome();

        protected ReviewOutcome()
        {
        }
    }

    public class CancellationSuggested : ReviewOutcome
    {
    }
}
