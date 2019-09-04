# TCPingInfoView

[![License](https://img.shields.io/github/license/HMBSbige/TCPingInfoView.svg?label=License)](https://github.com/HMBSbige/TCPingInfoView/blob/master/LICENSE)

[![Steam](https://img.shields.io/github/release/HMBSbige/TCPingInfoView.svg?label=Steam)](https://store.steampowered.com/app/828090)
[![Steam release date](https://img.shields.io/github/release-date/HMBSbige/TCPingInfoView.svg?label=Released)](https://store.steampowered.com/app/828090)

[![Latest](https://img.shields.io/github/release-pre/HMBSbige/TCPingInfoView.svg?label=Latest)](https://github.com/HMBSbige/TCPingInfoView/releases)
[![Latest release date](https://img.shields.io/github/release-date-pre/HMBSbige/TCPingInfoView.svg?label=Released)](https://github.com/HMBSbige/TCPingInfoView/releases)

![](https://github.com/HMBSbige/TCPingInfoView/workflows/.NET%20Build/badge.svg)

**建议运行环境**
* Windows 7 或以上
* [.NET Core 3.0 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0)

# [Steam 上购买](https://store.steampowered.com/app/828090)

## 这是做什么的
用来测试 TCP 连接延迟、监视你的网络服务是否正常的简单工具

~~而我是用来监视我的 [ASF](https://github.com/JustArchiNET/ArchiSteamFarm)、游戏服务器和 NAS 的~~

## 截图
![](pic/preview.png)

## TXT列表格式举例
```
# Google
[2607:f8b0:4007:801::2004]:443 IPv6 地址
172.217.14.68:80
www.google.com 谷歌，默认443端口

# Youtube
[2607:f8b0:4007:80e::200e] IPv6 地址
172.217.14.78 IPv4 地址
www.youtube.com:80 80端口 注释可包括空格
```

## 应该支持
* 高 DPI
* 兼容 Win7
* IPv6

## 待完成
* 超时提示