
namespace LibraryLite.UseCases.Interfaces
{
    /// <summary>
    /// Defines a handler for a request with a return value
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public interface IRequestHandler<in TRequest, out TResponse>
        where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest message);
    }
    /// <summary>
    /// Defines an handler without a return value
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    public interface IRequesthandler<in TRequest>
        where TRequest : IRequest
    {
        void Handle(TRequest message);
    }
}
