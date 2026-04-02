using Ihugi.Common.ErrorWork;

namespace Ihugi.Domain.Errors;

// TODO: XML docs
public static class DomainErrors
{
    public static class User
    {
        public static Error NotFound(Guid id) => new(
            "User.NotFound",
            $"User with ID {id} not found");

        public static Error NoContent(Guid id) => new(
            "User.NoContent",
            $"User with ID {id} has already been deleted.");

        public static Error EmailAlreadyInUse(string email) => new(
            "User.EmailAlreadyInUse",
            $"Email {email} is already taken.");

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "Login or password are incorrect.");
    }

    public static class Chat
    {
        public static Error NotFound(Guid id) => new(
            "Chat.NotFound",
            $"Chat with ID {id} not found.");

        public static Error UserNotMember(Guid id, string chatName) => new(
            "Chat.UserNotMember",
            $"User with ID {id} is not a member of the chat {chatName}");
    }

    public static class Message
    {
        public static Error EmptyMessage => new(
            "Message.EmptyMessage",
            "Message cannot be empty or contain only of whitespaces.");

        public static Error NotFound(Guid id) => new(
            "Message.NotFound",
            "Message with ID {id} not found");

        public static readonly Error NotCreated = new(
            "Message.NotCreated",
            "Message creation failed.");
    }
}