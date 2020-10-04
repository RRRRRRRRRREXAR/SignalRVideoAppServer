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
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unit;
        private readonly MapperConfiguration mapConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new BllMapProfile());
        });
        public MessageService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task DeleteMessage(MessageDTO message)
        {
            await unit.Repository<Message>().Delete(message.Id);
            unit.Save();
        }

        public async Task EditMessage(MessageDTO message)
        {
            var mapper = new Mapper(mapConfig);
            await Task.Run(() =>
            {
                unit.Repository<Message>().Update(mapper.Map<Message>(message));
            });
            unit.Save();
        }

        public async Task SaveMessage(MessageDTO message)
        {
            var mapper = new Mapper(mapConfig);
            var dialog = unit.Repository<Conversation>().FindAsNoTracking(d => d == mapper.Map<Conversation>(message.DialogId));
            if (dialog.IsCompleted)
            {
                await unit.Repository<Message>().Create(mapper.Map<Message>(message));
            }
            else
            {
                await unit.Repository<Conversation>().Create(new Conversation 
                { 
                    Name = "",
                    Owner = mapper.Map<User>(message.Sender),
                    Users = new List<User> 
                    { 
                        mapper.Map<User>(message.Sender),
                        mapper.Map<User>(message.Recipient) 
                    }  
                });
            }
            unit.Save();
        }
    }
}
