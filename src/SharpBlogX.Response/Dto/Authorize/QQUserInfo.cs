using Newtonsoft.Json;

namespace SharpBlogX.Dto.Authorize
{
    public class QQUserInfo
    {
        public string Id { get; set; }

        [JsonProperty("nickname")]
        public string Name { get; set; }

        [JsonProperty("figureurl_qq")]
        public string Avatar { get; set; }

        public string Email { get; set; } = "";
    }

    public class QQOpenId
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
    }
}