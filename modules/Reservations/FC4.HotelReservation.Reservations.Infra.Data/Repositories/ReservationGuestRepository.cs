using FC4.HotelReservation.Reservations.Domain.Entities;
using FC4.HotelReservation.Reservations.Domain.Repositories;
using FC4.HotelReservation.Reservations.Domain.ValueObjects;
using FC4.HotelReservation.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FC4.HotelReservation.Reservations.Infra.Data.Repositories;

public class ReservationGuestRepository(HotelDbContext context) : IReservationGuestRepository
{
    public async Task<GuestInfo?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken)
    {
        var guest = await context.Guests
            .SingleOrDefaultAsync(g => g.Id == hotelId, cancellationToken);
        return guest == null ? null : new GuestInfo(guest.Id, guest.FullName, new Email(guest.Email.Value));
    }
}