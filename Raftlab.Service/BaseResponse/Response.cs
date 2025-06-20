using FluentValidation.Results;
using Raftlab.Core.ProjectEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raftlab.Service.BaseResponse
{
    /// <summary>
    /// this is Base Response For All Queries and Commands
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> 
    {
        public string Status => HasError ? ResponseTypeEnum.Fail.ToString() : ResponseTypeEnum.Success.ToString();
        public List<MessageData> Errors { get; set; } = new List<MessageData>();
        public bool HasError => Errors.Any(p => p.Type == MessageTypeEnum.Error.ToString());
        public bool HasInfo => Errors.Any(p => p.Type == MessageTypeEnum.Info.ToString());
        public Response()
        {
            Errors = new List<MessageData>();
        }
        public Response(T Value)
        {
            this.Data = Value;
        }
        public Response(string error, string code, MessageTypeEnum messageType = MessageTypeEnum.Error)
        {
            this.Errors.Add(new MessageData()
            {
                Message = error,
                Title = code,
                Type = messageType.ToString()
            });
        }
        public Response(List<MessageData> messages)
        {
            this.Errors.AddRange(messages);
        }
        public Response(ValidationResult result) 
        {
            this.Errors.AddRange(result.Errors.Select(z => new MessageData()
            {
                Message = z.ErrorMessage,
                Title = "",
                Type = "Error"
            }));
        }
        public T Data { get; set; }
    }

    public class MessageData
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
