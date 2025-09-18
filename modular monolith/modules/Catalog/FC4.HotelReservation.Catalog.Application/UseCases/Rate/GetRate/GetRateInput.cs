using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;

public record GetRateInput(
    Guid HotelId,
    Guid RoomTypeId,
    DateTime StartDate,
    DateTime EndDate,
    int RoomQuantity
) : IRequest<IEnumerable<GetRateOutput>>;