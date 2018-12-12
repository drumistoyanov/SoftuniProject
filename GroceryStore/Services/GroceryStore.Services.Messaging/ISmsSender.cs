using System.Threading.Tasks;

namespace GroceryStore.Services.Messaging
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
