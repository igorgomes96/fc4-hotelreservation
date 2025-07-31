using MediatR;

namespace FC4.HotelReservation.Application.UseCases.Guest.CreateGuest;

public interface ICreateGuest : IRequestHandler<CreateGuestInput, CreateGuestOutput>;