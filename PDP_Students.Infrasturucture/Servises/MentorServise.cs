using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;

namespace PDP_Students.Infrasturucture.Servises;

public class MentorServise : IMentorServise
{
    public Task<ResponseModel<bool>> DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<IEnumerable<Mentor>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<Mentor>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsValidRefreshToken(string refreshToken, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<Token>> LoginAsync(Credential credential)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<bool>> LogOutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<Token>> RefreshTokenAsync(Token token)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<GetRegisterModel>> RegisterAsync(Mentor entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveRefreshToken(string refreshToken, Mentor entity)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<bool>> UpdateAsync(Mentor entity)
    {
        throw new NotImplementedException();
    }
}
