namespace Superdigital.Backend.ContaCorrente.Domain.Specifications
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        string Mensagem { get; set; }

        bool IsSatisfiedBy(TEntity entity, double Valor);
    }
}
