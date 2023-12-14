using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;
using PDP_Students.Domain.Models.StudentDTO;
using PDP_Students.Infrasturucture.DataAcces;
using PDP_Students.UI.CustomFilters;

namespace PDP_Students.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [CustomAutorizationFilter]
    [CustomRecourseFilter]
    [CustomResultFilter]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentServise _studentServise;
        private readonly PDP_StudentDbContext _db;

        public AccountController(IMapper mapper, IStudentServise studentServise, PDP_StudentDbContext db)
        {
            _mapper = mapper;
            _studentServise = studentServise;
            _db = db;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ResponseModel<GetRegisterModel>> Register(StudentCreateDTO studentCreate)
        {
            throw new Exception();
            Student mappedStudent = _mapper.Map<Student>(studentCreate);

            ResponseModel<GetRegisterModel> studentEntity = await _studentServise.RegisterAsync(mappedStudent);

            //StudentGetDTO studentDto = _mapper.Map<StudentGetDTO>(studentEntity);

            return studentEntity;
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<ResponseModel<Token>> Login(Credential credential)
        {
            var res = _studentServise.LoginAsync(credential);
            return res;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseModel<Token>> RefreshToken(Token token)
           => await _studentServise.RefreshTokenAsync(token);

        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseModel<bool>> LogOut()
        {
            // _studentServise.LogOutAsync();
            return new(true);
        }
            

        [HttpDelete]
        public Task<ResponseModel<bool>> Delete(int id)
            => _studentServise.DeleteAsync(id);

    }
}
