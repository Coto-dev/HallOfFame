namespace HallOfFame.Common.DataTransferObjects;

/// <summary>
/// DTO for creating and editing person
/// </summary>
public class PersonCreateEditDto {
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillEditDto> Skills { get; set; } = [];
}