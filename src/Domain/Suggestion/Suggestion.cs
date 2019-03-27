namespace Domain.Suggestion
{
    using Ports;

    public class Suggestion : ISerializeToDocument<Document>
    {
        int purchaseOrderLineId;
        
        public Suggestion(int purchaseOrderLineId)
        {
            this.purchaseOrderLineId = purchaseOrderLineId;
        }

        public Document ToDocument()
        {
            return new Document { Id = purchaseOrderLineId };
        }

        public void Load(Document document)
        {
            purchaseOrderLineId = document.Id;
        }
    }
}

