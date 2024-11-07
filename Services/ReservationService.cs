using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services.Adstraction;

namespace BadmintonBlast.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // Lấy danh sách tất cả các Reservation
        public async Task<IEnumerable<ReservationDTO>> GetAsync()
        {
            return await unitOfWork
                .ReservationRepository
                .GetAsync<ReservationDTO>();
        }



        // Lấy thông tin chi tiết của Reservation theo ID
        public async Task<IEnumerable<ReservationDTO>> GetByInvoiceIdAsync(int invoiceId)
        {
            // Lấy danh sách các thực thể Reservation từ kho dữ liệu với cùng InvoiceId
            var reservations = await unitOfWork.ReservationRepository.GetAsync<ReservationDTO>(r => r.Idinvoice == invoiceId);

            // Kiểm tra nếu không có thực thể nào
            if (reservations == null || !reservations.Any())
            {
                return Enumerable.Empty<ReservationDTO>(); // Trả về danh sách rỗng nếu không có dữ liệu
            }

            // Ánh xạ danh sách thực thể Reservation sang danh sách DTO
            var reservationDtos = mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            return reservationDtos;
        }


        // Thêm mới một Reservation
        public async Task InsertAsync(ReservationDTO reservation)
        {
            var entity = mapper.Map<ReservationDTO, Reservation>(reservation);
            await unitOfWork.ReservationRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        // Cập nhật thông tin Reservation theo ID
        public async Task UpdateAsync(int id, ReservationDTO reservationDTO)
        {
            if (reservationDTO == null)
            {
                throw new ArgumentNullException(nameof(reservationDTO), "Reservation data is required.");
            }

            var existingEntity = await unitOfWork.ReservationRepository.GetSingleAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            // Map giá trị cập nhật từ ReservationDTO sang thực thể hiện có
            mapper.Map(reservationDTO, existingEntity);

            // Cập nhật thực thể trong kho dữ liệu
            unitOfWork.ReservationRepository.Update(existingEntity);

            // Lưu các thay đổi
            await unitOfWork.SaveChangesAsync();
        }

        // Xóa một Reservation theo ID
        public async Task DeleteAsync(int id)
        {
            await unitOfWork.ReservationRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
