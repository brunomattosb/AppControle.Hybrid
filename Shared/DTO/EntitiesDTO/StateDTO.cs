using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using System.Text.Json.Serialization;

namespace Shared.DTO.EntitiesDTO;
public class StateDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

}