namespace Domain.PurchaseOrderLine
{
    using System;
    using Ports;
    using Time;

    public class PurchaseOrderLine : ISerializeToDocument<Document>
    {
        Document document;

        public PurchaseOrderLine(Document document)
        {
            this.document = document;
        }

        public PurchaseOrderLine(
            int id,
            DateTime expectedDeliveryDate,
            int expectedQuantity)
        {
            document = new Document
            {
                State = State.Submitted,
                Id = id,
                ExpectedQuantity = expectedQuantity,
                ExpectedDeliveryDate = expectedDeliveryDate
            };
        }

        public void Cancel()
        {
            document.State = State.Cancelled;
        }

        public ReviewOutcome Review()
        {
            if(document.State == State.Cancelled)
            {
                return ReviewOutcome.None;
            }

            if(Clock.Instance.Now + TimeSpan.FromDays(60) > document.ExpectedDeliveryDate)
            {
                return new CancellationSuggested();
            }
            
            return ReviewOutcome.None;
        }

        public Document ToDocument()
        {
            return document;
        }

        public void Load(Document document)
        {
            this.document = document;
        }
    }
}
