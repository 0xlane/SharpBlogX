namespace SharpBlogX.Options
{
    public class HttpsOptions
    {
        /// <summary>
        /// https service address
        /// </summary>
        /// <value></value>
        public string ListenAddress { get; set; }

        /// <summary>
        /// https service port
        /// </summary>
        /// <value></value>
        public int ListenPort { get; set; }

        /// <summary>
        /// public certificate file path
        /// </summary>
        /// <value></value>
        public string PublicCertFile { get; set; }

        /// <summary>
        /// private certificate file path
        /// </summary>
        /// <value></value>
        public string PrivateCertFile { get; set; }
    }
}