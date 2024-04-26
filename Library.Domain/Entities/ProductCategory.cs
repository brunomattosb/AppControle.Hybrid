﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Domain.Entities;
public class ProductCategory
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }

    public int ProductId { get; set; }
   
    public Category? Category { get; set; }

    [JsonIgnore]
    public int CategoryId { get; set; }
}