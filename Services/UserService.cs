using System;
using Mentorship.Api.Dtos.Users;
using Mentorship.Api.Entities;
using Mentorship.Api.Repository;

namespace Mentorship.Api.Services;

public class UserService  (UserRepository _repo)
{
    private readonly UserRepository repo = _repo;

  public async Task<List<User>> GetAllAsync()
    {
        return await repo.GetAll();
    }
  public async Task<User> CreateUser(CreateUserDto Userdto)
    {
        return await repo.CreateUser(Userdto);
    }
    public async Task<User?> GetSingleUser(int id)
    {
        return await repo.GetSingle(id);
    }
    public async Task<User?> UpdateUser(int id, UpdateUserDto Userdto)
    {
        return await repo.UpdateUser(id, Userdto);
    }
    public async Task<User?> DeleteUser(int id)
    {
        return await repo.DeleteUser(id);
    }
}
