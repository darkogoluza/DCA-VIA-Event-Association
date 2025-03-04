using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;

public class Invitation: AggregateRoot
{
   private StatusType _statusType;
   private Guest _invitee;

   private Invitation(StatusType statusType, Guest invitee)
   {
      _statusType = statusType;
      _invitee = invitee;
   }

   public static Result<Invitation> Create(StatusType statusType, Guest invitee)
   {
      throw new NotImplementedException();
   }
}
