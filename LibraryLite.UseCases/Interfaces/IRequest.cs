
namespace LibraryLite.UseCases.Interfaces
{
    /// <summary>
    /// Marker interface to represent a request with a void response
    /// </summary>
    public interface IRequest { }
    /// <summary>
    /// Marker interface to represent an interface with a TResponse
    /// </summary>
    /// <typeparam name="TResponse">Response Type</typeparam>
    public interface IRequest<out TResponse> { }
}
