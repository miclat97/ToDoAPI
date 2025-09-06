using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Bll.Features.Tasks.DTOs;
using ToDoAPI.Bll.Features.Tasks.Queries.GetAllTasks;
using ToDoAPI.Dal.UnitOfWork;

namespace ToDoAPI.Bll.Features.Tasks.Queries.GetIncomingTasks
{
    public class GetIncomingTasksQueryHandler : IRequestHandler<GetIncomingTasksQuery, IEnumerable<TaskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetIncomingTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetIncomingTasksQuery request, CancellationToken cancellationToken)
        {
            var incomingTasks = await _unitOfWork.Tasks.FindAsync(x => (x.ExpiryDate > DateTime.Now && x.ExpiryDate <= DateTime.Now.AddDays(7)));
            return _mapper.Map<IEnumerable<TaskDto>>(incomingTasks);
        }
    }
}
