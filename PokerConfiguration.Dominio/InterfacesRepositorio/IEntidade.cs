namespace PokerConfiguration.Dominio.InterfacesRepositorio
{
    public interface IEntidade<T>
    {
        T Id { get; set; }
    }
}
