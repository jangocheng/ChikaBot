ChikaBot
========

一个开源的QQ/OICQ机器人后端

> 这个东西极大可能性跑不起来
> 如果需要有限范围内的帮助请在Issues区提问

## 功能列表
1. 签到
2. 状态
3. 抽卡模拟
4. 积分商店
5. 随机禁言
6. 忘了...

## 依赖的平台/框架
 + .Net Framework 4.6或者更高
 + 酷Q Pro 5.11.4 或者更高
 + Newbe.CQP.Framework 1.1或者更高
 + 剩下的在NuGet中引用了的程序包

## 小提示
```using System.Data.Sqlite;
using Newbe.CQP.Framework;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ValueTuple;
```
试试引用这几个命名空间（没有请添加NuGet包）

## 如何使用
1. 将项目克隆到本地，并使用Visual Studio 2017 Community或者更新的版本编译
2. 将 项目目录 / bin / Debug下的文件按照Newbe.CQP.Framework的说明复制到酷Q Pro目录
3. 将酷Q Pro切换到开发模式（详细步骤见酷Q 文档）
4. 启动酷Q

## 缺陷列表
1. 高并发下SQLite会卡
2. 对内存要求较高
3. 抽卡模拟功能需要很麻烦的配置
4. 没写自动建表功能（我的锅）
5. 仅支持Windows 7或者更高的系统（如果你能把和Newbe框架交互的部分重写，**理论**上可以在Unix like系统上运行）
6. 运行效率过低

## 版权声明
1. 这个程序以**MIT许可证**开源
2. ChikaBot不是商标，只是和某偶像企划的角色名称相似

[QPomelo](https://llquery.cn) 2018
