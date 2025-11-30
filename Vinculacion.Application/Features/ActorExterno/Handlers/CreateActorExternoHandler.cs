using MediatR;
using Vinculacion.Application.Features.ActorExterno.Commands;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.Application.Features.ActorExterno.Handlers
{
    public class CreateActorExternoHandler: IRequestHandler<CreateActorExternoCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;
        public Task<int> Handle(CreateActorExternoCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
