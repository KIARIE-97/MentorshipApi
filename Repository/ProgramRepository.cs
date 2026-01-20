using System;
using Mentorship.Api.Data;
using Mentorship.Api.Dtos.Programs;
using Mentorship.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Api.Repository;

public class ProgramRepository
{
    private readonly AppDbContext _context;
    public ProgramRepository(AppDbContext context)
    {
        _context = context;
    }
    //get
    public async Task<List<MentorshipProgram>> GetAll()
    {
        return await _context.Programs.ToListAsync();
    }
    //create
    public async Task<MentorshipProgram> CreateProgram(CreateProgramDto programDto)
    {
        var program = new MentorshipProgram
        {
          Title = programDto.Title,
          Description = programDto.Description,
          Start = programDto.Start  
        };
        _context.Programs.Add(program);
        await _context.SaveChangesAsync();
        return program;
    }
    //getbyid
    public async Task<MentorshipProgram?> GetSingle(int id)
    {
        return await _context.Programs.FirstOrDefaultAsync(p => p.ProgramId == id);
    }
    //update
    public async Task<MentorshipProgram?> UpdateProgram (int id, UpdateProgramDto programDto)
    {
        var program = await _context.Programs.FindAsync(id);
        if(program == null)  return null;

        if (programDto.Title != null) program.Title = programDto.Title;
        if (programDto.Description != null) program.Description = programDto.Description;
        if (programDto.Start != null) program.Start = programDto.Start.Value;
        if (programDto.End != null) program.End = programDto.End.Value;

        await _context.SaveChangesAsync();
        return program;
    }
    //delete
    public async Task<MentorshipProgram?> DeleteProgram(int id)
    {
        var program = await _context.Programs.FindAsync(id);
        if (program == null) return null;
        _context.Programs.Remove(program);
        await _context.SaveChangesAsync();
        return program;
    }
}
