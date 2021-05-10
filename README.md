# SharpBlog

一个使用 csharp 开发而成的博客网站，支持跨平台部署，界面美观，适合喜欢 csharp、markdown 的技术朋友。

Site: [https://sharpblog.cn](https://sharpblog.cn)

Document: [SharpBlog-Docs](https://www.yuque.com/gotoreinject/sharpblog/dcylg9)

# 架构

.NET 5/Blazor/Abp。

- **sharpblog-api**: api service, default listening in 44380, provide api for web and admin.
- **sharpblog-web**: web service.
- **sharpblog-admin**: backend service.

# 特点

- 跨平台部署，支持 docker
- 良好的 API
- .NET 5 支持
- SEO 优化
- 开源，代码结构清晰
- 多主题支持
- MarkDown 支持
- API、Admin、Web 服务分布式部署

# 环境依赖

- .NET 5 [dev: sdk, publish: runtime]
- mongo [store data]
- redis [cache data]
- linux/windows/mac

# 本地运行

提前准备好 mongo/redis, 手动在 appsettings.yml 中修改成自己的地址.

**1. 启动 api 组件：**
```bash
cd /source/to/SharpBlogX/src/SharpBlogX.Api
dotnet run
```
访问 `http://localhost:44380` 查看接口列表。

**2. 启动 web 组件**
```bash
cd /source/to/SharpBlogX/src/SharpBlogX.Web
dotnet run
```
访问 `http://localhost:44381` 查看博客页面。

**3. 启动 admin 组件**
```bash
cd /source/to/SharpBlogX/src/SharpBlogX.Admin
dotnet run
```
访问 `http://localhost:44382` 查看博客页面。


# 部署方式

推荐使用 docker 进行部署，参考文档：[sharpblog-docs](https://www.yuque.com/gotoreinject/sharpblog/dcylg9)。

# 功能截图

待补充。

# 致谢

待补充。
