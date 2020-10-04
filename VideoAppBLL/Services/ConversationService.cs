using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;
using VideoAppBLL.Interfaces;
using VideoAppBLL.MapProfiles;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork unit;
        private readonly MapperConfiguration mapConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new BllMapProfile());
        });

        public ConversationService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task CreateConversation(ConversationDTO conversation)
        {
            var mapper = new Mapper(mapConfig);
            await unit.Repository<Conversation>().Create(mapper.Map<Conversation>(conversation));
            unit.Save();
        }

        public async Task DeleteConversation(int DialogId, UserDTO user)
        {
            var mapper = new Mapper(mapConfig);
            var conversation = await unit.Repository<Conversation>().Get(DialogId);
            if(conversation.Owner == mapper.Map<User>(user))
            {
                await unit.Repository<Conversation>().Delete(DialogId);
            }
            else
            {
                conversation.Users.Remove(mapper.Map<User>(user));
                unit.Repository<Conversation>().Update(conversation);
            }
            unit.Save();
        }

        public async Task<IEnumerable<ConversationDTO>> GetAllConversations(UserDTO user)
        {
            var mapper = new Mapper(mapConfig);
            var conversations = await unit.Repository<Conversation>().FindMany(c => c.Users.Contains(mapper.Map<User>(user)));
            return mapper.Map<List<ConversationDTO>>(conversations);
        }

        public async Task<ConversationDTO> GetConversation(int ConversationId)
        {
            var mapper = new Mapper(mapConfig);
            var conv = await unit.Repository<Conversation>().Get(ConversationId);
            return mapper.Map<ConversationDTO>(conv);
        }

        public async Task RemoveUser(UserDTO user,UserDTO owner,ConversationDTO conversation)
        {
            var mapper = new Mapper(mapConfig);
            var conv = await unit.Repository<Conversation>().Get(conversation.Id);
            if (conv.Users.Contains(mapper.Map<User>(user)) && conv.Owner == mapper.Map<User>(owner))
            {
                conv.Users.Remove(mapper.Map<User>(user));
                unit.Repository<Conversation>().Update(conv);
            }
            unit.Save();
        }
    }
}
