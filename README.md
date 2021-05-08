# SharpBlog

一个使用 csharp 开发而成的博客网站，支持跨平台部署，界面美观，适合喜欢 csharp、markdown 的技术朋友。

![web_home](https://cdn.jsdelivr.net/gh/cnsimo/pic_bed@master/web_home.png)

# 架构

前端代码直接引用自一个 hugo 主题：[leaveit](https://themes.gohugo.io/leaveit/)

后端采用 .NET 5/Blazor/Abp。

# 部署

两种方式：直接部署、Docker 部署，推荐后者。

## Docker 部署

1. Please use root priv for following.

```bash
# install docker
apt install docker.io
apt install docker-compose

# deploy redis, if you need to use cache function
docker pull redis
docker run -itd --name redis -p 127.0.0.1:6379:6379 redis --requirepass "123456"

# deploy mongo
mkdir mongo-auth && cd mongo-auth
cat << EOF > initdb.js
db.createUser(
    {
        user: "admin",
        pwd: "123456",
        roles:[
            {
                role: "readWrite",
                db:   "sharpblog"
            }
        ]
    }
);
EOF

cat <<EOF > docker-compose.yml
mongo:
  image: mongo:latest
  container_name: mongo
  environment:
      MONGO_INITDB_DATABASE: sharpblog
      MONGO_INITDB_ROOT_USERNAME: admin
      MONGO_INITDB_ROOT_PASSWORD: 123456
  volumes:
      - ./initdb.js:/docker-entrypoint-initdb.d/initdb.js:ro
  ports:
    - "9898:27017"
  command: mongod --auth
EOF
docker-compose up -d
```

2. 待补充

# 功能截图

待补充。

# 致谢

待补充。