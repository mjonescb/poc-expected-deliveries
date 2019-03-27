namespace Domain.Ports
{
    public interface ISerializeToDocument<TDocument>
    {
        TDocument ToDocument();
        void Load(TDocument document);
    }
}
