using FC4.HotelReservation.Catalog.Domain.Repositories;
using FC4.HotelReservation.Shared.Application;
using FC4.HotelReservation.Shared.Application.Exceptions;

namespace FC4.HotelReservation.Catalog.Application.UseCases.Room.CreateRoom;

public class CreateRoom(
    IRoomRepository roomRepository,
    IRoomTypeRepository roomTypeRepository,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : ICreateRoom
{
    public async Task<CreateRoomOutput> Handle(CreateRoomInput request, CancellationToken cancellationToken)
    {
        _ = await roomTypeRepository.GetByIdAsync(request.RoomTypeId, cancellationToken)
            ?? throw new NotFoundException("Room type not found");
        
        _ = await hotelRepository.GetByIdAsync(request.HotelId, cancellationToken)
            ?? throw new NotFoundException("Hotel not found");
        
        var room = request.ToRoom();
        await roomRepository.CreateAsync(room, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateRoomOutput(room.Id);
    }
}