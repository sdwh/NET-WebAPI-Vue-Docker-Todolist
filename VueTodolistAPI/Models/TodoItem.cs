using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TodoItem
{
    [Key]
    public int TodoItemID { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Title { get; set; }

    [MaxLength]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [ForeignKey("Category")]
    public int? CategoryID { get; set; }

    public Category? Category { get; set; }
}
