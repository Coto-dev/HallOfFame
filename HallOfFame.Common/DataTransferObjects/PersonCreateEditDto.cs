namespace HallOfFame.Common.DataTransferObjects;

public class PersonCreateEditDto {
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillEditDto> Skills { get; set; } = [];
}