﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Model;
using System;
using System.Threading.Tasks;
using Politics.Dtos;
using Politics.Helpers;
using System.Linq;

namespace Politics.Data
{
  public class PoliticiansRepository : IPoliticiansRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public PoliticiansRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<PaginatedList<PoliticianOutDto>> GetAllPoliticians(int? pageNumber, int? pageSize, string? partyId, string? search)
    {
      var politicians = _context.Politicians.Include(politician => politician.Party);
      if (partyId is not null)
      {
        if (partyId != "null")
        {
          politicians = politicians.Where(politician => politician.PartyId == partyId).Include(politician => politician.Party);
        } else
        {
          politicians = politicians.Where(politician => politician.PartyId == null).Include(politician => politician.Party); ;
        }
      }
      if (search is not null)
      {
        var lowercaseSearch = search.ToLower();
        politicians = politicians.Where(politician => (
          (politician.FirstName.ToLower() + ' ' +politician.LastName.ToLower()).Contains(lowercaseSearch) ||
          politician.Description.ToLower().Contains(lowercaseSearch)
        )).Include(politician => politician.Party);
      }
      var paginatedPoliticians = await PaginatedList<Politician>.CreateAsync(politicians, pageNumber ?? 1, pageSize ?? 10);
      return _mapper.Map<PaginatedList<Politician>, PaginatedList<PoliticianOutDto>>(paginatedPoliticians);
    }

    public async Task<PoliticianOutDto> GetPoliticianById(string id)
    {
      var politician = await _context.Politicians.Include(politician => politician.Party).FirstOrDefaultAsync(p => p.PoliticianId == id);
      if (politician is null)
      {
        return null;
      }
      return _mapper.Map<Politician, PoliticianOutDto>(politician);
    }

    public async Task<PoliticianOutDto> AddPolitician(PoliticianDto politicianDto, string userId)
    {
      var politician = _mapper.Map<PoliticianDto, Politician>(politicianDto);
      politician.PoliticianId = Guid.NewGuid().ToString();
      politician.CreatedAt = DateTime.Now;
      politician.CreatedById = userId;
      if (politician.PartyId is not null)
      {
        var party = await _context.Parties.FindAsync(politician.PartyId);
        if (party is null)
        {
          return null;
        }
        politician.Party = party;
      }
      await _context.Politicians.AddAsync(politician);
      await _context.SaveChangesAsync();

      var createdPolitician = await _context.Politicians.FindAsync(politician.PoliticianId);
      if (createdPolitician is null)
      {
        return null;
      }
      return _mapper.Map<Politician, PoliticianOutDto>(createdPolitician);
    }

    public async Task<PoliticianOutDto> UpdatePolitician(string politicianId, UpdatePoliticianDto politicianDto, string userId)
    {
      var politician = await _context.Politicians.Include(politician => politician.Party).FirstOrDefaultAsync(politician => politician.PoliticianId == politicianId);
      politician.PartyId = politicianDto.PartyId;
      politician.Description = politicianDto.Description;
      politician.UpdatedById = userId;
      politician.UpdatedAt = DateTime.Now;
      await _context.SaveChangesAsync();
      return _mapper.Map<Politician, PoliticianOutDto>(politician);
    }

    public async Task<PoliticianOutDto> DeletePolitician(string id)
    {
      var politician = await _context.Politicians.FindAsync(id);
      if (politician is null)
      {
        return null;
      }
      _context.Politicians.Remove(politician);
      await _context.SaveChangesAsync();
      return _mapper.Map<Politician, PoliticianOutDto>(politician);
    }
  }
}
