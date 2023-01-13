using System.Net;

namespace Domain.Responses
{
    public interface IResponse
    {
    }

    public class OkResponse : IResponse
    {
    }

    public class EntityResponse<T> : OkResponse
    {
        public T Entity { get; set; }

        public EntityResponse(T entity)
        {
            Entity = entity;
        }
    }

    public class FailResponse : IResponse
    {
        public string Error { get; set; }

        public FailResponse(string error)
        {
            this.Error = error;
        }
    }
}