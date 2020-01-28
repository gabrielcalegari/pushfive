using AutoMapper;
using PushFive.Voting.Domain.Command;
using PushFive.Voting.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PushFive.Voting.WebApi.AutoMapper
{
    public class DtoToCommandProfile : Profile
    {
        public DtoToCommandProfile()
        {
            CreateMap<VotingPost, AddVotingCommand>().ConvertUsing<VotingPostToAddVotingCommandConverter>();
        }
    }

    public class VotingPostToAddVotingCommandConverter : ITypeConverter<VotingPost, AddVotingCommand>
    {
        public AddVotingCommand Convert(VotingPost source, AddVotingCommand destination, ResolutionContext context)
        {
            var itemsCommand = GetVotingItems(source);
            var command = new AddVotingCommand(source.Name, source.Email, itemsCommand);
            return command;
        }

        private IEnumerable<AddVotingCommand.VotingItem> GetVotingItems(VotingPost source)
        {
            for (int i = 0; i < source.Items.Length; i++)
            {
                yield return new AddVotingCommand.VotingItem(i + 1, source.Items[i].SongId);
            }
        }
    }
}
