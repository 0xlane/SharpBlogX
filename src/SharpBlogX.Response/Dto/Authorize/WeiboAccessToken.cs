using Newtonsoft.Json;

namespace SharpBlogX.Dto.Authorize
{
    public class WeiboAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("uid")]
        public virtual string Uid { get; set; }
    }
}