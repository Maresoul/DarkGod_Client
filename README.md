# DarkGod_Client

Unity开发的3D-ARPG俯视角网络游戏，完整的客户端+服务端架构，实现常见的游戏业务系统和战斗模块。

● gif形式展示，如果未显示稍等一段时间加载

● 完整项目演示地址：[项目演示地址](https://www.bilibili.com/video/BV1MpTkeCEWe/?spm_id_from=333.999.0.0&vd_source=15ce64d8f8fad36086523ce711dec730)

## 项目架构

- **客户端**: 基于Unity3D引擎开发，使用C#编写游戏逻辑。
- **服务端**: 基于PESocket库的服务器开发，数据库使用MySQL，支持多玩家并发处理。
- **网络通信**: 使用WebSocket协议实现客户端与服务端之间的实时通信

## UI逻辑框架

![UI逻辑框架](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/UI%E9%80%BB%E8%BE%91%E6%A1%86%E6%9E%B6.png)

## 服务端逻辑

![服务端逻辑](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/%E6%9C%8D%E5%8A%A1%E7%AB%AF%E6%A1%86%E6%9E%B6.png)

## 战斗逻辑框架

![战斗逻辑框架](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/%E6%88%98%E6%96%97%E9%80%BB%E8%BE%91%E6%A1%86%E6%9E%B6.png)

## 功能演示

1. **登陆场景和角色创建(服务器端和数据库)**
   <imgsrc="https://github.com/Maresoul/DarkGod\_Client/blob/main/DisplayResources/tinywow\_%E5%9C%BA%E6%99%AF%E5%8A%A0%E8%BD%BD\_1\_62447109.gif"alt="登陆场景和角色创建"width="400" />
2. **界面加载(异步)**
   ![界面加载](路径/到/gif图)
3. **Tip动态提示**
   ![Tip动态提示](路径/到/gif图)
4. **玩家移动和主城UI**
   ![玩家移动和主城UI](路径/到/gif图)
5. **角色信息展示面板**
   ![角色信息展示面板](路径/到/gif图)
6. **任务引导（Navigator自动寻路）**
   ![任务引导](路径/到/gif图)
7. **NPC对话**
   ![NPC对话](路径/到/gif图)
8. **角色强化升级**
   ![角色强化升级](路径/到/gif图)
9. **铸造**
   ![铸造](路径/到/gif图)
10. **任务系统**
    ![任务系统](路径/到/gif图)
11. **世界聊天**
    ![世界聊天](路径/到/gif图)
12. **角色下线后的定时体力恢复系统**
    ![定时体力恢复系统](路径/到/gif图)
13. **副本系统**
    ![副本系统](路径/到/gif图)
14. **角色状态控制**
    ![角色状态控制](路径/到/gif图)
15. **技能表现（CD,位移，动画播放，伤害检测）**
    ![技能表现](路径/到/gif图)
16. **普攻连招**
    ![普攻连招](路径/到/gif图)
17. **怪物AI表现**
    ![怪物AI表现](路径/到/gif图)
18. **攻击与受击，血条控制，UI飘字**
    ![攻击与受击](路径/到/gif图)
19. **战斗失败**
    ![战斗失败](路径/到/gif图)
20. **战斗成功结算界面**
    ![战斗成功结算界面](路径/到/gif图)
