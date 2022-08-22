using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess.Data;
using Tangy_DataAccess;
using Tangy_Models;
using Tangy_Common;
using Microsoft.EntityFrameworkCore;

namespace Tangy_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(objDTO);
                _db.OrderHeader.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();

                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                _db.OrderDetail.AddRange(obj.OrderDetails);
                await _db.SaveChangesAsync();

                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDTO;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeader.FirstOrDefaultAsync(u => u.Id == id);
            if (objHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _db.OrderDetail.Where(u => u.OrderHeaderId == id);

                _db.OrderDetail.RemoveRange(objDetail);
                _db.OrderHeader.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDTO> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeader.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetail.Where(u => u.OrderHeaderId == id).ToList(),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {

            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeader;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetail;

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id).ToList(),
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);

        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _db.OrderHeader.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDTO();
            }
            if (data.Status == SD.Status_Pending)
            {
                data.Status = SD.Status_Confirmed;
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
        {
            if (objDTO != null)
            {
                var OrderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(objDTO);
                _db.OrderHeader.Update(OrderHeader);
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(OrderHeader);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeader.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (status == SD.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
