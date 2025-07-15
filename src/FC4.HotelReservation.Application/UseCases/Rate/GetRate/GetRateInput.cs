using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Rate.GetRate;

public record GetRateInput(
    Guid HotelId,
    Guid RoomTypeId,
    DateTime StartDate,
    DateTime EndDate,
    int RoomQuantity
) : IRequest<IEnumerable<GetRateOutput>>;