# ToDoAPI



Simple API ToDo list in .NET 8 with modular structure (3 projects + 1 test project): API itself (controllers, endpoints), Business logic and Data Access Layer.

Unit tests in xUnit





##### Main design patterns:



* CQRS - queries and commands (changes in database) are separated in dedicated class/handlers, which are invoking by MediatR.
* AutoMapper - is mapping objects from database logic into business logic and vice versa.
* UnitOfWork with BaseRepository in database logic project will give possibility to easy add new features and repositories - all of them can use the same basic, repeatable tasks like FindAllAsync(lambda expression) instead of implementing logic for all those basic db tasks one by one for each repository.
* Unit and integration tests in xUnit - and because project is modular by design, it's much easier to test and mock everything.





Database structure:



ToDoAPI.Dal.Entities.TaskEntity

```
public class TaskEntity
{
   [Key]
   public int Id { get; set; }
   [MaxLength(200)]
   [Required]
   public required string Title { get; set; }
   [Required]
   public bool IsCompleted { get; set; } = false;
   public string? Description { get; set; }
   [Range(0, 100)]
   public int PercentageComplete { get; set; } = 0;
   [Required]
   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   public DateTime? ExpiryDate { get; set; }
}
```

