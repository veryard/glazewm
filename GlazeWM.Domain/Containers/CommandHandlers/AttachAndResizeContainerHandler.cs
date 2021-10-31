﻿using System.Linq;
using GlazeWM.Domain.Containers.Commands;
using GlazeWM.Domain.Windows;
using GlazeWM.Infrastructure.Bussing;

namespace GlazeWM.Domain.Containers.CommandHandlers
{
  class AttachAndResizeContainerHandler : ICommandHandler<AttachAndResizeContainerCommand>
  {
    private Bus _bus;

    public AttachAndResizeContainerHandler(Bus bus)
    {
      _bus = bus;
    }

    public CommandResponse Handle(AttachAndResizeContainerCommand command)
    {
      var childToAdd = command.ChildToAdd;
      var targetParent = command.TargetParent;
      var targetIndex = command.TargetIndex;

      if (!(childToAdd is TilingWindow || childToAdd is SplitContainer))
        return CommandResponse.Ok;

      _bus.Invoke(new AttachContainerCommand(childToAdd, targetParent, targetIndex));

      var resizableSiblings = childToAdd.SelfAndSiblings.Where(container => container is IResizable);
      double defaultPercent = 1.0 / resizableSiblings.Count();

      // Adjust `SizePercentage` of the added container and its siblings.
      foreach (var sibling in resizableSiblings)
        (sibling as IResizable).SizePercentage = defaultPercent;

      return CommandResponse.Ok;
    }
  }
}