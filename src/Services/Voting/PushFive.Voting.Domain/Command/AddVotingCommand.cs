using FluentValidation;
using System;
using System.Collections.Generic;

namespace PushFive.Voting.Domain.Command
{
    public class AddVotingCommand : Core.Messages.Command
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<VotingItem> VotingItems { get; private set; }

        public AddVotingCommand(string name, string email, IEnumerable<VotingItem> votingItems)
        {
            Name = name;
            Email = email;
            VotingItems = votingItems;
        }

        public class VotingItem
        {
            public int Order { get; private set; }
            public Guid SongId { get; private set; }

            public VotingItem(int order, Guid songId)
            {
                Order = order;
                SongId = songId;
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVotingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddVotingCommandValidation : AbstractValidator<AddVotingCommand>
    {
        public AddVotingCommandValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Voter name can not be empty")
                .MaximumLength(64);

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Voter email is required")
                .EmailAddress()
                .MaximumLength(128);

            RuleForEach(c => c.VotingItems).SetValidator(new VotingItemValidation());
        }

        public class VotingItemValidation : AbstractValidator<AddVotingCommand.VotingItem>
        {
            public VotingItemValidation()
            {
                RuleFor(c => c.Order)
                    .InclusiveBetween(1, 5)
                    .WithMessage("Order should be between 1 and 5");

                RuleFor(c => c.SongId)
                    .NotEmpty()
                    .WithMessage("Invalid song");
            }
        }
    }
}
