# ToDoAPI



Simple API ToDo list in .NET 8 with modular structure (3 projects + 1 test project): API itself (controllers, endpoints), Business logic and Data Access Layer.

Unit tests in xUnit





### Main design patterns:



* CQRS - queries and commands (changes in database) are separated in dedicated class/handlers, which are invoking by MediatR.
* AutoMapper - is mapping objects from database logic into business logic and vice versa.
* UnitOfWork with BaseRepository in database logic project will give possibility to easy add new features and repositories - all of them can use the same basic, repeatable tasks like FindAllAsync(lambda expression) instead of implementing logic for all those basic db tasks one by one for each repository.
* Unit and integration tests in xUnit - and because project is modular by design, it's much easier to test and mock everything.



### To run in docker container using docker-compose:

```
docker-compose up -d
```

Swagger will be at: http://localhost:8080/swagger/index.html



## API Endpoints:


> **GET** /api/Tasks - get all tasks



> **GET** /api/Tasks/{Id} - get task by Id



> **GET** /api/Tasks/GetIncomingTasks - get tasks with expiry date within next 7 days



> **POST** /api/Tasks - Create new task, json example:
> ```
> {
>   "title": "string",
>   "isCompleted": false,
>   "description": "string",
>   "percentageComplete": 0,
>   "createdAt": "2025-09-07T18:38:25.783Z",
>   "expiryDate": "2025-09-07T18:38:25.783Z"
> }
> ```



> **PUT** /api/Tasks/{Id} - Update task with Id
> 
> ```
> {
>   "id": 1,
>   "title": "string",
>   "description": "string",
>   "isCompleted": true,
>   "percentageComplete": 100,
>   "createdAt": "2025-09-07T18:39:49.194Z",
>   "expiryDate": "2025-09-07T18:39:49.194Z"
> }
> ```



> **PATCH** /api/Tasks/{id}/percent - Update task percent complete
> 
> When set to 100, task is also mark as done, if set to less than 100 - task is marked as undone
> ```
> {
>   "percentageComplete": 0
> }
> ```



> **PATCH** /api/Tasks/{id}/status - Mark task as done or not
> When isCompleted set to true, percentage is set to 100, when false - percentage is set to 0
> 
> ```
> {
>   "isCompleted": false
> }
> ```


### Database structure:


##### ToDoAPI.Dal.Entities.TaskEntity

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
