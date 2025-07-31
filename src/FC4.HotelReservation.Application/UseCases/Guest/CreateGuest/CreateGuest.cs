using FC4.HotelReservation.Application.Common;
using FC4.HotelReservation.Domain.Repositories;

namespace FC4.HotelReservation.Application.UseCases.Guest.CreateGuest;

public class CreateGuest(IGuestRepository guestRepository,
    IUnitOfWork unitOfWork) : ICreateGuest
{
    public async Task<CreateGuestOutput> Handle(CreateGuestInput request,
        CancellationToken cancellationToken)
    {
        var guest = request.ToGuest();
        await guestRepository.CreateGuestAsync(guest, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateGuestOutput(guest.Id);
    }
}