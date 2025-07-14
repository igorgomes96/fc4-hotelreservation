using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Rate.GetRate;

public interface IGetRate : IRequestHandler<GetRateInput, GetRateOutput>;