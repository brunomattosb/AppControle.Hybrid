using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppControle.Shared.Entities;
using System.Text.Json.Serialization;

namespace AppControle.Shared.DTO.EntitiesDTO;
public class CategoryDTO
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

}