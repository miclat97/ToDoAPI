using MediatR;
using ToDoAPI.Dal.UnitOfWork;

namespace ToDoAPI.Bll.Features.Tasks.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeTaskStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);

            if (task == null)
                return false;

            task.IsCompleted = request.IsCompleted;

            if (request.IsCompleted) // if task is set as completed, set it as 100% complete
                task.PercentageComplete = 100;
            else
                task.PercentageComplete = 0;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
