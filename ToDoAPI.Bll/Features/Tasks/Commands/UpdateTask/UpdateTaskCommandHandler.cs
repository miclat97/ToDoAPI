using AutoMapper;
using MediatR;
using ToDoAPI.Dal.UnitOfWork;

namespace ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);

            //auto mapping
            _mapper.Map(request, task);


            _unitOfWork.Tasks.Update(task!);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}