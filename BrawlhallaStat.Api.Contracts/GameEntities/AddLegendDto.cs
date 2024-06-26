﻿namespace BrawlhallaStat.Api.Contracts.GameEntities;

public class AddLegendDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int FirstWeaponId { get; set; }
    public int SecondWeaponId { get; set; }
}