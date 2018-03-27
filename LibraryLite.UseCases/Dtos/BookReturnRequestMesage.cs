using LibraryLite.UseCases.Interfaces;

namespace LibraryLite.UseCases.Dtos
{
    public class BookReturnRequestMesage : IRequest
    {
        public int Id { get; private set; }

        public BookReturnRequestMesage(int id)
        {
            Id = id;
        }
    }
}
