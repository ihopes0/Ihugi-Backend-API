using Ihugi.Common.ErrorWork;

namespace Ihugi.Domain.Errors;

// TODO: XML docs
public static class DomainErrors
{
    public static class User
    {
        public static readonly Error NotFound = new Error(
            "User.NotFound",
            "Пользователя с таким ID не существует");

        public static readonly Error NoContent = new Error(
            "User.NoContent",
            "Пользователя с таким ID не существует");

        public static readonly Error EmailAlreadyInUse = new Error(
            "User.EmailAlreadyInUse",
            "Пользователь с таким email уже существует.");
    }

    public static class Chat
    {
        public static readonly Error NotFound = new Error(
            "Chat.NotFound",
            "Чата с таким ID не существует");
    }
}