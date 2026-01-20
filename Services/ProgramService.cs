using System;
using Mentorship.Api.Dtos.Programs;
using Mentorship.Api.Entities;
using Mentorship.Api.Repository;

namespace Mentorship.Api.Services;

public class ProgramService (ProgramRepository _repo)
{
    private readonly ProgramRepository repo = _repo;

  public async Task<List<MentorshipProgram>> GetAllAsync()
    {
        return await repo.GetAll();
    }
  public  async Task<MentorshipProgram?> GetOne(int id)
    {
      return await repo.GetSingle(id);   
    }
    public  async Task<MentorshipProgram?> UpdateMentorshipProgram(int id, UpdateProgramDto programDto)
    {
      return await repo.UpdateProgram(id, programDto);   
    }  
   
   public  async Task<MentorshipProgram?> DeleteProgram(int id)
    {
      return await repo.DeleteProgram(id);   
    } 
}
