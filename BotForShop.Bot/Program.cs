﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using BotForShop.BLL;

namespace BotForShop.Bot
{
    public class Program
    {
        static string token = Environment.GetEnvironmentVariable("Gram");
        //public static UserService UserService { get; set; }
        static void Main(string[] args)
        {
            ITelegramBotClient bot = new TelegramBotClient(token);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.WriteLine("Ok");

            Console.ReadLine();

        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Your name is {message.Chat.FirstName}");
                }
                else if (message.Text != null)
                {
                    //var user = new UserInputModel()
                    //{
                    //    Name = message.Text
                    //};

                    //Console.WriteLine(user.Name);

                    var userService = new UserService();

                    //userService.AddUser(user);

                    var users = userService.GetAllUsers();


                    string mess = "";

                    foreach (var item in users)
                    {
                        mess += $"{item.Name} \n";
                    }
                    await botClient.SendTextMessageAsync(message.Chat, mess);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"you entered - {message.Text}");
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
