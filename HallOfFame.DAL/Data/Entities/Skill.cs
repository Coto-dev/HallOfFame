using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFame.DAL.Data.Entities;

public class Skill {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    [Range(1, 10)]
    public byte Level { get; set; }

    public Person Person { get; set; }
}