using AnketPortali01.Dtos;
using AnketPortali01.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnketPortali01.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public GroupController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<GroupDto> GetList()
        {
            var Groups = _context.Groups.ToList();
            var GroupDtos = _mapper.Map<List<GroupDto>>(Groups);
            return GroupDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public GroupDto Get(int id)
        {
            var Group = _context.Groups.Where(s => s.Id == id).SingleOrDefault();
            var GroupDto = _mapper.Map<GroupDto>(Group);
            return GroupDto;
        }
        [HttpGet]
        [Route("{id}/Products")]
        public List<MessagingDto> GetProducts(int id)
        {
            var products = _context.Messagings.Where(s => s.GroupId == id).ToList();
            var productDtos = _mapper.Map<List<MessagingDto>>(products);
            return productDtos;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ResultDto Post(GroupDto dto)
        {
            if (_context.Groups.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Kategori Adı Kayıtlıdır!";
                return result;
            }
            var Group = _mapper.Map<Group>(dto);
            Group.Updated = DateTime.Now;
            Group.Created = DateTime.Now;
            _context.Groups.Add(Group);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Eklendi";
            return result;
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResultDto Put(GroupDto dto)
        {
            var Group = _context.Groups.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (Group == null)
            {
                result.Status = false;
                result.Message = "Kategori Bulunamadı!";
                return result;
            }
            Group.Name = (string)dto.Name;
            Group.IsActive = dto.IsActive;
            Group.Updated = DateTime.Now;

            _context.Groups.Update(Group);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public ResultDto Delete(int id)
        {
            var Group = _context.Groups.Where(s => s.Id == id).SingleOrDefault();
            if (Group == null)
            {
                result.Status = false;
                result.Message = "Kategori Bulunamadı!";
                return result;
            }
            _context.Groups.Remove(Group);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kategori Silindi";
            return result;
        }
    }
}
