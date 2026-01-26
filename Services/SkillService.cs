using System;
using Mentorship.Api.Dtos.Skills;
using Mentorship.Api.Entities;
using Mentorship.Api.Repository;

namespace Mentorship.Api.Services;

public class SkillService (SkillRepository _repo)
{
    private readonly SkillRepository repo = _repo;

    public async Task<List<Skill>> GetAllSkills()
    {
        return await repo.GetAll();
    }
    public async Task<Skill?> DeleteSkill(int id)
    {

        return await repo.DeleteSkill(id);
    }
    public async Task<Skill> CreateSkill (CreateSkillDto skilldto)
    {
        return await repo.CreateSkill(skilldto);
    }
}
