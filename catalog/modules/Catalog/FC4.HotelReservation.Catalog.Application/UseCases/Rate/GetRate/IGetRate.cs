using MediatR;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Rate.GetRate;

public interface IGetRate : IRequestHandler<GetRateInput, IEnumerable<GetRateOutput>>;