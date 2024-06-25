using AnketPortali01.Dtos;
using AnketPortali01.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AnketPortali01.Controllers
{
    [Route("api/messagings")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public MessagingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<MessagingDto> GetList()
        {
            var Messagings = _context.Messagings.ToList();
            var productDtos = _mapper.Map<List<MessagingDto>>(Messagings);
            return productDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public MessagingDto Get(int id)
        {
            var product = _context.Messagings.Where(s => s.Id == id).SingleOrDefault();
            var productDto = _mapper.Map<MessagingDto>(product);
            return productDto;
        }

        [HttpPost]
        public ResultDto Post(MessagingDto dto)
        {
            if (_context.Messagings.Any(c => c.Name == dto.Name))
            {
                result.Status = false;
                result.Message = "Girilen   mesaj Adı Kayıtlıdır!";
                return result;
            }
            var product = _mapper.Map<messaging>(dto);
            product.Updated = DateTime.Now;
            product.Created = DateTime.Now;
            _context.Messagings.Add(product);
            _=_context.SaveChanges();
            result.Status = true;
            result.Message = "mesaj Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(MessagingDto dto)
        {
            var product = _context.Messagings.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (product == null)
            {
                result.Status = false;
                result.Message = "Anket Bulunamadı!";
                return result;
            }
            product.Name = dto.Name;
            product.IsActive = dto.IsActive;
            product.Price = dto.Price;
            product.Updated = DateTime.Now;
            product.CategoryId = dto.CategoryId;
            _context.Messagings.Update(product);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Anket Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var product = _context.Messagings.Where(s => s.Id == id).SingleOrDefault();
            if (product == null)
            {
                result.Status = false;
                result.Message = "Anket Bulunamadı!";
                return result;
            }
            _context.Messagings.Remove(product);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Anket Silindi";
            return result;
        }
    }
}
