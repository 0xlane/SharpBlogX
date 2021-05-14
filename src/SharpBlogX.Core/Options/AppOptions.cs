namespace SharpBlogX.Options
{
    public class AppOptions
    {
        /// <summary>
        /// Https
        /// </summary>
        /// <value></value>
        public HttpsOptions Https { get; set; }

        /// <summary>
        /// Blog
        /// </summary>
        /// <value></value>
        public BlogOptions Blog { get; set; }

        /// <summary>
        /// Waline
        /// </summary>
        /// <value></value>
        public WalineOptions Waline { get; set; }

        /// <summary>
        /// Notification
        /// </summary>
        /// <value></value>
        public NotificationOptions Notification { get; set; }

        /// <summary>
        /// Swagger
        /// </summary>
        public SwaggerOptions Swagger { get; set; }

        /// <summary>
        /// Storage
        /// </summary>
        public StorageOptions Storage { get; set; }

        /// <summary>
        /// Cors
        /// </summary>
        public CorsOptions Cors { get; set; }

        /// <summary>
        /// Jwt
        /// </summary>
        public JwtOptions Jwt { get; set; }

        /// <summary>
        /// Worker
        /// </summary>
        public WorkerOptions Worker { get; set; }

        /// <summary>
        /// TencentCloud
        /// </summary>
        public TencentCloudOptions TencentCloud { get; set; }

        /// <summary>
        /// Authorize
        /// </summary>
        public AuthorizeOptions Authorize { get; set; }
    }
}