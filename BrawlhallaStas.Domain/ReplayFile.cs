﻿using BrawlhallaStat.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace BrawlhallaStat.Domain;

public class ReplayFile : IHaveId<string>
{
    [Key]
    public string Id { get; set; } = null!;

    public string AuthorId { get; set; } = null!;
    public User Author { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public byte[] FileData { get; set; } = null!;
}