using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Common.DataTransferObjects;

/// <summary>
/// DTO for editing skill
/// </summary>
public class SkillEditDto {
    public string Name { get; set; }
    [Range(1,10)]
    public byte Level { get; set; }
}