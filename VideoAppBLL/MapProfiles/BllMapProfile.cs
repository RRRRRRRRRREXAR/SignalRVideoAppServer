﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VideoAppBLL.DTO;
using VideoAppDAL.Entities;

namespace VideoAppBLL.MapProfiles
{
    class BllMapProfile:Profile
    {
        public BllMapProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(
                dest => dest.ProfileImage,
                opt => opt.MapFrom(src => src.ProfileImage));

            CreateMap<User, UserDTO>()
                .ForMember(
                dest => dest.ProfileImage,
                opt => opt.MapFrom(src => src.ProfileImage));

            CreateMap<ConversationDTO, Conversation>()
                .ForMember(
                dest => dest.Messages,
                opt => opt.MapFrom(src => src.Messages))
                .ForMember(
                dest => dest.Users,
                opt => opt.MapFrom(src => src.Users))
                .ForMember(
                dest => dest.Owner,
                opt => opt.MapFrom(src => src.Owner));

            CreateMap<Conversation, ConversationDTO>()
                .ForMember(
                dest => dest.Messages,
                opt => opt.MapFrom(src => src.Messages))
                .ForMember(
                dest => dest.Users,
                opt => opt.MapFrom(src => src.Users))
                .ForMember(
                dest => dest.Owner,
                opt => opt.MapFrom(src => src.Owner));

            CreateMap<ImageDTO, Image>()
                .ForMember(
                dest => dest.UserId,
                opt => opt.MapFrom(src => src.User));

            CreateMap<Image, ImageDTO>()
                    .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src.UserId));

            CreateMap<MessageDTO, Message>()
                .ForMember(
                dest => dest.DialogId,
                opt => opt.MapFrom(src => src.DialogId));
        }
    }
}
