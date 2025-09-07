using MediatR;
using ToDoAPI.Dal.UnitOfWork;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTaskPercent
{
    public class UpdateTaskPercentHandler : IRequestHandler<UpdateTaskPercentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskPercentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateTaskPercentCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);

            if (task == null)
                return false;

            task.PercentageComplete = request.PercentageComplete;

            if (request.PercentageComplete == 100) // if task is done in 100%, mark it as completed
                task.IsCompleted = true;
            else if (request.PercentageComplete < 100) // and if is less than 100%, mark it as not completed
                task.IsCompleted = false;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }

}
