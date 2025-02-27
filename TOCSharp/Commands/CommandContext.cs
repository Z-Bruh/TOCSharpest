using System.Threading.Tasks;

namespace TOCSharp.Commands
{
    public class CommandContext
    {
        public bool IsChat { get; set; }
        public bool IsWhisper { get; set; }
        public string Sender { get; set; }= null!;
        public string ChatRoomID { get; set; }= null!;
        public string Message { get; set; } = null!;
        public string Prefix { get; set; } = "";
        public CommandsSystem CommandsSystem { get; set; } = null!;

        internal CommandContext()
        {

        }

        public async Task ReplyAsync(string message, string? toWhisper = null)
        {
            if (this.IsChat)
            {
                var split = message.Split("\n");
                for (int i = 0; i < split.Length; i++)
                {
                    string? str = split[i];

                    await this.CommandsSystem.Client.SendChatMessageAsync(this.ChatRoomID, str, toWhisper);
                    if (i != split.Length - 1)
                    {
                        await Task.Delay(1000);
                    }
                }
            }
            else
            {
                await this.CommandsSystem.Client.SendIMAsync(message, this.Sender);
            }
        }
    }
}