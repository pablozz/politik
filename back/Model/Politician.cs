﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Politics.Model
{
  public class Politician
  {
    [Required]
    public string PoliticianId { get; set; }
    public string PartyId { get; set; }
    public Party Party { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string CreatedById { get; set; }
    public string UpdatedById { get; set; }
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
  }
}
