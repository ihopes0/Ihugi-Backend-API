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

        public static readonly Error UserNotMember = new Error(
            "Chat.UserNotMember",
            "Пользователь не является участником чата");
    }

    public static class Message
    {
        public static readonly Error EmptyMessage = new Error(
            "Message.EmptyMessage",
            "Сообщение не может быть пустым или содержать только пробелы");

        public static readonly Error NotFound = new Error(
            "Message.NotFound",
            "Сообщение с таким ID не найдено.");

        public static readonly Error NotCreated = new Error(
            "Message.NotCreated",
            "Сообщение не было создано.");
    }
}