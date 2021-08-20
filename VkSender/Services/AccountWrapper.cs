using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkSender.Services
{
    public interface IAccountWrapper
    {
        void PostMessage(string link, string message);
    }

    public class AccountWrapper : IAccountWrapper
    {
        private VkApi _vkApi;
        public AccountWrapper()
        {
            _vkApi = new VkApi();
            _vkApi.Authorize(new ApiAuthParams
            {
                AccessToken = "123"// Токен достается по jsonFile
            });
        }

        public void PostMessage(string link, string message)
        {
            try
            {

                var shortLink = (link.Replace("https://vk.com/", ""));
                if (shortLink.Any(x => !char.IsDigit(x)))
                {
                    SendMessage(shortLink, message);
                }
                SendMessage(long.Parse(shortLink), message);
            }
            catch (Exception e)
            {
                // Типо логи...
            }

        }

        private void SendMessage(long id, string message, ReadOnlyCollection<Photo> photos = null) //перегрузка по id https://vk.com/123123123
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = id,
                Message = message
            });
        }

        private void SendMessage(string domenName, string message, ReadOnlyCollection<Photo> photos = null) //  перегрузка по домену https://vk.com/pavelwhitetiger
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                Domain = domenName,
                Message = message
            });
        }
    }
}

