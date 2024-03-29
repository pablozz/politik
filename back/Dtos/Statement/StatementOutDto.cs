﻿using System.Collections.Generic;

namespace Politics.Dtos
{
  public class StatementOutDto
  {
    public string StatementId { get; set; }
    public string Politician { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedAt { get; set; }
  }
}
