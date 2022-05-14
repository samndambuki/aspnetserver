using System.ComponentModel.DataAnnotations;

public class Post{

 [Key]
 public int PostId { get; set; }

 [Required]
 [MaxLength(100)]
 public String Title { get; set; } = string.Empty;

 [Required]
 [MaxLength(100000)]
 public string Content { get; set; } = string.Empty;
 


}