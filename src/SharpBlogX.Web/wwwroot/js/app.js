window.onload = function () {
    const api = "https://api.sharpblog.cn:43380";
    const cdn = "https://cdn.jsdelivr.net/gh/imuncle/live2d@master";

    const currentTheme = window.localStorage.getItem('theme');
    const isDark = currentTheme === 'dark';

    var loadStyle = function (name) {
        var link = document.createElement("link");
        link.rel = "stylesheet";
        link.href = `https://cdn.jsdelivr.net/npm/vditor@3.4.7/dist/js/highlight.js/styles/${name}.css`;
        document.querySelector("body").append(link);
    }

    if (isDark) {
        document.querySelector('body').classList.add('dark-theme');
        document.getElementById('switch_default').checked = true;
        document.getElementById('mobile-toggle-theme').innerText = ' · Dark';
    } else {
        document.querySelector('body').classList.remove('dark-theme');
        document.getElementById('switch_default').checked = false;
        document.getElementById('mobile-toggle-theme').innerText = ' · Light';
    }

    const pathname = location.pathname;
    if (pathname == "/") {

    } else if (pathname.includes('/post/')) {
        var name = isDark ? "solarized-dark256" : "github";
        loadStyle(name);
    }
    else {
        var paths = ['/posts', '/categories', '/tags', '/apps'];
        if (paths.includes(pathname)) {
            document.querySelector(`.menu .menu-item[href='${location.pathname}']`).classList.add('active');
        }
    }

    document.querySelector('.toggleBtn').addEventListener('click', () => {
        if (document.querySelector('body').classList.contains('dark-theme')) {
            document.querySelector('body').classList.remove('dark-theme');
        } else {
            document.querySelector('body').classList.add('dark-theme');
        }

        var theme = document.body.classList.contains('dark-theme') ? 'dark' : 'light';
        window.localStorage.setItem('theme', theme);

        if (pathname.includes('/post/')) {
            var name = theme === 'dark' ? "solarized-dark256" : "github";
            loadStyle(name);
        }
    });

    document.getElementById('mobile-toggle-theme').addEventListener('click', () => {
        if (document.querySelector('body').classList.contains('dark-theme')) {
            document.querySelector('body').classList.remove('dark-theme');
            document.getElementById('mobile-toggle-theme').innerText = ' · Light';
        } else {
            document.querySelector('body').classList.add('dark-theme');
            document.getElementById('mobile-toggle-theme').innerText = ' · Dark';
        }

        var theme = document.body.classList.contains('dark-theme') ? 'dark' : 'light';
        window.localStorage.setItem('theme', theme);

        if (pathname.includes('/post/')) {
            var name = theme === 'dark' ? "solarized-dark256" : "github";
            loadStyle(name);
        }
    });

    document.querySelector('.menu-toggle').addEventListener('click', () => {
        var toggleMenu = document.querySelector('.menu-toggle');
        var mobileMenu = document.getElementById('mobile-menu');
        if (toggleMenu.classList.contains('active')) {
            toggleMenu.classList.remove('active');
            mobileMenu.classList.remove('active');
        } else {
            toggleMenu.classList.add("active");
            mobileMenu.classList.add("active");
        }
    });

    const models = [
        `${cdn}/model/Epsilon2.1/Epsilon2.1.model.json`,
        `${cdn}/model/haru/haru_01.model.json`,
        `${cdn}/model/haru/haru_02.model.json`,
        `${cdn}model/haruto/haruto.model.json`,
        `${cdn}/model/hijiki/hijiki.model.json`,
        `${cdn}/model/tororo/tororo.model.json`,
        `${cdn}/model/izumi/izumi.model.json`,
        `${cdn}/model/miku/miku.model.json`,
        `${cdn}/model/shizuku/shizuku.model.json`,
        `${cdn}/model/wanko/wanko.model.json`,
        `${cdn}/model/z16/z16.model.json`,
        `${cdn}/model/penchan/penchan.model.json`
    ];
    L2Dwidget.init({
        "model": {
            jsonPath: models[parseInt(Math.random() * (models.length))]
        },
        "display": {
            "position": "right",
            "width": 150,
            "height": 210,
            "hOffset": 5,
            "vOffset": 5,
            "superSample": 1,
        },
        "mobile": {
            "scale": 1,
            "show": true,
            "motion": true,
        },
        "react": {
            "opacityDefault": .5,
            "opacityOnHover": .2
        }
    });
};