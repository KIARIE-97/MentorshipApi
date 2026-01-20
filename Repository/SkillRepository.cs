using System;
using Mentorship.Api.Data;
using Mentorship.Api.Dtos.Skills;
using Mentorship.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Api.Repository;
//handle enums 
public class SkillRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Skill>> GetAll()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<Skill> CreateSkill(CreateSkillDto skillDto)
    {
        var skill = new Skill
        {
            SkillName = skillDto.SkillName,
            Category = skillDto.Category
        };
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<Skill?> DeleteSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if(skill == null) return null;
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

}
