using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ToDoServiceStackCore
{
    [DataContract]
    [Route("/users")]
    public class UsersRequest : IReturn<UsersResponse> { }

    [DataContract]
    public class UsersResponse : IHasResponseStatus
    {
        public UsersResponse()
        {
            this.ResponseStatus = new ResponseStatus();
            this.Users = new List<User>();
        }

        [DataMember]
        public List<User> Users { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    [Route("/user")]
    public class CreateUserRequest : IReturn<CreateUserResponse>
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }

    public class CreateUserResponse
    {
        public CreateUserResponse()
        {
            this.ResponseStatus = new ResponseStatus();
            this.Result = new User();
        }
        public User Result { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class CreateUser
    {

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        [AutoIncrement]
        public string Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
